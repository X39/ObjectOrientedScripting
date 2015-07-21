using Compiler.OOS_LanguageObjects;
using Compiler.OOS_LanguageObjects.HelperClasses;
using Compiler.OOS_LanguageObjects.Ex;




using System;



public class Parser {
	public const int _EOF = 0;
	public const int _UINTEGER = 1;
	public const int _UDOUBLE = 2;
	public const int _INTEGER = 3;
	public const int _DOUBLE = 4;
	public const int _FQIDENT = 5;
	public const int _IDENT = 6;
	public const int _LOCALIDENT = 7;
	public const int _LINETERMINATOR = 8;
	public const int _STRING = 9;
	public const int _COMMA = 10;
	public const int maxT = 70;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;
	OosContainer oosTreeBase;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
		oosTreeBase = null;
	}
	
    public void getBaseContainer(out OosContainer blo)
    {
        blo =  oosTreeBase;
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

	
	void SCALAR(out BaseLangObject bloOut) {
		var v = new OosValue(); bloOut = (BaseLangObject)v; 
		if (la.kind == 4) {
			Get();
			v.Value = t.val; 
		} else if (la.kind == 3) {
			Get();
			v.Value = t.val; 
		} else if (la.kind == 2) {
			Get();
			v.Value = t.val; 
		} else if (la.kind == 1) {
			Get();
			v.Value = t.val; 
		} else SynErr(71);
	}

	void ARRAY(out BaseLangObject bloOut) {
		var v1 = new OosValue(); bloOut = (BaseLangObject)v1;  BaseLangObject v2; 
		Expect(11);
		v1.append("["); 
		VALUE(out v2);
		v1.append((OosValue)v2); 
		while (la.kind == 10) {
			Get();
			v1.append(","); 
			VALUE(out v2);
			v1.append((OosValue)v2); 
		}
		Expect(12);
		v1.append("]"); 
	}

	void VALUE(out BaseLangObject bloOut) {
		var v = new OosValue(); bloOut = (BaseLangObject)v;  
		if (StartOf(1)) {
			SCALAR(out bloOut);
		} else if (la.kind == 9) {
			Get();
			v.Value = t.val; 
		} else if (la.kind == 11) {
			ARRAY(out bloOut);
		} else if (la.kind == 13 || la.kind == 14) {
			if (la.kind == 13) {
				Get();
				v.Value = t.val; 
			} else {
				Get();
				v.Value = t.val; 
			}
		} else SynErr(72);
	}

	void ENCAPSULATION(out ClassEncapsulation e) {
		e = ClassEncapsulation.PUBLIC; 
		if (la.kind == 15) {
			Get();
			e = ClassEncapsulation.PRIVATE; 
		} else if (la.kind == 16) {
			Get();
			e = ClassEncapsulation.PUBLIC; 
		} else SynErr(73);
	}

	void ASSIGNMENTOPERATORS(out AssignmentOperators ao) {
		ao = AssignmentOperators.Equals; 
		if (la.kind == 17) {
			Get();
			ao = AssignmentOperators.PlusEquals; 
		} else if (la.kind == 18) {
			Get();
			ao = AssignmentOperators.MinusEquals; 
		} else if (la.kind == 19) {
			Get();
			ao = AssignmentOperators.MultipliedEquals; 
		} else if (la.kind == 20) {
			Get();
			ao = AssignmentOperators.DividedEquals; 
		} else if (la.kind == 21) {
			Get();
			ao = AssignmentOperators.Equals; 
		} else SynErr(74);
	}

	void OOS() {
		oosTreeBase = new OosContainer(); BaseLangObject blo; 
		while (la.kind == 22 || la.kind == 25 || la.kind == 32) {
			if (la.kind == 22) {
				NAMESPACE(out blo);
				oosTreeBase.addChild(blo); 
			} else if (la.kind == 25) {
				CLASS(out blo);
				oosTreeBase.addChild(blo); 
			} else if (scanner.FollowedBy("function")) {
				GLOBALFUNCTION(out blo);
				oosTreeBase.addChild(blo); 
			} else {
				GLOBALVARIABLE(out blo);
				oosTreeBase.addChild(blo); 
			}
		}
	}

	void NAMESPACE(out BaseLangObject bloOut) {
		var n = new OosNamespace(); bloOut = (BaseLangObject)n; BaseLangObject blo; 
		Expect(22);
		Expect(6);
		n.Name = t.val; 
		Expect(23);
		while (la.kind == 22 || la.kind == 25 || la.kind == 32) {
			if (la.kind == 22) {
				NAMESPACE(out blo);
				n.addChild(blo); 
			} else if (la.kind == 25) {
				CLASS(out blo);
				n.addChild(blo); 
			} else if (scanner.FollowedBy("function")) {
				GLOBALFUNCTION(out blo);
				n.addChild(blo); 
			} else {
				GLOBALVARIABLE(out blo);
				n.addChild(blo); 
			}
		}
		Expect(24);
		if (la.kind == 8) {
			Get();
			while (la.kind == 8) {
				Get();
			}
		}
	}

	void CLASS(out BaseLangObject bloOut) {
		var c = new OosClass(); bloOut = (BaseLangObject)c; BaseLangObject blo; 
		Expect(25);
		Expect(6);
		c.Name = t.val; 
		if (la.kind == 26) {
			Get();
			Expect(6);
			c.ParentClasses.Add(t.val); 
			while (la.kind == 10) {
				Get();
				Expect(6);
				c.ParentClasses.Add(t.val); 
			}
		}
		Expect(23);
		while (StartOf(2)) {
			if (la.kind == 25) {
				CLASS(out blo);
				c.addChild(blo); blo.setParent(c); 
			} else if (la.kind == 29) {
				CLASSCONSTRUCTOR(out blo);
				c.addChild(blo); blo.setParent(c); ((BaseFunctionObject)blo).Name = "constructor"; 
			} else if (la.kind == 32) {
				if (scanner.FollowedBy("function")) {
					GLOBALFUNCTION(out blo);
					c.addChild(blo); blo.setParent(c); 
				} else {
					GLOBALVARIABLE(out blo);
					c.addChild(blo); blo.setParent(c); 
				}
			} else {
				if (scanner.FollowedByWO("function")) {
					CLASSFUNCTION(out blo);
					c.addChild(blo); blo.setParent(c); 
				} else {
					CLASSVARIABLE(out blo);
					c.addChild(blo); blo.setParent(c); 
				}
			}
		}
		Expect(24);
		if (la.kind == 8) {
			Get();
			while (la.kind == 8) {
				Get();
			}
		}
	}

	void GLOBALFUNCTION(out BaseLangObject bloOut) {
		var gf = new OosGlobalFunction(); bloOut = (BaseLangObject)gf; BaseLangObject blo; ListString argL;
		Expect(32);
		Expect(30);
		Expect(6);
		gf.Name = t.val; 
		ARGLIST(out argL);
		gf.ArgList = argL.getList(); 
		Expect(23);
		while (StartOf(3)) {
			if (StartOf(4)) {
				CODEINSTRUCTION(out blo);
				gf.addChild(blo); 
				Expect(8);
				while (la.kind == 8) {
					Get();
				}
			} else {
				CODEINSTRUCTION_OPTIONALSC(out blo);
				gf.addChild(blo); 
				if (la.kind == 8) {
					Get();
					while (la.kind == 8) {
						Get();
					}
				}
			}
		}
		Expect(24);
		if (la.kind == 8) {
			Get();
			while (la.kind == 8) {
				Get();
			}
		}
	}

	void GLOBALVARIABLE(out BaseLangObject bloOut) {
		var gv = new OosGlobalVariable(); bloOut = (BaseLangObject)gv; BaseLangObject blo; 
		Expect(32);
		Expect(33);
		Expect(6);
		gv.Name = t.val; 
		if (la.kind == 21) {
			Get();
			EXPRESSION(out blo);
			gv.Value = blo; 
		}
		Expect(8);
		while (la.kind == 8) {
			Get();
		}
	}

	void CLASSCONSTRUCTOR(out BaseLangObject bloOut) {
		var cf = new OosClassFunction(); cf.Encapsulation = ClassEncapsulation.PUBLIC; bloOut = (BaseLangObject)cf; BaseLangObject blo; ListString argL;
		Expect(29);
		ARGLIST(out argL);
		cf.ArgList = argL.getList(); 
		Expect(23);
		while (StartOf(3)) {
			if (StartOf(4)) {
				CODEINSTRUCTION(out blo);
				cf.addChild(blo); 
				Expect(8);
				while (la.kind == 8) {
					Get();
				}
			} else {
				CODEINSTRUCTION_OPTIONALSC(out blo);
				cf.addChild(blo); 
				if (la.kind == 8) {
					Get();
					while (la.kind == 8) {
						Get();
					}
				}
			}
		}
		Expect(24);
		if (la.kind == 8) {
			Get();
			while (la.kind == 8) {
				Get();
			}
		}
	}

	void CLASSFUNCTION(out BaseLangObject bloOut) {
		var cf = new OosClassFunction(); bloOut = (BaseLangObject)cf; BaseLangObject blo; ListString argL; ClassEncapsulation e;
		if (la.kind == 15 || la.kind == 16) {
			ENCAPSULATION(out e);
			cf.Encapsulation = e; 
		}
		Expect(30);
		if (la.kind == 31) {
			Get();
			cf.OverrideBase = true; 
		}
		Expect(6);
		cf.Name = t.val; 
		ARGLIST(out argL);
		cf.ArgList = argL.getList(); 
		Expect(23);
		while (StartOf(3)) {
			if (StartOf(4)) {
				CODEINSTRUCTION(out blo);
				cf.addChild(blo); 
				Expect(8);
				while (la.kind == 8) {
					Get();
				}
			} else {
				CODEINSTRUCTION_OPTIONALSC(out blo);
				cf.addChild(blo); 
				if (la.kind == 8) {
					Get();
					while (la.kind == 8) {
						Get();
					}
				}
			}
		}
		Expect(24);
		if (la.kind == 8) {
			Get();
			while (la.kind == 8) {
				Get();
			}
		}
	}

	void CLASSVARIABLE(out BaseLangObject bloOut) {
		var cv = new OosClassVariable(); bloOut = (BaseLangObject)cv; ClassEncapsulation e; BaseLangObject blo; 
		if (la.kind == 15 || la.kind == 16) {
			ENCAPSULATION(out e);
			cv.Encapsulation = e; 
		}
		Expect(33);
		Expect(6);
		cv.Name = t.val; 
		if (la.kind == 21) {
			Get();
			EXPRESSION(out blo);
			cv.Value = blo; 
		}
		Expect(8);
		while (la.kind == 8) {
			Get();
		}
	}

	void ARGLIST(out ListString l) {
		l = new ListString(); 
		Expect(27);
		if (la.kind == 7) {
			Get();
			l.Add(t.val); 
			while (la.kind == 10) {
				Get();
				Expect(7);
				l.Add(t.val); 
			}
		}
		Expect(28);
	}

	void CODEINSTRUCTION(out BaseLangObject bloOut) {
		bloOut = null; 
		if (la.kind == 61) {
			THROWINSTRUCTION(out bloOut);
		} else if (la.kind == 62) {
			RETURNINSTRUCTION(out bloOut);
		} else if (la.kind == 67) {
			BREAKINSTRUCTION(out bloOut);
		} else if (la.kind == 48 || la.kind == 49) {
			TYPEOF(out bloOut);
		} else if (la.kind == 33) {
			LOCALVARIABLE(out bloOut);
		} else if (scanner.FollowedBy("(")) {
			FUNCTIONCALL(out bloOut);
		} else if (scanner.FollowedBy(new string[] { "instanceof", "instanceOf" })) {
			INSTANCEOF(out bloOut);
		} else if (la.kind == 5 || la.kind == 7) {
			ASSIGNMENT(out bloOut);
		} else if (la.kind == 63) {
			ISSET(out bloOut);
		} else if (la.kind == 53) {
			SQFCALL(out bloOut);
		} else SynErr(75);
	}

	void CODEINSTRUCTION_OPTIONALSC(out BaseLangObject bloOut) {
		bloOut = null; 
		switch (la.kind) {
		case 54: {
			FORLOOP(out bloOut);
			break;
		}
		case 55: {
			WHILELOOP(out bloOut);
			break;
		}
		case 68: {
			IFELSE(out bloOut);
			break;
		}
		case 59: {
			TRYCATCH(out bloOut);
			break;
		}
		case 58: {
			SWITCH(out bloOut);
			break;
		}
		case 64: {
			NATIVEMULTI(out bloOut);
			break;
		}
		default: SynErr(76); break;
		}
	}

	void EXPRESSION(out BaseLangObject bloOut) {
		bloOut = null; 
		if (la.kind == 27) {
			Get();
			EXPRESSION_HELPER(out bloOut);
			Expect(28);
		} else if (StartOf(5)) {
			EXPRESSION_HELPER(out bloOut);
		} else SynErr(77);
	}

	void EXPRESSION_OPERATOR(out ExpressionOperator eo) {
		eo = ExpressionOperator.NA; 
		switch (la.kind) {
		case 34: {
			Get();
			eo = ExpressionOperator.And; 
			if (la.kind == 34) {
				Get();
				eo = ExpressionOperator.AndAnd; 
			}
			break;
		}
		case 35: {
			Get();
			eo = ExpressionOperator.Or; 
			if (la.kind == 35) {
				Get();
				eo = ExpressionOperator.OrOr; 
			}
			break;
		}
		case 36: {
			Get();
			eo = ExpressionOperator.Equals; 
			if (la.kind == 21) {
				Get();
				eo = ExpressionOperator.ExplicitEquals; 
			}
			break;
		}
		case 37: {
			Get();
			eo = ExpressionOperator.Plus; 
			break;
		}
		case 38: {
			Get();
			eo = ExpressionOperator.Minus; 
			break;
		}
		case 39: {
			Get();
			eo = ExpressionOperator.Multiplication; 
			break;
		}
		case 40: {
			Get();
			eo = ExpressionOperator.Division; 
			break;
		}
		case 41: {
			Get();
			eo = ExpressionOperator.Larger; 
			break;
		}
		case 42: {
			Get();
			eo = ExpressionOperator.LargerEquals; 
			break;
		}
		case 43: {
			Get();
			eo = ExpressionOperator.Smaller; 
			break;
		}
		case 44: {
			Get();
			eo = ExpressionOperator.SmallerEquals; 
			break;
		}
		default: SynErr(78); break;
		}
	}

	void EXPRESSION_SINGLE(out BaseLangObject bloOut) {
		bloOut = null; 
		switch (la.kind) {
		case 63: {
			ISSET(out bloOut);
			break;
		}
		case 1: case 2: case 3: case 4: case 9: case 11: case 13: case 14: {
			VALUE(out bloOut);
			break;
		}
		case 52: {
			OBJECTCREATION(out bloOut);
			break;
		}
		case 5: case 7: {
			if (scanner.FollowedBy("(")) {
				FUNCTIONCALL(out bloOut);
			} else if (la.kind == 5) {
				Get();
				bloOut = new OosVariable(t.val); 
			} else {
				Get();
				bloOut = new OosVariable(t.val); 
			}
			break;
		}
		case 53: {
			SQFCALL(out bloOut);
			break;
		}
		case 66: {
			NATIVESINGLE(out bloOut);
			break;
		}
		default: SynErr(79); break;
		}
	}

	void ISSET(out BaseLangObject bloOut) {
		var obj = new OosIsSet(); bloOut = (BaseLangObject)obj; BaseLangObject blo; 
		Expect(63);
		Expect(27);
		EXPRESSION(out blo);
		obj.Expression = blo; 
		Expect(28);
	}

	void OBJECTCREATION(out BaseLangObject bloOut) {
		var oc = new OosObjectCreation(); bloOut = (BaseLangObject)oc; ListBaseLangObject cl; 
		Expect(52);
		Expect(5);
		oc.Identifier = new OosVariable(t.val); 
		CALLLIST(out cl);
		oc.Children.AddRange(cl.getList()); 
	}

	void FUNCTIONCALL(out BaseLangObject bloOut) {
		var fc = new OosFunctionCall(); bloOut = (BaseLangObject)fc; ListBaseLangObject cl; 
		if (la.kind == 5) {
			Get();
			fc.Identifier = new OosVariable(t.val); 
		} else if (la.kind == 7) {
			Get();
			fc.Identifier = new OosVariable(t.val); 
		} else SynErr(80);
		CALLLIST(out cl);
		fc.Children.AddRange(cl.getList()); 
	}

	void SQFCALL(out BaseLangObject bloOut) {
		var obj = new OosSqfCall(); bloOut = (BaseLangObject)obj; BaseLangObject blo; 
		Expect(53);
		if (la.kind == 27) {
			Get();
			while (StartOf(6)) {
				EXPRESSION(out blo);
				obj.addChild(blo); 
			}
			Expect(28);
		}
		Expect(5);
		obj.InstructionName = t.val; obj.markEnd(); 
		if (la.kind == 27) {
			Get();
			while (StartOf(6)) {
				EXPRESSION(out blo);
				obj.addChild(blo); 
			}
			Expect(28);
		}
	}

	void NATIVESINGLE(out BaseLangObject bloOut) {
		var obj = new OosNative(); bloOut = (BaseLangObject)obj; 
		Expect(66);
		Expect(27);
		Get();
		obj.nativeCode += t.val; 
		while (StartOf(7)) {
			Get();
			obj.nativeCode += " " + t.val; 
		}
		Expect(28);
	}

	void EXPRESSION_HELPER(out BaseLangObject bloOut) {
		var e = new OosExpression(); bloOut = (BaseLangObject)e; BaseLangObject blo; ExpressionOperator eo; 
		if (la.kind == 45) {
			Get();
			e.Negate = true; 
		}
		EXPRESSION_SINGLE(out blo);
		e.LInstruction = blo; 
		while (StartOf(8)) {
			EXPRESSION_OPERATOR(out eo);
			e.Op = eo; 
			EXPRESSION(out blo);
			e.RInstruction = blo; 
		}
	}

	void LOCALVARIABLE(out BaseLangObject bloOut) {
		var lv = new OosLocalVariable(); bloOut = (BaseLangObject)lv; BaseLangObject blo; 
		Expect(33);
		Expect(7);
		lv.Name = t.val; 
		if (la.kind == 21) {
			Get();
			EXPRESSION(out blo);
			lv.Value = blo; 
		}
	}

	void ASSIGNMENT(out BaseLangObject bloOut) {
		string v1 = ""; string v2 = ""; bloOut = null; BaseLangObject blo; 
		if (la.kind == 5) {
			Get();
			v1 = t.val; 
		} else if (la.kind == 7) {
			Get();
			v1 = t.val; 
		} else SynErr(81);
		if (la.kind == 11) {
			Get();
			Expect(1);
			v2 = t.val; 
			Expect(12);
		}
		if (la.kind == 46 || la.kind == 47) {
			var obj = new OosQuickAssignment(); obj.Variable = new OosVariable(v1); obj.ArrayPosition = v2; bloOut = (BaseLangObject)obj; 
			if (la.kind == 46) {
				Get();
				obj.QuickAssignmentType = QuickAssignmentTypes.PlusPlus; 
			} else {
				Get();
				obj.QuickAssignmentType = QuickAssignmentTypes.MinusMinus; 
			}
		} else if (StartOf(9)) {
			var obj = new OosVariableAssignment(); obj.Variable = new OosVariable(v1); obj.ArrayPosition = v2; bloOut = (BaseLangObject)obj; AssignmentOperators ao; 
			ASSIGNMENTOPERATORS(out ao);
			obj.AssignmentOperator = ao; 
			EXPRESSION(out blo);
			obj.Value = blo; 
		} else SynErr(82);
	}

	void CALLLIST(out ListBaseLangObject l) {
		l = new ListBaseLangObject(); BaseLangObject blo; 
		Expect(27);
		if (StartOf(6)) {
			EXPRESSION(out blo);
			l.Add(blo); 
			while (la.kind == 10) {
				Get();
				EXPRESSION(out blo);
				l.Add(blo); 
			}
		}
		Expect(28);
	}

	void TYPEOF(out BaseLangObject bloOut) {
		var to = new OosTypeOf(); bloOut = (BaseLangObject)to; BaseLangObject blo; 
		if (la.kind == 48) {
			Get();
		} else if (la.kind == 49) {
			Get();
		} else SynErr(83);
		Expect(27);
		EXPRESSION(out blo);
		to.Argument = blo; 
		Expect(28);
	}

	void INSTANCEOF(out BaseLangObject bloOut) {
		var io = new OosInstanceOf(); bloOut = (BaseLangObject)io; BaseLangObject blo; 
		EXPRESSION(out blo);
		io.LArgument = blo; 
		if (la.kind == 50) {
			Get();
		} else if (la.kind == 51) {
			Get();
		} else SynErr(84);
		EXPRESSION(out blo);
		io.RArgument = blo; 
	}

	void FORLOOP(out BaseLangObject bloOut) {
		var fl = new OosForLoop(); bloOut = (BaseLangObject)fl; BaseLangObject blo; 
		Expect(54);
		Expect(27);
		if (la.kind == 5 || la.kind == 7 || la.kind == 33) {
			if (la.kind == 5 || la.kind == 7) {
				ASSIGNMENT(out blo);
				fl.Arg1 = blo; 
			} else {
				LOCALVARIABLE(out blo);
				fl.Arg1 = blo; 
			}
		}
		Expect(8);
		EXPRESSION(out blo);
		fl.Arg2 = blo; 
		Expect(8);
		if (la.kind == 5 || la.kind == 7) {
			ASSIGNMENT(out blo);
			fl.Arg3 = blo; 
		}
		Expect(28);
		CODEBODY(bloOut);
	}

	void WHILELOOP(out BaseLangObject bloOut) {
		var wl = new OosWhileLoop(); bloOut = (BaseLangObject)wl; BaseLangObject blo; 
		Expect(55);
		Expect(27);
		EXPRESSION(out blo);
		wl.Expression = blo; 
		Expect(28);
		CODEBODY(bloOut);
	}

	void IFELSE(out BaseLangObject bloOut) {
		var ie = new OosIfElse(); bloOut = (BaseLangObject)ie; BaseLangObject blo; 
		Expect(68);
		Expect(27);
		EXPRESSION(out blo);
		ie.Expression = blo; 
		Expect(28);
		CODEBODY(bloOut);
		ie.markEnd(); 
		if (la.kind == 69) {
			Get();
			CODEBODY(bloOut);
		}
	}

	void TRYCATCH(out BaseLangObject bloOut) {
		var tc = new OosTryCatch(); bloOut = (BaseLangObject)tc; 
		Expect(59);
		CODEBODY(bloOut);
		Expect(60);
		tc.markEnd(); 
		Expect(27);
		Expect(5);
		tc.CatchVariable = new OosVariable(t.val); 
		Expect(28);
		CODEBODY(bloOut);
	}

	void SWITCH(out BaseLangObject bloOut) {
		var s = new OosSwitch(); bloOut = (BaseLangObject)s; BaseLangObject blo; 
		Expect(58);
		Expect(27);
		EXPRESSION(out blo);
		s.Expression = blo; 
		Expect(28);
		Expect(23);
		while (la.kind == 56 || la.kind == 57) {
			CASE(out blo);
			s.addChild(blo); 
		}
		Expect(24);
	}

	void NATIVEMULTI(out BaseLangObject bloOut) {
		var obj = new OosNative(); bloOut = (BaseLangObject)obj; 
		Expect(64);
		Get();
		obj.nativeCode += t.val; 
		while (StartOf(10)) {
			Get();
			obj.nativeCode += " " + t.val; 
		}
		Expect(65);
	}

	void THROWINSTRUCTION(out BaseLangObject bloOut) {
		var tr = new OosThrow(); bloOut = (BaseLangObject)tr; BaseLangObject blo; 
		Expect(61);
		EXPRESSION(out blo);
		tr.Expression = blo; 
	}

	void RETURNINSTRUCTION(out BaseLangObject bloOut) {
		var tr = new OosReturn(); bloOut = (BaseLangObject)tr; BaseLangObject blo; 
		Expect(62);
		EXPRESSION(out blo);
		tr.Expression = blo; 
	}

	void BREAKINSTRUCTION(out BaseLangObject bloOut) {
		var b = new OosBreak(); bloOut = (BaseLangObject)b; 
		Expect(67);
	}

	void CODEBODY(BaseLangObject bloOut) {
		BaseLangObject blo; 
		if (StartOf(3)) {
			if (StartOf(4)) {
				CODEINSTRUCTION(out blo);
				bloOut.addChild(blo); 
				Expect(8);
				while (la.kind == 8) {
					Get();
				}
			} else {
				CODEINSTRUCTION_OPTIONALSC(out blo);
				bloOut.addChild(blo); 
				if (la.kind == 8) {
					Get();
					while (la.kind == 8) {
						Get();
					}
				}
			}
		} else if (la.kind == 23) {
			Get();
			while (StartOf(3)) {
				if (StartOf(4)) {
					CODEINSTRUCTION(out blo);
					bloOut.addChild(blo); 
					Expect(8);
					while (la.kind == 8) {
						Get();
					}
				} else {
					CODEINSTRUCTION_OPTIONALSC(out blo);
					bloOut.addChild(blo); 
					if (la.kind == 8) {
						Get();
						while (la.kind == 8) {
							Get();
						}
					}
				}
			}
			Expect(24);
		} else SynErr(85);
	}

	void CASE(out BaseLangObject bloOut) {
		var c = new OosCase(); bloOut = (BaseLangObject)c; BaseLangObject blo; 
		if (la.kind == 56) {
			Get();
			VALUE(out blo);
			c.Value = blo; 
		} else if (la.kind == 57) {
			Get();
		} else SynErr(86);
		Expect(26);
		CODEBODY(bloOut);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_T,_T,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_T, _x,_T,_x,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _T,_T,_x,_x, _T,_T,_T,_T, _x,_x,_T,_T, _x,_T,_T,_T, _T,_x,_T,_T, _T,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_T, _x,_T,_x,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _T,_T,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_T,_T, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_T, _x,_T,_x,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_T,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_x,_T, _x,_T,_x,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_T,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x,_T,_T, _T,_T,_T,_x}

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
			case 1: s = "UINTEGER expected"; break;
			case 2: s = "UDOUBLE expected"; break;
			case 3: s = "INTEGER expected"; break;
			case 4: s = "DOUBLE expected"; break;
			case 5: s = "FQIDENT expected"; break;
			case 6: s = "IDENT expected"; break;
			case 7: s = "LOCALIDENT expected"; break;
			case 8: s = "LINETERMINATOR expected"; break;
			case 9: s = "STRING expected"; break;
			case 10: s = "COMMA expected"; break;
			case 11: s = "\"[\" expected"; break;
			case 12: s = "\"]\" expected"; break;
			case 13: s = "\"true\" expected"; break;
			case 14: s = "\"false\" expected"; break;
			case 15: s = "\"private\" expected"; break;
			case 16: s = "\"public\" expected"; break;
			case 17: s = "\"+=\" expected"; break;
			case 18: s = "\"-=\" expected"; break;
			case 19: s = "\"*=\" expected"; break;
			case 20: s = "\"/=\" expected"; break;
			case 21: s = "\"=\" expected"; break;
			case 22: s = "\"namespace\" expected"; break;
			case 23: s = "\"{\" expected"; break;
			case 24: s = "\"}\" expected"; break;
			case 25: s = "\"class\" expected"; break;
			case 26: s = "\":\" expected"; break;
			case 27: s = "\"(\" expected"; break;
			case 28: s = "\")\" expected"; break;
			case 29: s = "\"constructor\" expected"; break;
			case 30: s = "\"function\" expected"; break;
			case 31: s = "\"override\" expected"; break;
			case 32: s = "\"static\" expected"; break;
			case 33: s = "\"auto\" expected"; break;
			case 34: s = "\"&\" expected"; break;
			case 35: s = "\"|\" expected"; break;
			case 36: s = "\"==\" expected"; break;
			case 37: s = "\"+\" expected"; break;
			case 38: s = "\"-\" expected"; break;
			case 39: s = "\"*\" expected"; break;
			case 40: s = "\"/\" expected"; break;
			case 41: s = "\">\" expected"; break;
			case 42: s = "\">=\" expected"; break;
			case 43: s = "\"<\" expected"; break;
			case 44: s = "\"<=\" expected"; break;
			case 45: s = "\"!\" expected"; break;
			case 46: s = "\"++\" expected"; break;
			case 47: s = "\"--\" expected"; break;
			case 48: s = "\"typeof\" expected"; break;
			case 49: s = "\"typeOf\" expected"; break;
			case 50: s = "\"instanceof\" expected"; break;
			case 51: s = "\"instanceOf\" expected"; break;
			case 52: s = "\"new\" expected"; break;
			case 53: s = "\"SQF\" expected"; break;
			case 54: s = "\"for\" expected"; break;
			case 55: s = "\"while\" expected"; break;
			case 56: s = "\"case\" expected"; break;
			case 57: s = "\"default\" expected"; break;
			case 58: s = "\"switch\" expected"; break;
			case 59: s = "\"try\" expected"; break;
			case 60: s = "\"catch\" expected"; break;
			case 61: s = "\"throw\" expected"; break;
			case 62: s = "\"return\" expected"; break;
			case 63: s = "\"isset\" expected"; break;
			case 64: s = "\"startnative\" expected"; break;
			case 65: s = "\"endnative\" expected"; break;
			case 66: s = "\"native\" expected"; break;
			case 67: s = "\"break\" expected"; break;
			case 68: s = "\"if\" expected"; break;
			case 69: s = "\"else\" expected"; break;
			case 70: s = "??? expected"; break;
			case 71: s = "invalid SCALAR"; break;
			case 72: s = "invalid VALUE"; break;
			case 73: s = "invalid ENCAPSULATION"; break;
			case 74: s = "invalid ASSIGNMENTOPERATORS"; break;
			case 75: s = "invalid CODEINSTRUCTION"; break;
			case 76: s = "invalid CODEINSTRUCTION_OPTIONALSC"; break;
			case 77: s = "invalid EXPRESSION"; break;
			case 78: s = "invalid EXPRESSION_OPERATOR"; break;
			case 79: s = "invalid EXPRESSION_SINGLE"; break;
			case 80: s = "invalid FUNCTIONCALL"; break;
			case 81: s = "invalid ASSIGNMENT"; break;
			case 82: s = "invalid ASSIGNMENT"; break;
			case 83: s = "invalid TYPEOF"; break;
			case 84: s = "invalid INSTANCEOF"; break;
			case 85: s = "invalid CODEBODY"; break;
			case 86: s = "invalid CASE"; break;

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
