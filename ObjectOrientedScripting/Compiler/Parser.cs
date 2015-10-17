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
	public const int _T_SQUAREBRACKETOPEN = 9;
	public const int _T_SQUAREBRACKETCLOSE = 10;
	public const int _T_CODEBRACKETOPEN = 11;
	public const int _T_CODEBRACKETCLOSE = 12;
	public const int _T_INSTANCEACCESS = 13;
	public const int _T_COMMA = 14;
	public const int _T_STATICCASTOPERATOR = 15;
	public const int _T_DYNAMICCASTOPERATOR = 16;
	public const int maxT = 57;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	private Base baseObject;
	public Base BaseObject { get { return baseObject; } }
	
	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
		baseObject = null;
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
	bool peekString(int count, params string[] values)
	{
		Token t = la;
        for(; count > 0; count --)
            t = scanner.Peek();
		foreach(var it in values)
		{
			if(t.val == it)
			{
				scanner.ResetPeek();
				return true;
			}
		}
        scanner.ResetPeek();
		return false;
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
		outObj = new Ident(parent, t.val, t.line, t.col); 
	}

	void IDENTACCESS(out pBaseLangObject outObj, pBaseLangObject parent) {
		pBaseLangObject blo; pBaseLangObject ident; outObj = null; 
		if (la.kind == 15 || la.kind == 16) {
			CAST(out outObj, parent);
		}
		IDENT(out ident, parent);
		if(outObj == null) outObj = ident; else outObj.addChild(ident); 
		if (la.kind == 6 || la.kind == 7 || la.kind == 9) {
			if (la.kind == 7) {
				BODY_FUNCTIONCALL(out blo, ident);
				((Ident)ident).addChild(blo); 
			} else if (la.kind == 9) {
				BODY_ARRAYACCESS(out blo, ident);
				((Ident)ident).addChild(blo); 
			} else {
				BODY_ASSIGNMENT(out blo, ident);
				((Ident)ident).addChild(blo); 
			}
		}
		if (la.kind == 13) {
			Get();
			IDENTACCESS(out blo, ident);
			((Ident)ident).addChild(blo); 
		}
	}

	void CAST(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Cast(parent); outObj = obj; VarType vt; pBaseLangObject ident; 
		if (la.kind == 15) {
			Get();
			obj.isStaticCast = true; 
			if (StartOf(1)) {
				VARTYPE(out vt);
				obj.varType = new VarTypeObject(vt); 
			} else if (la.kind == 3) {
				IDENT(out ident, obj);
				obj.varType = new VarTypeObject((Ident)ident); 
			} else SynErr(58);
			Expect(15);
		} else if (la.kind == 16) {
			Get();
			obj.isStaticCast = false; 
			if (StartOf(1)) {
				VARTYPE(out vt);
				obj.varType = new VarTypeObject(vt); 
			} else if (la.kind == 3) {
				IDENT(out ident, obj);
				obj.varType = new VarTypeObject((Ident)ident); 
			} else SynErr(59);
			Expect(16);
		} else SynErr(60);
	}

	void BODY_FUNCTIONCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new FunctionCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(7);
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 14) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(8);
	}

	void BODY_ARRAYACCESS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new ArrayAccess(parent); outObj = obj; pBaseLangObject blo; 
		Expect(9);
		VALUE(out blo, obj);
		obj.addChild(blo); 
		Expect(10);
	}

	void BODY_ASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		Expect(6);
		obj.operation = t.val; 
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
		} else if (la.kind == 11) {
			OP_NEWARRAY(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(61);
	}

	void VARTYPE(out VarType e) {
		e = VarType.Void; 
		switch (la.kind) {
		case 20: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 21: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 22: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 23: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 24: {
			Get();
			e = VarType.Bool; 
			break;
		}
		case 25: {
			Get();
			e = VarType.String; 
			break;
		}
		case 26: {
			Get();
			e = VarType.Object; 
			break;
		}
		default: SynErr(62); break;
		}
		if (la.kind == 9) {
			Get();
			Expect(10);
			switch(e)
			{
			    case VarType.Scalar:
			        e = VarType.ScalarArray;
			        break;
			    case VarType.Bool:
			        e = VarType.BoolArray;
			        break;
			    case VarType.String:
			        e = VarType.StringArray;
			        break;
			    default:
			        SemErr("Cannot Arrayify VarTypes which are not string/scalar/bool");
			        break;
			} 
		}
	}

	void ENCAPSULATION(out Encapsulation e) {
		e = Encapsulation.NA; 
		if (la.kind == 17) {
			Get();
			e = Encapsulation.Public; 
		} else if (la.kind == 18) {
			Get();
			e = Encapsulation.Private; 
		} else if (la.kind == 19) {
			Get();
			e = Encapsulation.Protected; 
		} else SynErr(63);
	}

	void BOOLEAN(out bool flag) {
		flag = la.val == "true"; Get(); return; 
		if (la.kind == 27) {
			Get();
			flag = true; 
		} else if (la.kind == 28) {
			Get();
			flag = false; 
		} else SynErr(64);
	}

	void VALUE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Value(parent); outObj = obj; outObj = obj; pBaseLangObject blo; bool flag; 
		if (la.kind == 2) {
			Get();
			obj.varType = VarType.String; obj.value = t.val; 
		} else if (la.kind == 1) {
			Get();
			obj.varType = VarType.Scalar; obj.value = t.val; 
		} else if (la.val == "true" || la.val == "false") {
			BOOLEAN(out flag);
			obj.varType = VarType.Bool; obj.value = (flag ? "true" : "false"); 
		} else if (la.kind == 27 || la.kind == 28) {
			BOOLEAN(out flag);
			obj.varType = VarType.Bool; obj.value = (flag ? "true" : "false"); 
		} else SynErr(65);
	}

	void EXPRESSION_HELPER(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Expression(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; 
		if (la.kind == 29) {
			Get();
			obj.negate = true; 
		}
		if (la.kind == 43) {
			OP_NEWINSTANCE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.val == "true" || la.val == "false" ) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (StartOf(3)) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.kind == 3 || la.kind == 15 || la.kind == 16) {
			IDENTACCESS(out blo, obj);
			obj.lExpression = blo; 
			if (la.kind == 49) {
				OP_INSTANCEOF(out blo, obj, blo);
				obj.lExpression = blo; 
			}
		} else if (la.kind == 56) {
			OP_SQFCALL(out blo, obj);
			obj.lExpression = blo; 
		} else SynErr(66);
		while (la.kind == 5 || la.kind == 30 || la.kind == 31) {
			if (la.kind == 30) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 31) {
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
		Expect(43);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		BODY_FUNCTIONCALL(out blo2, blo);
		blo.addChild(blo2); 
	}

	void OP_INSTANCEOF(out pBaseLangObject outObj, pBaseLangObject parent, pBaseLangObject identAccess) {
		var obj = new InstanceOf(parent); outObj = obj; pBaseLangObject blo; obj.LIdent = identAccess; identAccess.Parent = obj; 
		Expect(49);
		IDENT(out blo, obj);
		obj.RIdent = (Ident)blo; 
	}

	void OP_SQFCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new SqfCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(56);
		if (la.kind == 7) {
			Get();
			while (StartOf(2)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(8);
		}
		IDENT(out blo, outObj);
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
		} else if (StartOf(4)) {
			EXPRESSION_HELPER(out outObj, parent);
		} else SynErr(67);
	}

	void OOS() {
		var obj = new Base(); baseObject = obj; pBaseLangObject blo; 
		while (StartOf(5)) {
			if (la.kind == 33) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 34) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 36) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (StartOf(6)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(68);
			}
		}
	}

	void NAMESPACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Namespace(parent); outObj = obj; pBaseLangObject blo; 
		Expect(33);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(11);
		while (StartOf(5)) {
			if (la.kind == 33) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 34) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 36) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
					TERMINATOR();
				} else if (StartOf(6)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(69);
			}
		}
		Expect(12);
	}

	void CLASS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosClass(parent); outObj = obj; pBaseLangObject blo; Encapsulation e = Encapsulation.Private; bool hasConstructor = false; 
		Expect(34);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 35) {
			Get();
			IDENT(out blo, obj);
			obj.addParentClass((Ident)blo); 
			while (la.kind == 14) {
				Get();
				IDENT(out blo, obj);
				obj.addParentClass((Ident)blo); 
			}
		}
		obj.markEnd(); 
		Expect(11);
		while (StartOf(7)) {
			e = Encapsulation.Private; 
			if (StartOf(8)) {
				if (la.kind == 17 || la.kind == 18 || la.kind == 19) {
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
			} else if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN) && la.val.Equals(obj.Name.OriginalValue) ) {
				CONSTRUCTOR(out blo, obj, e);
				obj.addChild(blo); hasConstructor = true; 
			} else if (StartOf(6)) {
				FUNCTION(out blo, obj, e);
				obj.addChild(blo); 
			} else SynErr(70);
		}
		Expect(12);
		if(!hasConstructor) {
		            var constructor = new Function(obj);
		            try
		            {
		              constructor.Name = obj.Name;
		            }
		            catch (Exception ex)
		            {
		              SemErr(ex.Message);
		            }
		            constructor.varType = new VarTypeObject(obj.Name); 
		            constructor.markArgListEnd();
		            obj.addChild(constructor);
		        } 
	}

	void INTERFACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosInterface(parent); outObj = obj; pBaseLangObject blo; 
		Expect(36);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(11);
		while (StartOf(6)) {
			VFUNCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(12);
	}

	void NEWVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.col, la.line); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(1)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (la.kind == 3 || la.kind == 38) {
			bool isStrict = false; 
			if (la.kind == 38) {
				Get();
				isStrict = true; 
			}
			IDENT(out blo, obj);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
		} else SynErr(71);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
	}

	void FUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(1)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (la.kind == 37) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else if (la.kind == 3 || la.kind == 38) {
			bool isStrict = false; 
			if (la.kind == 38) {
				Get();
				isStrict = true; 
			}
			IDENT(out blo, obj);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
		} else SynErr(72);
		if (la.kind == 39) {
			Get();
			obj.Override = true; 
		}
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(7);
		if (StartOf(9)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 14) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(8);
		obj.markArgListEnd(); 
		Expect(11);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(12);
	}

	void CONSTRUCTOR(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent); obj.varType = new VarTypeObject(((oosClass)parent).Name, true); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(7);
		if (StartOf(9)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 14) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(8);
		obj.markArgListEnd(); 
		Expect(11);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(12);
	}

	void VFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VirtualFunction(parent); outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(1)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (la.kind == 37) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else if (la.kind == 3 || la.kind == 38) {
			bool isStrict = false; 
			if (la.kind == 38) {
				Get();
				isStrict = true; 
			}
			IDENT(out blo, obj);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
		} else SynErr(73);
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		Expect(7);
		if (StartOf(11)) {
			if (StartOf(1)) {
				VARTYPE(out v);
				obj.argTypes.Add(new VarTypeObject(v)); 
			} else {
				IDENT(out blo, obj);
				obj.argTypes.Add(new VarTypeObject((Ident)blo)); 
			}
			while (la.kind == 14) {
				Get();
				if (StartOf(1)) {
					VARTYPE(out v);
					obj.argTypes.Add(new VarTypeObject(v)); 
				} else if (la.kind == 3) {
					IDENT(out blo, obj);
					obj.argTypes.Add(new VarTypeObject((Ident)blo)); 
				} else SynErr(74);
			}
		}
		Expect(8);
		TERMINATOR();
	}

	void CODEINSTRUCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (StartOf(12)) {
			CODEINSTRUCTION_SC(out outObj, parent);
			TERMINATOR();
		} else if (StartOf(13)) {
			CODEINSTRUCTION_NSC(out outObj, parent);
		} else SynErr(75);
	}

	void AUTOVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.col, la.line); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		Expect(40);
		obj.varType = new VarTypeObject(VarType.Auto); 
		IDENT(out blo, obj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
	}

	void CODEINSTRUCTION_SC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; pBaseLangObject blo; 
		if (la.kind == 50) {
			OP_THROW(out outObj, parent);
		} else if (la.kind == 51) {
			OP_RETURN(out outObj, parent);
		} else if ((peekString(0, "scalar", "int", "double", "float", "bool", "string", "object") && peekCompare(-1, _T_IDENT)) || peekCompare(_T_IDENT, _T_IDENT) ) {
			NEWVARIABLE(out outObj, parent);
			if (la.kind == 6) {
				BODY_ASSIGNMENT(out blo, outObj);
				outObj.addChild(blo); 
			}
		} else if (la.kind == 40) {
			AUTOVARIABLE(out outObj, parent);
			BODY_ASSIGNMENT(out blo, outObj);
			outObj.addChild(blo); 
		} else if (StartOf(2)) {
			EXPRESSION(out outObj, parent);
		} else SynErr(76);
	}

	void OP_THROW(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Throw(parent); outObj = obj; pBaseLangObject blo; 
		Expect(50);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void OP_RETURN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Return(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; 
		Expect(51);
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
		}
	}

	void CODEINSTRUCTION_NSC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (la.kind == 41) {
			OP_FOR(out outObj, parent);
		} else if (la.kind == 42) {
			OP_WHILE(out outObj, parent);
		} else if (la.kind == 44) {
			OP_IFELSE(out outObj, parent);
		} else if (la.kind == 52) {
			OP_SWITCH(out outObj, parent);
		} else if (la.kind == 46) {
			OP_TRYCATCH(out outObj, parent);
		} else SynErr(77);
	}

	void OP_FOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new For(parent); outObj = obj; pBaseLangObject blo; 
		Expect(41);
		Expect(7);
		if (StartOf(12)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg1 = blo; 
		}
		TERMINATOR();
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.forArg2 = blo; 
		}
		TERMINATOR();
		if (StartOf(12)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg3 = blo; 
		}
		Expect(8);
		if (la.kind == 11) {
			Get();
			while (StartOf(14)) {
				if (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(12);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(78);
	}

	void OP_WHILE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new While(parent); outObj = obj; pBaseLangObject blo; 
		Expect(42);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		if (la.kind == 11) {
			Get();
			while (StartOf(14)) {
				if (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(12);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(79);
	}

	void OP_IFELSE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new IfElse(parent); outObj = obj; pBaseLangObject blo; 
		Expect(44);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		if (la.kind == 11) {
			Get();
			while (StartOf(10)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(12);
		} else if (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(80);
		if (la.kind == 45) {
			Get();
			obj.markIfEnd(); 
			if (la.kind == 11) {
				Get();
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				}
				Expect(12);
			} else if (StartOf(10)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(81);
		}
	}

	void OP_SWITCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Switch(parent); Case caseObj; outObj = obj; pBaseLangObject blo; 
		Expect(52);
		Expect(7);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(8);
		Expect(11);
		while (la.kind == 53 || la.kind == 54 || la.kind == 55) {
			if (la.kind == 53) {
				Get();
				caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); 
				EXPRESSION(out blo, obj);
				caseObj.expression = blo; 
				Expect(35);
				while (la.kind == 53) {
					Get();
					EXPRESSION(out blo, obj);
					caseObj.addChild(blo); 
					Expect(35);
				}
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 48) {
					OP_BREAK(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 50) {
					OP_THROW(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 51) {
					OP_RETURN(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else SynErr(82);
			} else {
				if (la.kind == 54) {
					Get();
					caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); caseObj.expression = null; 
					Expect(35);
				} else {
					Get();
					caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); caseObj.expression = null; 
				}
				while (StartOf(10)) {
					CODEINSTRUCTION(out blo, obj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 48) {
					OP_BREAK(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 50) {
					OP_THROW(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else if (la.kind == 51) {
					OP_RETURN(out blo, obj);
					caseObj.endOfCase = blo; 
					TERMINATOR();
				} else SynErr(83);
			}
		}
		Expect(12);
	}

	void OP_TRYCATCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new TryCatch(parent); outObj = obj; pBaseLangObject blo; 
		Expect(46);
		Expect(11);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(12);
		Expect(47);
		Expect(7);
		NEWVARIABLE(out blo, obj);
		obj.variable = blo; 
		Expect(8);
		obj.markIfEnd(); 
		Expect(11);
		while (StartOf(10)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(12);
	}

	void OP_NEWARRAY(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewArray(parent); outObj = obj; pBaseLangObject blo; 
		Expect(11);
		if (StartOf(2)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 14) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(12);
	}

	void OP_BREAK(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Break(parent); outObj = obj; pBaseLangObject blo; 
		Expect(48);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _T,_T,_T,_T, _T,_x,_T,_x, _x,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _T,_x,_x,_T, _x,_x,_x,_x, _x,_x,_T,_T, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _T,_x,_T,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_T, _T,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _T,_T,_T,_T, _T,_x,_T,_x, _T,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x}

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
			case 9: s = "T_SQUAREBRACKETOPEN expected"; break;
			case 10: s = "T_SQUAREBRACKETCLOSE expected"; break;
			case 11: s = "T_CODEBRACKETOPEN expected"; break;
			case 12: s = "T_CODEBRACKETCLOSE expected"; break;
			case 13: s = "T_INSTANCEACCESS expected"; break;
			case 14: s = "T_COMMA expected"; break;
			case 15: s = "T_STATICCASTOPERATOR expected"; break;
			case 16: s = "T_DYNAMICCASTOPERATOR expected"; break;
			case 17: s = "\"public\" expected"; break;
			case 18: s = "\"private\" expected"; break;
			case 19: s = "\"protected\" expected"; break;
			case 20: s = "\"scalar\" expected"; break;
			case 21: s = "\"int\" expected"; break;
			case 22: s = "\"double\" expected"; break;
			case 23: s = "\"float\" expected"; break;
			case 24: s = "\"bool\" expected"; break;
			case 25: s = "\"string\" expected"; break;
			case 26: s = "\"object\" expected"; break;
			case 27: s = "\"true\" expected"; break;
			case 28: s = "\"false\" expected"; break;
			case 29: s = "\"!\" expected"; break;
			case 30: s = "\"++\" expected"; break;
			case 31: s = "\"--\" expected"; break;
			case 32: s = "\"static\" expected"; break;
			case 33: s = "\"namespace\" expected"; break;
			case 34: s = "\"class\" expected"; break;
			case 35: s = "\":\" expected"; break;
			case 36: s = "\"interface\" expected"; break;
			case 37: s = "\"void\" expected"; break;
			case 38: s = "\"strict\" expected"; break;
			case 39: s = "\"override\" expected"; break;
			case 40: s = "\"auto\" expected"; break;
			case 41: s = "\"for\" expected"; break;
			case 42: s = "\"while\" expected"; break;
			case 43: s = "\"new\" expected"; break;
			case 44: s = "\"if\" expected"; break;
			case 45: s = "\"else\" expected"; break;
			case 46: s = "\"try\" expected"; break;
			case 47: s = "\"catch\" expected"; break;
			case 48: s = "\"break\" expected"; break;
			case 49: s = "\"is\" expected"; break;
			case 50: s = "\"throw\" expected"; break;
			case 51: s = "\"return\" expected"; break;
			case 52: s = "\"switch\" expected"; break;
			case 53: s = "\"case\" expected"; break;
			case 54: s = "\"default\" expected"; break;
			case 55: s = "\"default:\" expected"; break;
			case 56: s = "\"SQF\" expected"; break;
			case 57: s = "??? expected"; break;
			case 58: s = "invalid CAST"; break;
			case 59: s = "invalid CAST"; break;
			case 60: s = "invalid CAST"; break;
			case 61: s = "invalid BODY_ASSIGNMENT"; break;
			case 62: s = "invalid VARTYPE"; break;
			case 63: s = "invalid ENCAPSULATION"; break;
			case 64: s = "invalid BOOLEAN"; break;
			case 65: s = "invalid VALUE"; break;
			case 66: s = "invalid EXPRESSION_HELPER"; break;
			case 67: s = "invalid EXPRESSION"; break;
			case 68: s = "invalid OOS"; break;
			case 69: s = "invalid NAMESPACE"; break;
			case 70: s = "invalid CLASS"; break;
			case 71: s = "invalid NEWVARIABLE"; break;
			case 72: s = "invalid FUNCTION"; break;
			case 73: s = "invalid VFUNCTION"; break;
			case 74: s = "invalid VFUNCTION"; break;
			case 75: s = "invalid CODEINSTRUCTION"; break;
			case 76: s = "invalid CODEINSTRUCTION_SC"; break;
			case 77: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 78: s = "invalid OP_FOR"; break;
			case 79: s = "invalid OP_WHILE"; break;
			case 80: s = "invalid OP_IFELSE"; break;
			case 81: s = "invalid OP_IFELSE"; break;
			case 82: s = "invalid OP_SWITCH"; break;
			case 83: s = "invalid OP_SWITCH"; break;

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
