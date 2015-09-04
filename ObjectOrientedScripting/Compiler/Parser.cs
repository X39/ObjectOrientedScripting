using Compiler.OOS_LanguageObjects;



using System;



public class Parser {
	public const int _EOF = 0;
	public const int _T_SCALAR = 1;
	public const int _T_STRING = 2;
	public const int _T_IDENT = 3;
	public const int _T_TERMINATOR = 4;
	public const int _T_EXPOP = 5;
	public const int _T_ASSIGNMENTCHAR = 6;
	public const int _T_ROUNDBRACKETOPEN = 7;
	public const int _T_ROUNDBRACKETCLOSE = 8;
	public const int _T_CODEBRACKETOPEN = 9;
	public const int _T_CODEBRACKETCLOSE = 10;
	public const int maxT = 49;

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
			{
				scanner.ResetPeek();
				return false;
			}
            if (t.next == null)
                t = scanner.Peek();
            else
                t = t.next;
		}
        scanner.ResetPeek();
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

	void IDENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		Expect(3);
		var obj = new Ident(parent, t.val); outObj = obj;
	}

	void ENCAPSULATION(out Encapsulation e) {
		e = Encapsulation.NA; 
		if (la.kind == 11) {
			Get();
			e = Encapsulation.Public; 
		} else if (la.kind == 12) {
			Get();
			e = Encapsulation.Private; 
		} else if (la.kind == 13) {
			Get();
			e = Encapsulation.Protected; 
		} else SynErr(50);
	}

	void VARTYPE(out VarType e) {
		e = VarType.Void; 
		switch (la.kind) {
		case 14: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 15: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 16: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 17: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 18: {
			Get();
			e = VarType.Bool; 
			break;
		}
		case 19: {
			Get();
			e = VarType.String; 
			break;
		}
		case 20: {
			Get();
			e = VarType.Object; 
			break;
		}
		default: SynErr(51); break;
		}
	}

	void BOOLEAN(out bool flag) {
		flag = false; 
		if (la.kind == 21) {
			Get();
			flag = true; 
		} else if (la.kind == 22) {
			Get();
			flag = false; 
		} else SynErr(52);
	}

	void VALUE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Value(parent); outObj = obj; outObj = obj; pBaseLangObject blo; bool flag; 
		if (la.kind == 2) {
			Get();
			obj.varType = VarType.String; obj.value = t.val; 
		} else if (la.kind == 1) {
			Get();
			obj.varType = VarType.Scalar; obj.value = t.val; 
		} else if (la.kind == 21 || la.kind == 22) {
			BOOLEAN(out flag);
			obj.varType = VarType.Bool; obj.value = (flag ? "true" : "false"); 
		} else SynErr(53);
	}

	void EXPRESSION_HELPER(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Expression(parent); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; 
		if (la.kind == 23) {
			Get();
			obj.negate = true; 
		}
		if (StartOf(1)) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.kind == 36) {
			OP_NEWINSTANCE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.kind == 3) {
			IDENT(out blo, obj);
			obj.lExpression = blo; 
			if (la.kind == 7) {
				BODY_FUNCTIONCALL(out blo2, blo);
				blo.addChild(blo2); 
			}
		} else if (la.kind == 48) {
			OP_SQFCALL(out blo, obj);
			obj.lExpression = blo; 
		} else SynErr(54);
		while (la.kind == 5 || la.kind == 24 || la.kind == 25) {
			if (la.kind == 24) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 25) {
				Get();
				obj.expOperator = t.val; 
			} else {
				Get();
				obj.expOperator = t.val; 
				EXPRESSION(out blo, obj);
				obj.rExpression = blo; 
			}
		}
	}

	void OP_NEWINSTANCE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewInstance(parent); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; 
		Expect(36);
		IDENT(out blo, obj);
		obj.addChild(blo); 
		BODY_FUNCTIONCALL(out blo2, blo);
		blo.addChild(blo2); 
	}

	void BODY_FUNCTIONCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		Expect(7);
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 30) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(8);
	}

	void OP_SQFCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new SqfCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(48);
		if (la.kind == 7) {
			Get();
			while (StartOf(2)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(8);
		}
		IDENT(out blo, parent);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 7) {
			Get();
			obj.markEnd(); 
			while (StartOf(2)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(8);
		}
	}

	void EXPRESSION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (la.kind == 7) {
			Get();
			EXPRESSION_HELPER(out outObj, parent);
			Expect(8);
		} else if (StartOf(3)) {
			EXPRESSION_HELPER(out outObj, parent);
		} else SynErr(55);
	}

	void OOS() {
		var obj = new Base(); pBaseLangObject blo; 
		while (StartOf(4)) {
			if (la.kind == 27) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 28) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 31) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (StartOf(5)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(56);
			}
		}
	}

	void NAMESPACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Namespace(parent); outObj = obj; pBaseLangObject blo; 
		Expect(27);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(9);
		while (StartOf(4)) {
			if (la.kind == 27) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 28) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 31) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (StartOf(5)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(57);
			}
		}
		Expect(10);
	}

	void CLASS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosClass(parent); outObj = obj; pBaseLangObject blo; Encapsulation e = Encapsulation.Private; 
		Expect(28);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 29) {
			Get();
			IDENT(out blo, obj);
			obj.addParentClass(blo); 
			while (la.kind == 30) {
				Get();
				IDENT(out blo, obj);
				obj.addParentClass(blo); 
			}
		}
		Expect(9);
		while (StartOf(6)) {
			if (StartOf(7)) {
				if (la.kind == 11 || la.kind == 12 || la.kind == 13) {
					ENCAPSULATION(out e);
				} else {
					Get();
					e = Encapsulation.Static; 
				}
			}
			if (peekCompare(-1, -1, _T_TERMINATOR) ) {
				NEWVARIABLE(out blo, obj, e);
				obj.addChild(blo); 
				TERMINATOR();
			} else if (StartOf(5)) {
				FUNCTION(out blo, obj, e);
				obj.addChild(blo); 
			} else SynErr(58);
		}
		Expect(10);
	}

	void INTERFACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosInterface(parent); outObj = obj; pBaseLangObject blo; 
		Expect(31);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(9);
		while (StartOf(8)) {
			VFUNCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(10);
	}

	void NEWVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		VARTYPE(out v);
		obj.varType = v; 
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
	}

	void FUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(8)) {
			if (StartOf(9)) {
				VARTYPE(out v);
				obj.functionVarType = v; 
			} else {
				Get();
				obj.functionVarType = VarType.Void; 
			}
		}
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(7);
		if (StartOf(9)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 30) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(8);
		obj.markArgListEnd(); 
		Expect(9);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(10);
	}

	void VFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VirtualFunction(parent); outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(9)) {
			VARTYPE(out v);
			obj.functionVarType = v; 
		} else if (la.kind == 32) {
			Get();
			obj.functionVarType = VarType.Void; 
		} else SynErr(59);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(7);
		if (StartOf(9)) {
			VARTYPE(out v);
			obj.argTypes.Add(v); 
			while (la.kind == 30) {
				Get();
				VARTYPE(out v);
				obj.argTypes.Add(v); 
			}
		}
		Expect(8);
		TERMINATOR();
	}

	void CODEINSTRUCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (StartOf(11)) {
			CODEINSTRUCTION_SC(out outObj, parent);
			TERMINATOR();
		} else if (StartOf(12)) {
			CODEINSTRUCTION_NSC(out outObj, parent);
		} else SynErr(60);
	}

	void AUTOVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		Expect(33);
		obj.varType = VarType.Auto; 
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
	}

	void CODEINSTRUCTION_SC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; pBaseLangObject blo; 
		if (la.kind == 42) {
			OP_THROW(out outObj, parent);
		} else if (la.kind == 43) {
			OP_RETURN(out outObj, parent);
		} else if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN) ) {
			IDENT(out outObj, parent);
			BODY_FUNCTIONCALL(out blo, outObj);
			outObj.addChild(blo); 
		} else if (peekCompare(_T_IDENT, _T_ASSIGNMENTCHAR) ) {
			IDENT(out outObj, parent);
			BODY_ASSIGNMENT(out blo, outObj);
			outObj.addChild(blo); 
		} else if (StartOf(9)) {
			NEWVARIABLE(out outObj, parent);
			if (la.kind == 6) {
				BODY_ASSIGNMENT(out blo, outObj);
				outObj.addChild(blo); 
			}
		} else if (la.kind == 33) {
			AUTOVARIABLE(out outObj, parent);
			BODY_ASSIGNMENT(out blo, outObj);
			outObj.addChild(blo); 
		} else if (StartOf(2)) {
			EXPRESSION(out outObj, parent);
		} else SynErr(61);
	}

	void OP_THROW(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Throw(parent); outObj = obj; pBaseLangObject blo; 
		Expect(42);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void OP_RETURN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Return(parent); outObj = obj; pBaseLangObject blo; 
		Expect(43);
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
		}
	}

	void BODY_ASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		Expect(6);
		obj.operation = t.val; 
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void CODEINSTRUCTION_NSC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (la.kind == 34) {
			OP_FOR(out outObj, parent);
		} else if (la.kind == 35) {
			OP_WHILE(out outObj, parent);
		} else if (la.kind == 37) {
			OP_IFELSE(out outObj, parent);
		} else if (la.kind == 44) {
			OP_SWITCH(out outObj, parent);
		} else if (la.kind == 39) {
			OP_TRYCATCH(out outObj, parent);
		} else SynErr(62);
	}

	void OP_FOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new For(parent); outObj = obj; pBaseLangObject blo; 
		Expect(34);
		Expect(7);
		if (StartOf(11)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg1 = blo; 
		}
		TERMINATOR();
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.forArg2 = blo; 
		}
		TERMINATOR();
		if (StartOf(11)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg3 = blo; 
		}
		Expect(8);
		if (la.kind == 9) {
			Get();
			while (StartOf(13)) {
				if (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(10);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(63);
	}

	void OP_WHILE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new While(parent); outObj = obj; pBaseLangObject blo; 
		Expect(35);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		if (la.kind == 9) {
			Get();
			while (StartOf(13)) {
				if (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(10);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(64);
	}

	void OP_IFELSE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new IfElse(parent); outObj = obj; pBaseLangObject blo; 
		Expect(37);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		if (la.kind == 9) {
			Get();
			while (StartOf(10)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(10);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(65);
		if (la.kind == 38) {
			Get();
			obj.markIfEnd(); 
			if (la.kind == 9) {
				Get();
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				}
				Expect(10);
			} else if (StartOf(10)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(66);
		}
	}

	void OP_SWITCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Switch(parent); Case caseObj; outObj = obj; pBaseLangObject blo; 
		Expect(44);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
		Expect(8);
		Expect(9);
		while (la.kind == 45 || la.kind == 46 || la.kind == 47) {
			if (la.kind == 45) {
				Get();
				caseObj = new Case(obj); obj.addChild(caseObj); 
				EXPRESSION(out blo, obj);
				caseObj.expression = blo; 
				Expect(29);
				while (la.kind == 45) {
					Get();
					EXPRESSION(out blo, obj);
					caseObj.addChild(blo); 
					Expect(29);
				}
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 41) {
					OP_BREAK(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 42) {
					OP_THROW(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 43) {
					OP_RETURN(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else SynErr(67);
			} else {
				if (la.kind == 46) {
					Get();
					caseObj = new Case(obj); obj.addChild(caseObj); caseObj.expression = new Ident(caseObj, "default"); 
					Expect(29);
				} else {
					Get();
					caseObj = new Case(obj); obj.addChild(caseObj); caseObj.expression = new Ident(caseObj, "default"); 
				}
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 41) {
					OP_BREAK(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 42) {
					OP_THROW(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 43) {
					OP_RETURN(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else SynErr(68);
			}
		}
		Expect(10);
	}

	void OP_TRYCATCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new TryCatch(parent); outObj = obj; pBaseLangObject blo; 
		Expect(39);
		Expect(9);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(10);
		Expect(40);
		Expect(7);
		NEWVARIABLE(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		obj.markIfEnd(); 
		Expect(9);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(10);
	}

	void OP_BREAK(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Break(parent); outObj = obj; pBaseLangObject blo; 
		Expect(41);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _T,_T,_x,_T, _x,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _T,_x,_x,_x, _x,_x,_T,_T, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _x,_T,_x,_T, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _T,_T,_x,_T, _x,_T,_T,_T, _T,_x,_x,_x, _T,_x,_x}

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
			case 6: s = "T_ASSIGNMENTCHAR expected"; break;
			case 7: s = "T_ROUNDBRACKETOPEN expected"; break;
			case 8: s = "T_ROUNDBRACKETCLOSE expected"; break;
			case 9: s = "T_CODEBRACKETOPEN expected"; break;
			case 10: s = "T_CODEBRACKETCLOSE expected"; break;
			case 11: s = "\"public\" expected"; break;
			case 12: s = "\"private\" expected"; break;
			case 13: s = "\"protected\" expected"; break;
			case 14: s = "\"scalar\" expected"; break;
			case 15: s = "\"int\" expected"; break;
			case 16: s = "\"double\" expected"; break;
			case 17: s = "\"float\" expected"; break;
			case 18: s = "\"bool\" expected"; break;
			case 19: s = "\"string\" expected"; break;
			case 20: s = "\"object\" expected"; break;
			case 21: s = "\"true\" expected"; break;
			case 22: s = "\"false\" expected"; break;
			case 23: s = "\"!\" expected"; break;
			case 24: s = "\"++\" expected"; break;
			case 25: s = "\"--\" expected"; break;
			case 26: s = "\"static\" expected"; break;
			case 27: s = "\"namespace\" expected"; break;
			case 28: s = "\"class\" expected"; break;
			case 29: s = "\":\" expected"; break;
			case 30: s = "\",\" expected"; break;
			case 31: s = "\"interface\" expected"; break;
			case 32: s = "\"void\" expected"; break;
			case 33: s = "\"auto\" expected"; break;
			case 34: s = "\"for\" expected"; break;
			case 35: s = "\"while\" expected"; break;
			case 36: s = "\"new\" expected"; break;
			case 37: s = "\"if\" expected"; break;
			case 38: s = "\"else\" expected"; break;
			case 39: s = "\"try\" expected"; break;
			case 40: s = "\"catch\" expected"; break;
			case 41: s = "\"break\" expected"; break;
			case 42: s = "\"throw\" expected"; break;
			case 43: s = "\"return\" expected"; break;
			case 44: s = "\"switch\" expected"; break;
			case 45: s = "\"case\" expected"; break;
			case 46: s = "\"default\" expected"; break;
			case 47: s = "\"default:\" expected"; break;
			case 48: s = "\"SQF\" expected"; break;
			case 49: s = "??? expected"; break;
			case 50: s = "invalid ENCAPSULATION"; break;
			case 51: s = "invalid VARTYPE"; break;
			case 52: s = "invalid BOOLEAN"; break;
			case 53: s = "invalid VALUE"; break;
			case 54: s = "invalid EXPRESSION_HELPER"; break;
			case 55: s = "invalid EXPRESSION"; break;
			case 56: s = "invalid OOS"; break;
			case 57: s = "invalid NAMESPACE"; break;
			case 58: s = "invalid CLASS"; break;
			case 59: s = "invalid VFUNCTION"; break;
			case 60: s = "invalid CODEINSTRUCTION"; break;
			case 61: s = "invalid CODEINSTRUCTION_SC"; break;
			case 62: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 63: s = "invalid OP_FOR"; break;
			case 64: s = "invalid OP_WHILE"; break;
			case 65: s = "invalid OP_IFELSE"; break;
			case 66: s = "invalid OP_IFELSE"; break;
			case 67: s = "invalid OP_SWITCH"; break;
			case 68: s = "invalid OP_SWITCH"; break;

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
