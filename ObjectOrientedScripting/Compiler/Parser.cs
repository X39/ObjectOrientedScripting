
using System;



public class Parser {
	public const int _EOF = 0;
	public const int _T_SCALAR = 1;
	public const int _T_STRING = 2;
	public const int _T_IDENT = 3;
	public const int _T_TERMINATOR = 4;
	public const int _T_EXPOP = 5;
	public const int _T_ASSIGNEMENTCHAR = 6;
	public const int _T_ROUNDBRACKETOPEN = 7;
	public const int _T_ROUNDBRACKETCLOSE = 8;
	public const int maxT = 43;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}
	
	bool peekCompare(params int[] values)
	{
		Token t = la;
		foreach(int i in values)
		{
			if(i != -1 && t.kind != i)
				return false;
			t = t.next;
		}
		return true;
	}
	
	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void TERMINATOR() {
		Expect(4);
		while (la.kind == 4) {
			Get();
		}
	}

	void IDENT() {
		Expect(3);
	}

	void ENCAPSULATION() {
		if (la.kind == 9) {
			Get();
		} else if (la.kind == 10) {
			Get();
		} else if (la.kind == 11) {
			Get();
		} else SynErr(44);
	}

	void VARTYPE() {
		if (la.kind == 12) {
			Get();
		} else if (la.kind == 13) {
			Get();
		} else if (la.kind == 14) {
			Get();
		} else SynErr(45);
	}

	void STRING() {
		Expect(2);
	}

	void BOOLEAN() {
		if (la.kind == 15) {
			Get();
		} else if (la.kind == 16) {
			Get();
		} else SynErr(46);
	}

	void VALUE() {
		if (la.kind == 2) {
			STRING();
		} else if (la.kind == 1) {
			Get();
		} else if (la.kind == 15 || la.kind == 16) {
			BOOLEAN();
		} else SynErr(47);
	}

	void EXPRESSION_HELPER() {
		if (la.kind == 17) {
			Get();
		}
		if (StartOf(1)) {
			VALUE();
		} else if (la.kind == 31) {
			OP_NEWINSTANCE();
		} else if (la.kind == 3) {
			IDENT();
			BODY_FUNCTIONCALL();
		} else if (la.kind == 42) {
			OP_SQFCALL();
		} else SynErr(48);
		while (la.kind == 5) {
			Get();
			EXPRESSION();
		}
	}

	void OP_NEWINSTANCE() {
		Expect(31);
		IDENT();
	}

	void BODY_FUNCTIONCALL() {
		Expect(7);
		if (StartOf(2)) {
			EXPRESSION();
			while (la.kind == 24) {
				Get();
				EXPRESSION();
			}
		}
		Expect(8);
	}

	void OP_SQFCALL() {
		Expect(42);
		if (la.kind == 7) {
			Get();
			while (StartOf(2)) {
				EXPRESSION();
			}
			Expect(8);
		}
		Expect(3);
		if (la.kind == 7) {
			Get();
			while (StartOf(2)) {
				EXPRESSION();
			}
			Expect(8);
		}
	}

	void EXPRESSION() {
		if (la.kind == 7) {
			Get();
			EXPRESSION_HELPER();
			Expect(8);
		} else if (StartOf(3)) {
			EXPRESSION_HELPER();
		} else SynErr(49);
	}

	void OOS() {
		if (la.kind == 18) {
			NAMESPACE();
		} else if (la.kind == 22) {
			CLASS();
		} else if (la.kind == 25) {
			INTERFACE();
		} else SynErr(50);
		while (la.kind == 18 || la.kind == 22 || la.kind == 25) {
			if (la.kind == 18) {
				NAMESPACE();
			} else if (la.kind == 22) {
				CLASS();
			} else {
				INTERFACE();
			}
		}
	}

	void NAMESPACE() {
		Expect(18);
		Expect(3);
		Expect(19);
		while (StartOf(4)) {
			if (la.kind == 18) {
				NAMESPACE();
			} else if (la.kind == 22) {
				CLASS();
			} else if (la.kind == 25) {
				INTERFACE();
			} else {
				if (la.kind == 20) {
					Get();
					NEWVARIABLE();
				} else {
					FUNCTION();
				}
			}
		}
		Expect(21);
	}

	void CLASS() {
		Expect(22);
		Expect(3);
		if (la.kind == 23) {
			Get();
			IDENT();
			while (la.kind == 24) {
				Get();
				IDENT();
			}
		}
		Expect(19);
		while (StartOf(5)) {
			if (la.kind == 9 || la.kind == 10 || la.kind == 11) {
				ENCAPSULATION();
				FUNCTION();
			} else {
				NEWVARIABLE();
			}
		}
		Expect(21);
	}

	void INTERFACE() {
		Expect(25);
		Expect(3);
		Expect(19);
		while (la.kind == 9 || la.kind == 10 || la.kind == 11) {
			ENCAPSULATION();
			VFUNCTION();
		}
		Expect(21);
	}

	void NEWVARIABLE() {
		VARTYPE();
		Expect(3);
	}

	void FUNCTION() {
		if (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			VARTYPE();
		} else if (la.kind == 26) {
			Get();
		} else SynErr(51);
		IDENT();
		Expect(7);
		if (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			VARTYPE();
			IDENT();
			while (la.kind == 24) {
				Get();
				VARTYPE();
				IDENT();
			}
		}
		Expect(8);
		Expect(19);
		while (StartOf(6)) {
			CODEINSTRUCTION();
		}
		Expect(21);
	}

	void VFUNCTION() {
		Expect(27);
		if (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			VARTYPE();
		} else if (la.kind == 26) {
			Get();
		} else SynErr(52);
		Expect(3);
		Expect(7);
		if (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			VARTYPE();
			while (la.kind == 24) {
				Get();
				VARTYPE();
			}
		}
		Expect(8);
		TERMINATOR();
	}

	void CODEINSTRUCTION() {
		if (StartOf(7)) {
			CODEINSTRUCTION_SC();
			TERMINATOR();
		} else if (StartOf(8)) {
			CODEINSTRUCTION_NSC();
		} else SynErr(53);
	}

	void AUTOVARIABLE() {
		Expect(28);
		Expect(3);
	}

	void CODEINSTRUCTION_SC() {
		if (la.kind == 37) {
			OP_THROW();
		} else if (la.kind == 38) {
			OP_RETURN();
		} else if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN) ) {
			IDENT();
			BODY_FUNCTIONCALL();
		} else if (peekCompare(_T_IDENT, _T_ASSIGNEMENTCHAR) ) {
			IDENT();
			BODY_ASSIGNMENT();
		} else if (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			NEWVARIABLE();
			if (la.kind == 6) {
				BODY_ASSIGNMENT();
			}
		} else if (la.kind == 28) {
			AUTOVARIABLE();
			BODY_ASSIGNMENT();
		} else if (StartOf(2)) {
			EXPRESSION();
		} else SynErr(54);
	}

	void OP_THROW() {
		Expect(37);
		EXPRESSION();
	}

	void OP_RETURN() {
		Expect(38);
		if (StartOf(2)) {
			EXPRESSION();
		}
	}

	void BODY_ASSIGNMENT() {
		Expect(6);
		EXPRESSION();
	}

	void CODEINSTRUCTION_NSC() {
		if (la.kind == 29) {
			OP_FOR();
		} else if (la.kind == 30) {
			OP_WHILE();
		} else if (la.kind == 32) {
			OP_IFELSE();
		} else if (la.kind == 39) {
			OP_SWITCH();
		} else if (la.kind == 34) {
			OP_TRYCATCH();
		} else SynErr(55);
	}

	void OP_FOR() {
		Expect(29);
		Expect(7);
		if (StartOf(7)) {
			CODEINSTRUCTION_SC();
		}
		TERMINATOR();
		if (StartOf(2)) {
			EXPRESSION();
		}
		TERMINATOR();
		if (StartOf(7)) {
			CODEINSTRUCTION_SC();
		}
		Expect(8);
		if (la.kind == 19) {
			Get();
			while (StartOf(9)) {
				if (StartOf(6)) {
					CODEINSTRUCTION();
				} else {
					OP_BREAK();
					TERMINATOR();
				}
			}
			Expect(21);
		} else if (StartOf(6)) {
			CODEINSTRUCTION();
		} else SynErr(56);
	}

	void OP_WHILE() {
		Expect(30);
		Expect(7);
		EXPRESSION();
		Expect(8);
		if (la.kind == 19) {
			Get();
			while (StartOf(9)) {
				if (StartOf(6)) {
					CODEINSTRUCTION();
				} else {
					OP_BREAK();
					TERMINATOR();
				}
			}
			Expect(21);
		} else if (StartOf(6)) {
			CODEINSTRUCTION();
		} else SynErr(57);
	}

	void OP_IFELSE() {
		Expect(32);
		Expect(7);
		EXPRESSION();
		Expect(8);
		if (la.kind == 19) {
			Get();
			while (StartOf(6)) {
				CODEINSTRUCTION();
			}
			Expect(21);
		} else if (StartOf(6)) {
			CODEINSTRUCTION();
		} else SynErr(58);
		if (la.kind == 33) {
			Get();
			if (la.kind == 19) {
				Get();
				while (StartOf(6)) {
					CODEINSTRUCTION();
				}
				Expect(21);
			} else if (StartOf(6)) {
				CODEINSTRUCTION();
			} else SynErr(59);
		}
	}

	void OP_SWITCH() {
		Expect(39);
		Expect(7);
		EXPRESSION();
		Expect(8);
		Expect(19);
		while (la.kind == 40 || la.kind == 41) {
			if (la.kind == 40) {
				Get();
				EXPRESSION();
				Expect(23);
				while (la.kind == 40) {
					Get();
					EXPRESSION();
					Expect(23);
				}
				while (StartOf(6)) {
					CODEINSTRUCTION();
				}
				if (la.kind == 36) {
					OP_BREAK();
				} else if (la.kind == 37) {
					OP_THROW();
				} else if (la.kind == 38) {
					OP_RETURN();
				} else SynErr(60);
			} else {
				Get();
				Expect(23);
				while (StartOf(6)) {
					CODEINSTRUCTION();
				}
				if (la.kind == 36) {
					OP_BREAK();
				} else if (la.kind == 37) {
					OP_THROW();
				} else if (la.kind == 38) {
					OP_RETURN();
				} else SynErr(61);
			}
		}
		Expect(21);
	}

	void OP_TRYCATCH() {
		Expect(34);
		Expect(19);
		while (StartOf(6)) {
			CODEINSTRUCTION();
		}
		Expect(21);
		Expect(35);
		Expect(7);
		NEWVARIABLE();
		Expect(8);
		Expect(19);
		while (StartOf(6)) {
			CODEINSTRUCTION();
		}
		Expect(21);
	}

	void OP_BREAK() {
		Expect(36);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_T,_x, _T,_x,_T,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_x,_T,_x, _x,_T,_T,_T, _x,_x,_T,_x, _x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_T, _x,_x,_x,_x, _x,_T,_T,_x, _x,_x,_T,_x, _x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _T,_x,_T,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_x,_T,_x, _T,_T,_T,_T, _x,_x,_T,_x, _x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "T_SCALAR expected"; break;
			case 2: s = "T_STRING expected"; break;
			case 3: s = "T_IDENT expected"; break;
			case 4: s = "T_TERMINATOR expected"; break;
			case 5: s = "T_EXPOP expected"; break;
			case 6: s = "T_ASSIGNEMENTCHAR expected"; break;
			case 7: s = "T_ROUNDBRACKETOPEN expected"; break;
			case 8: s = "T_ROUNDBRACKETCLOSE expected"; break;
			case 9: s = "\"public\" expected"; break;
			case 10: s = "\"private\" expected"; break;
			case 11: s = "\"protected\" expected"; break;
			case 12: s = "\"scalar\" expected"; break;
			case 13: s = "\"bool\" expected"; break;
			case 14: s = "\"string\" expected"; break;
			case 15: s = "\"true\" expected"; break;
			case 16: s = "\"false\" expected"; break;
			case 17: s = "\"!\" expected"; break;
			case 18: s = "\"namespace\" expected"; break;
			case 19: s = "\"{\" expected"; break;
			case 20: s = "\"static\" expected"; break;
			case 21: s = "\"}\" expected"; break;
			case 22: s = "\"class\" expected"; break;
			case 23: s = "\":\" expected"; break;
			case 24: s = "\",\" expected"; break;
			case 25: s = "\"interface\" expected"; break;
			case 26: s = "\"void\" expected"; break;
			case 27: s = "\"virtual\" expected"; break;
			case 28: s = "\"auto\" expected"; break;
			case 29: s = "\"for\" expected"; break;
			case 30: s = "\"while\" expected"; break;
			case 31: s = "\"new\" expected"; break;
			case 32: s = "\"if\" expected"; break;
			case 33: s = "\"else\" expected"; break;
			case 34: s = "\"try\" expected"; break;
			case 35: s = "\"catch\" expected"; break;
			case 36: s = "\"break\" expected"; break;
			case 37: s = "\"throw\" expected"; break;
			case 38: s = "\"return\" expected"; break;
			case 39: s = "\"switch\" expected"; break;
			case 40: s = "\"case\" expected"; break;
			case 41: s = "\"default\" expected"; break;
			case 42: s = "\"SQF\" expected"; break;
			case 43: s = "??? expected"; break;
			case 44: s = "invalid ENCAPSULATION"; break;
			case 45: s = "invalid VARTYPE"; break;
			case 46: s = "invalid BOOLEAN"; break;
			case 47: s = "invalid VALUE"; break;
			case 48: s = "invalid EXPRESSION_HELPER"; break;
			case 49: s = "invalid EXPRESSION"; break;
			case 50: s = "invalid OOS"; break;
			case 51: s = "invalid FUNCTION"; break;
			case 52: s = "invalid VFUNCTION"; break;
			case 53: s = "invalid CODEINSTRUCTION"; break;
			case 54: s = "invalid CODEINSTRUCTION_SC"; break;
			case 55: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 56: s = "invalid OP_FOR"; break;
			case 57: s = "invalid OP_WHILE"; break;
			case 58: s = "invalid OP_IFELSE"; break;
			case 59: s = "invalid OP_IFELSE"; break;
			case 60: s = "invalid OP_SWITCH"; break;
			case 61: s = "invalid OP_SWITCH"; break;

			default: s = "error " + n; break;
		}
        Logger.Instance.log(Logger.LogLevel.ERROR, String.Format(errMsgFormat, line, col, s));
		//errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
        Logger.Instance.log(Logger.LogLevel.ERROR, String.Format(errMsgFormat, line, col, s));
		//errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
        Logger.Instance.log(Logger.LogLevel.ERROR, s);
		//errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
        Logger.Instance.log(Logger.LogLevel.WARNING, String.Format(errMsgFormat, line, col, s));
		//errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
        Logger.Instance.log(Logger.LogLevel.WARNING, s);
		//errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
