using Compiler.OOS_LanguageObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System;



public class Parser {
	public const int _EOF = 0;
	public const int _T_SCALAR = 1;
	public const int _T_STRING = 2;
	public const int _T_IDENT = 3;
	public const int _T_TERMINATOR = 4;
	public const int _T_EXPOP1 = 5;
	public const int _T_EXPOP2 = 6;
	public const int _T_EXPOP3 = 7;
	public const int _T_OTHEROP = 8;
	public const int _T_ASSIGNMENTCHAR = 9;
	public const int _T_EXTENDEDASSIGNMENTCHARS = 10;
	public const int _T_FASTASSIGNMENTCHARS = 11;
	public const int _T_ROUNDBRACKETOPEN = 12;
	public const int _T_ROUNDBRACKETCLOSE = 13;
	public const int _T_SQUAREBRACKETOPEN = 14;
	public const int _T_SQUAREBRACKETCLOSE = 15;
	public const int _T_CODEBRACKETOPEN = 16;
	public const int _T_CODEBRACKETCLOSE = 17;
	public const int _T_INSTANCEACCESS = 18;
	public const int _T_NAMESPACEACCESS = 19;
	public const int _T_COMMA = 20;
	public const int _T_TEMPLATEOPEN = 21;
	public const int _T_TEMPLATECLOSE = 22;
	public const int _T_SLASH = 23;
	public const int _T_BACKSLASH = 24;
	public const int maxT = 92;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
    public string file;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	public Base BaseObject { get; set;}
	
	public Parser(Scanner scanner, string file) {
		this.scanner = scanner;
        this.file = file;
		errors = new Errors();
	}
	public static List<string> UsedFiles = new List<string>();
	
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
	void Warning (string msg) {
		errors.Warning(la.line, la.col, msg);
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

	
	void FNCARGLIST(pBaseLangObject obj) {
		pBaseLangObject blo;
		VarTypeObject vto;
		
		Expect(12);
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			NEWVARIABLE(out blo, obj, vto);
			obj.addChild(blo); 
			while (la.kind == 20) {
				Get();
				VARTYPE(out vto, obj);
				NEWVARIABLE(out blo, obj, vto);
				obj.addChild(blo); 
			}
		}
		Expect(13);
	}

	void VARTYPE(out VarTypeObject e, pBaseLangObject parent, bool identFlag = false) {
		e = null; 
		if (StartOf(2)) {
			switch (la.kind) {
			case 32: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
			case 33: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
			case 34: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
			case 35: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
			case 36: {
				Get();
				e = new VarTypeObject(VarType.Bool); 
				break;
			}
			case 37: {
				Get();
				e = new VarTypeObject(VarType.Bool); 
				break;
			}
			case 38: {
				Get();
				e = new VarTypeObject(VarType.ScalarArray); 
				break;
			}
			case 39: {
				Get();
				e = new VarTypeObject(VarType.ScalarArray); 
				break;
			}
			case 40: {
				Get();
				e = new VarTypeObject(VarType.ScalarArray); 
				break;
			}
			case 41: {
				Get();
				e = new VarTypeObject(VarType.ScalarArray); 
				break;
			}
			case 42: {
				Get();
				e = new VarTypeObject(VarType.BoolArray); 
				break;
			}
			case 43: {
				Get();
				e = new VarTypeObject(VarType.BoolArray); 
				break;
			}
			}
		} else if (la.kind == 3 || la.kind == 19 || la.kind == 21) {
			pBaseLangObject blo; 
			IDENTACCESS(out blo, parent, identFlag);
			e = new VarTypeObject((Ident)blo); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, ((Ident)blo).LastIdent);
				e.TemplateObject = te; 
			}
		} else SynErr(93);
	}

	void NEWVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, VarTypeObject vto, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.line, la.col, this.file);
		obj.encapsulation = e;
		outObj = obj;
		pBaseLangObject blo;
		obj.varType = vto;
		
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		if (la.kind == 9 || la.kind == 10 || la.kind == 11) {
			BODY_ASSIGNMENT(out blo, outObj);
			obj.addChild(blo); 
		}
	}

	void SPECIAL_NEWVARIABLEFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e, bool fVFncOnly = false, bool isInline = false) {
		outObj = null;
		pBaseLangObject blo;
		bool isAsync = false;
		bool isVirtual = false;
		VarTypeObject vto = null;
		
		if (StartOf(1)) {
			VARTYPE(out vto, parent);
			if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN)) {
				FUNCTION(out blo, parent, e, vto, false, false, false, isInline);
				outObj = blo;
				if(!((Function)blo).IsVirtual && fVFncOnly)
				{
				SemErr("Non-Virtual function on VirtualOnly class");
				} 
				
			} else if (la.kind == 3) {
				NEWVARIABLE(out blo, parent, vto, e);
				outObj = blo; 
				TERMINATOR();
			} else SynErr(94);
		} else if (la.kind == 25) {
			Get();
			vto = new VarTypeObject(VarType.Void); 
			FUNCTION(out blo, parent, e, vto, false, false, false, isInline);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 26) {
			Get();
			isAsync = true; 
			if (la.kind == 27) {
				if(isInline)
				{
				SemErr("virtual functions can't be inline");
				}
				
				Get();
				isVirtual = true; 
			}
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 25) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(95);
			FUNCTION(out blo, parent, e, vto, isAsync, isVirtual, false, isInline);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 27) {
			if(isInline)
			{
			SemErr("virtual functions can't be inline");
			}
			
			Get();
			isVirtual = true; 
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 25) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(96);
			FUNCTION(out blo, parent, e, vto, isAsync, isVirtual);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 28) {
			if(isInline)
			{
			SemErr("external functions can't be inline");
			}
			
			Get();
			if (la.kind == 26) {
				Get();
				isAsync = true; 
			}
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 25) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(97);
			FUNCTION(out blo, parent, e, vto, isAsync, false, true);
			outObj = blo; 
			TERMINATOR();
		} else SynErr(98);
	}

	void FUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e, VarTypeObject vto, bool isAsync = false, bool isVirtual = false, bool isExternal = false, bool isInline = false) {
		var obj = new Function(parent); outObj = obj;
		obj.encapsulation = e;
		obj.IsAsync = isAsync;
		obj.IsVirtual = isVirtual;
		obj.varType = vto;
		obj.IsExternal = isExternal;
		obj.IsInline = isInline;
		obj.IsThrowing = false;
		pBaseLangObject blo;
		if(isExternal && isVirtual) SemErr("External function is marked as virtual");
		if(e == Encapsulation.Static && isVirtual) SemErr("Static function is marked as virtual");
		
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		obj.markArgListEnd(); 
		if (la.kind == 68) {
			Get();
			obj.IsThrowing = true; 
		}
		if(!isExternal) { 
		Expect(16);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(17);
		} 
	}

	void TERMINATOR() {
		Expect(4);
		while (la.kind == 4) {
			Get();
		}
	}

	void TEMPLATE(out Template obj, pBaseLangObject parent) {
		obj = new Template(parent, t.line, t.col, this.file); VarTypeObject vto; 
		Expect(21);
		VARTYPE(out vto, obj);
		obj.vtoList.Add(vto); 
		while (la.kind == 20) {
			Get();
			VARTYPE(out vto, obj);
			obj.vtoList.Add(vto); 
		}
		Expect(22);
	}

	void IDENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		Expect(3);
		outObj = new Ident(parent, t.val, t.line, t.col, this.file); 
	}

	void IDENTACCESS(out pBaseLangObject outObj, pBaseLangObject parent, bool allowBody = true) {
		pBaseLangObject blo; pBaseLangObject ident; outObj = null; bool isGlobalIdent = false; 
		if (la.kind == 21) {
			CAST(out outObj, parent);
		}
		if (la.kind == 19) {
			Get();
			isGlobalIdent = true; 
		}
		IDENT(out ident, (outObj == null ? parent : outObj));
		try{ ((Ident)ident).IsGlobalIdentifier = isGlobalIdent; } catch (Exception ex) { SemErr(ex.Message); } if(outObj == null) outObj = ident; else outObj.addChild(ident); 
		if(allowBody) { 
		if (la.kind == 12 || la.kind == 14) {
			if (la.kind == 12) {
				BODY_FUNCTIONCALL(out blo, ident);
				((Ident)ident).addChild(blo); 
			} else {
				BODY_ARRAYACCESS(out blo, ident);
				((Ident)ident).addChild(blo); 
			}
		}
		} 
		if (la.kind == 18 || la.kind == 19) {
			if (la.kind == 18) {
				Get();
				((Ident)ident).Access = Ident.AccessType.Instance; 
			} else {
				Get();
				((Ident)ident).Access = Ident.AccessType.Namespace; 
			}
			IDENTACCESS(out blo, ident, allowBody);
			((Ident)ident).addChild(blo); 
		}
	}

	void CAST(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Cast(parent); outObj = obj; VarTypeObject vto; 
		Expect(21);
		VARTYPE(out vto, obj);
		obj.varType = vto; 
		Expect(22);
	}

	void BODY_FUNCTIONCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new FunctionCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(12);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 20) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(13);
	}

	void BODY_ARRAYACCESS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new ArrayAccess(parent); outObj = obj; pBaseLangObject blo; 
		Expect(14);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
		Expect(15);
	}

	void ENCAPSULATION(out Encapsulation e) {
		e = Encapsulation.NA; 
		if (la.kind == 29) {
			Get();
			e = Encapsulation.Public; 
		} else if (la.kind == 30) {
			Get();
			e = Encapsulation.Private; 
		} else if (la.kind == 31) {
			Get();
			e = Encapsulation.Protected; 
		} else SynErr(99);
	}

	void BOOLEAN(out bool flag) {
		flag = la.val == "true"; Get(); return; /*fix for weirdo coco bug ...*/ 
		if (la.kind == 44) {
			Get();
			flag = true; 
		} else if (la.kind == 45) {
			Get();
			flag = false; 
		} else SynErr(100);
	}

	void VALUE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Value(parent); outObj = obj; outObj = obj; bool flag; 
		if (la.kind == 2) {
			Get();
			obj.varType.varType = VarType.Object; obj.value = t.val; obj.varType.ident = new Ident(obj, "string", t.line, t.col, this.file); obj.varType.ident.IsGlobalIdentifier = true; 
		} else if (la.kind == 1) {
			Get();
			obj.varType.varType = VarType.Scalar; obj.value = t.val; 
		} else if (la.val == "true" || la.val == "false") {
			BOOLEAN(out flag);
			obj.varType.varType = VarType.Bool; obj.value = (flag ? "true" : "false"); 
		} else if (la.kind == 44 || la.kind == 45) {
			BOOLEAN(out flag);
			
		} else SynErr(101);
	}

	void EXPRESSION(out pBaseLangObject outObj, pBaseLangObject parent, bool negate = false, bool hasBrackets = false) {
		var obj = new Expression(parent, negate, hasBrackets, t.line, t.col, this.file); outObj = obj; pBaseLangObject blo; 
		EXPRESSION_HELPER(obj, out blo);
		int lastOpType = 3;
		var opList = new List<string>();
		var objList = new List<pBaseLangObject>();
		objList.Add(blo);
		List<Expression> previousExpressions = new List<Expression>();
		previousExpressions.Add(obj);
		if(!negate) {
		
		while (StartOf(5)) {
			string op = ""; int thisOpType = -1; 
			switch (la.kind) {
			case 5: {
				Get();
				op = t.val; thisOpType = 1; 
				break;
			}
			case 23: {
				Get();
				op = t.val; thisOpType = 1; 
				break;
			}
			case 21: {
				Get();
				op = t.val; thisOpType = 2; 
				break;
			}
			case 22: {
				Get();
				op = t.val; thisOpType = 2; 
				break;
			}
			case 6: {
				Get();
				op = t.val; thisOpType = 2; 
				break;
			}
			case 7: {
				Get();
				op = t.val; thisOpType = 3; 
				break;
			}
			}
			EXPRESSION_HELPER(obj, out blo);
			if(thisOpType != lastOpType)
			{
				if(thisOpType < lastOpType)
				{
					var expList = new Expression(previousExpressions.Last(), negate, hasBrackets, t.line, t.col, this.file);
					if (opList.Count > 0)
					{
						if (opList.Count > 1)
						{
							expList.expressionOperators.AddRange(opList.GetRange(0, opList.Count - 1));
						}
						previousExpressions.Last().expressionOperators.Add(opList[opList.Count - 1]);
					}
					previousExpressions.Last().expressionObjects.AddRange(objList.GetRange(0, objList.Count - 1));
					previousExpressions.Last().expressionObjects.Add(expList);
					previousExpressions.Add(expList);
					previousExpressions.Last().expressionObjects.Add(objList[objList.Count - 1]);
					opList.Clear();
					objList.Clear();
				}
				else
				{
					var expList = previousExpressions.Last();
					expList.expressionOperators.AddRange(opList);
					expList.expressionObjects.AddRange(objList);
					previousExpressions.RemoveAt(previousExpressions.Count - 1);
					opList.Clear();
					objList.Clear();
				}
			}
			opList.Add(op);
			objList.Add(blo);
			lastOpType = thisOpType;
			
		}
		}
		previousExpressions.Last().expressionOperators.AddRange(opList);
		previousExpressions.Last().expressionObjects.AddRange(objList);
		
	}

	void EXPRESSION_HELPER(Expression obj, out pBaseLangObject blo) {
		blo = null; 
		if (la.kind == 12) {
			Get();
			EXPRESSION(out blo, obj, false);
			Expect(13);
		} else if (la.kind == 46) {
			Get();
			EXPRESSION(out blo, obj, true);
		} else if (la.kind == 77) {
			OP_NEWINSTANCE(out blo, obj);
		} else if (la.val == "true" || la.val == "false" ) {
			VALUE(out blo, obj);
		} else if (StartOf(6)) {
			VALUE(out blo, obj);
		} else if (la.kind == 3 || la.kind == 19 || la.kind == 21) {
			IDENTACCESS(out blo, obj);
			if (la.kind == 83) {
				OP_INSTANCEOF(out blo, obj, blo);
			}
		} else if (la.kind == 90) {
			OP_SQFCALL(out blo, obj);
		} else if (la.kind == 72) {
			OP_NULL(out blo, obj);
		} else if (la.kind == 73) {
			OP_DEREF(out blo, obj);
		} else SynErr(102);
	}

	void OP_NEWINSTANCE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewInstance(parent); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; 
		Expect(77);
		IDENTACCESS(out blo, obj, false);
		obj.Name = (Ident)blo; 
		if (la.kind == 21) {
			Template te; 
			TEMPLATE(out te, ((Ident)blo).LastIdent);
			obj.TemplateObject = te; 
		}
		BODY_FUNCTIONCALL(out blo2, ((Ident)blo).LastIdent);
		((Ident)blo).LastIdent.addChild(blo2); 
	}

	void OP_INSTANCEOF(out pBaseLangObject outObj, pBaseLangObject parent, pBaseLangObject identAccess) {
		var obj = new InstanceOf(parent); outObj = obj; pBaseLangObject blo; obj.LIdent = identAccess; identAccess.Parent = obj; 
		Expect(83);
		IDENTACCESS(out blo, obj);
		obj.RIdent = (Ident)blo; 
	}

	void OP_SQFCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new SqfCall(parent); outObj = obj; pBaseLangObject blo; VarTypeObject vto; 
		Expect(90);
		if (la.kind == 12) {
			Get();
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 16) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(103);
			while (la.kind == 20) {
				Get();
				if (StartOf(4)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 16) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(104);
			}
			Expect(13);
		}
		IDENT(out blo, outObj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 12) {
			Get();
			obj.markEnd(); 
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 16) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(105);
			while (la.kind == 20) {
				Get();
				if (StartOf(4)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 16) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(106);
			}
			Expect(13);
		}
		if (la.kind == 91) {
			obj.HasAs = true; 
			Get();
			VARTYPE(out vto, obj);
			obj.ReferencedType = vto; 
		}
	}

	void OP_NULL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Null(parent, t.line, t.col, this.file); outObj = obj; 
		Expect(72);
	}

	void OP_DEREF(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Deref(parent, t.line, t.col, this.file); outObj = obj; pBaseLangObject blo; 
		Expect(73);
		IDENTACCESS(out blo, obj, false);
		obj.addChild((Ident)blo); 
	}

	void OOS() {
		if(this.BaseObject == null) throw new Exception("BaseObject was never set"); var obj = this.BaseObject; pBaseLangObject blo; 
		if (la.kind == 48) {
			OP_USING();
			while (la.kind == 48) {
				OP_USING();
			}
		}
		while (StartOf(7)) {
			switch (la.kind) {
			case 50: {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 63: {
				CLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 51: {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 49: {
				ENUM(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 67: {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 47: {
				Get();
				if (StartOf(8)) {
					SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (la.kind == 59) {
					NATIVEFUNCTION(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(107);
				break;
			}
			}
		}
	}

	void OP_USING() {
		List<string> identList = new List<string>(); 
		Expect(48);
		if (la.kind == 19) {
			Get();
		}
		Expect(3);
		identList.Add(t.val); 
		while (la.kind == 19) {
			Get();
			Expect(3);
			identList.Add(t.val); 
		}
		if (errDist >= minErrDist)
		{
		
		bool flag = false;
		string currentFile = "";
		foreach(string lookupPath in new string[] {Wrapper.Compiler.ProjectFile.SrcFolder, Wrapper.Compiler.stdLibPath})
		{
			if(flag)
				break;
			currentFile = lookupPath;
			foreach (var it in identList)
			{
				string tmp = currentFile + it;
				if (Directory.Exists(tmp))
				{
					currentFile += it + '\\';
				}
				else if (Directory.EnumerateFiles(currentFile).Any(file =>
				{
					int index = file.LastIndexOf('\\');
					string tmpFile = file;
					file = file.Substring(index + 1);
					if (file.StartsWith(it))
					{
						index = file.IndexOf('.');
						if (index >= 0)
						{
							file = file.Substring(0, index);
						}
						if (file == it)
						{
							currentFile = tmpFile;
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return false;
					}
				}))
				{
					if (identList.Last() == it)
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			if (!UsedFiles.Contains(currentFile))
			{
				UsedFiles.Add(currentFile);
				var ppFiles = new List<Compiler.PostProcessFile>();
				Wrapper.Compiler.Instance.preprocessFile(new List<Wrapper.Compiler.preprocessFile_IfDefModes>(), currentFile, currentFile, ppFiles);
				Scanner scanner = new Scanner(ppFiles.First().FileStream);
				Base baseObject = new Base();
				Parser p = new Parser(scanner, currentFile);
				p.BaseObject = this.BaseObject;
				p.Parse();
				if (p.errors.count > 0)
				{
					this.errors.count += p.errors.count;
					Logger.Instance.log(Logger.LogLevel.ERROR, "In file '" + currentFile + "'");
				}
			}
		}
		else
		{
			string errString = "";
			foreach(var it in identList)
				errString += "::" + it;
			SemErr("Invalid Operation, path could not be dereferenced: " + errString);
		}
		}
		
		if (la.kind == 4) {
			TERMINATOR();
		}
	}

	void NAMESPACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Namespace(parent); outObj = obj; pBaseLangObject blo; 
		Expect(50);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo;
		
		var nsList = parent.getAllChildrenOf<Namespace>();
		foreach(var it in nsList)
		{
			if(it.Name.OriginalValue == ((Ident)blo).OriginalValue)
			{
				obj = it;
				outObj = obj;
				break;
			}
		}
		
		Expect(16);
		while (StartOf(7)) {
			switch (la.kind) {
			case 50: {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 63: {
				CLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 49: {
				ENUM(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 51: {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 67: {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 47: {
				Get();
				if (StartOf(8)) {
					SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (la.kind == 59) {
					NATIVEFUNCTION(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(108);
				break;
			}
			}
		}
		Expect(17);
	}

	void CLASS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosClass(parent);
		outObj = obj;
		pBaseLangObject blo;
		Encapsulation e = Encapsulation.Private;
		bool hasConstructor = false;
		bool flag_disableConstructor = false;
		bool flag_noObjectExtends = false;
		bool flag_virtualFunctionsOnly = false;
		
		Expect(63);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 52) {
			Get();
			while (la.kind == 53 || la.kind == 54 || la.kind == 64) {
				if (la.kind == 53) {
					Get();
					flag_disableConstructor = true; 
				} else if (la.kind == 54) {
					Get();
					flag_noObjectExtends = true; 
				} else {
					Get();
					flag_virtualFunctionsOnly = true; 
				}
			}
		}
		if (la.kind == 55) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParentClass((Ident)blo); 
		}
		obj.markEnd();
		if(obj.ParentClassesIdents.Count == 0 && !flag_noObjectExtends)
		{
		   var ident = new Ident(obj, "object", -1, -1, "");
		ident.IsGlobalIdentifier = true;
		   obj.addParentClass(ident);
		}
		obj.markExtendsEnd();
		
		if (la.kind == 65) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParentClass((Ident)blo); 
			while (la.kind == 20) {
				Get();
				IDENTACCESS(out blo, obj, false);
				obj.addParentClass((Ident)blo); 
			}
		}
		obj.markEnd(); 
		Expect(16);
		while (StartOf(9)) {
			e = Encapsulation.Private; 
			if (StartOf(10)) {
				if (la.kind == 29 || la.kind == 30 || la.kind == 31) {
					ENCAPSULATION(out e);
				} else {
					Get();
					e = Encapsulation.Static; 
				}
			}
			if (la.kind == 49) {
				ENUM(out blo, obj);
				obj.addChild(blo); 
			} else if (StartOf(11)) {
				bool isInline = false; 
				if (la.kind == 66) {
					Get();
					isInline = true; 
				}
				if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN) && la.val.Equals(obj.Name.OriginalValue) ) {
					CONSTRUCTOR(out blo, obj, e, isInline);
					obj.addChild(blo); hasConstructor = true; if(flag_disableConstructor)  { SemErr("Constructors are disabled in flags for this class"); } 
				} else if (StartOf(8)) {
					SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, e, flag_virtualFunctionsOnly, isInline);
					obj.addChild(blo); 
				} else SynErr(109);
			} else SynErr(110);
		}
		Expect(17);
		if(!hasConstructor && !flag_disableConstructor) {
		            var constructor = new Function(obj);
		constructor.encapsulation = Encapsulation.Public;
		            try
		            {
		              constructor.Name = new Ident(constructor, obj.Name.OriginalValue, obj.Name.Line, obj.Name.Pos, this.file);
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

	void NATIVECLASS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Native(parent, t.line, t.col, this.file);
		outObj = obj;
		pBaseLangObject blo;
		Template te;
		bool flag = false;
		bool flag_disableConstructor = false;
		bool flag_noObjectExtends = false;
		
		Expect(51);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 21) {
			TEMPLATE(out te, obj);
			obj.TemplateObject = te; flag = true; obj.VTO = new VarTypeObject(obj.Name); 
		}
		if (la.kind == 52) {
			Get();
			while (la.kind == 53 || la.kind == 54) {
				if (la.kind == 53) {
					Get();
					flag_disableConstructor = true; 
				} else {
					Get();
					flag_noObjectExtends = true; 
				}
			}
		}
		if (la.kind == 55) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParent((Ident)blo); 
		}
		if(obj.parentIdents.Count == 0 && !flag_noObjectExtends) { var ident = new Ident(obj, "nobject", obj.Name.Line, obj.Name.Pos, obj.Name.File); ident.IsGlobalIdentifier = true; obj.addParent(ident); } 
		if(!flag) obj.VTO = new VarTypeObject(obj.Name); 
		flag = false; 
		Expect(16);
		while (StartOf(12)) {
			if (la.kind == 56) {
				NATIVEASSIGN(out blo, obj);
				obj.addChild(blo); flag = true; if(flag_disableConstructor)  { SemErr("Constructors are disabled in flags for this class"); } 
			} else if (la.kind == 59) {
				NATIVEFUNCTION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 49) {
				ENUM(out blo, obj);
				obj.addChild(blo); 
			} else {
				NATIVEOPERATOR(out blo, obj);
				obj.addChild(blo); 
			}
		}
		if(!flag && !flag_disableConstructor)
		{
		var assign = new NativeAssign(obj, obj.Name.Line, obj.Name.Pos, obj.Name.File);
		assign.IsSimple = true;
		assign.Code = "throw \"Object is missing constructor\";";
		obj.addChild(assign);
		}
		
		Expect(17);
	}

	void ENUM(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosEnum(parent); outObj = obj; pBaseLangObject blo; oosEnum.EnumEntry entry; 
		Expect(49);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(16);
		entry = new oosEnum.EnumEntry(obj); obj.addChild(entry); 
		IDENT(out blo, entry);
		entry.Name = (Ident)blo; 
		if (la.kind == 9) {
			Get();
			VALUE(out blo, entry);
			entry.Value = (Value) blo; 
		}
		while (la.kind == 20) {
			entry = new oosEnum.EnumEntry(obj); obj.addChild(entry); 
			Get();
			IDENT(out blo, entry);
			entry.Name = (Ident)blo; 
			if (la.kind == 9) {
				Get();
				VALUE(out blo, entry);
				entry.Value = (Value) blo; 
			}
		}
		Expect(17);
	}

	void INTERFACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosInterface(parent); outObj = obj; pBaseLangObject blo; 
		Expect(67);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; obj.VTO = new VarTypeObject((Ident)blo); 
		Expect(16);
		while (StartOf(13)) {
			VFUNCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(17);
	}

	void NATIVEFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeFunction(parent, t.line, t.col, this.file);
		outObj = obj;
		pBaseLangObject blo;
		VarTypeObject vto;
		
		Expect(59);
		if (la.kind == 57) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.VTO = vto; 
		} else if (la.kind == 25) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else SynErr(111);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		while (StartOf(14)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(60);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEASSIGN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeAssign(parent, t.line, t.col, this.file);
		outObj = obj;
		obj.Name = new Ident(obj, ((Native)parent).Name.OriginalValue, ((Native)parent).Name.Line, ((Native)parent).Name.Pos, this.file);
		
		Expect(56);
		if (la.kind == 57) {
			Get();
			obj.IsSimple = true; 
		}
		FNCARGLIST(obj);
		while (StartOf(15)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(58);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEOPERATOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeOperator(parent, t.line, t.col, this.file);
		outObj = obj;
		VarTypeObject vto;
		
		Expect(61);
		if (la.kind == 57) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.VTO = vto; 
		} else if (la.kind == 25) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else SynErr(112);
		if (la.kind == 14) {
			Get();
			Expect(15);
			obj.OperatorType = OverridableOperator.ArrayAccess; 
		} else if (StartOf(5)) {
			switch (la.kind) {
			case 5: {
				Get();
				switch(t.val) {
				case "==":
				obj.OperatorType = OverridableOperator.ExplicitEquals;
				break;
				default:
				SemErr("The operator '" + t.val + "' is not supported for override");
				break;
				}
				break;
			}
			case 6: {
				Get();
				switch(t.val) {
				case "==":
				obj.OperatorType = OverridableOperator.ExplicitEquals;
				break;
				default:
				SemErr("The operator '" + t.val + "' is not supported for override");
				break;
				}
				break;
			}
			case 7: {
				Get();
				switch(t.val) {
				case "==":
				obj.OperatorType = OverridableOperator.ExplicitEquals;
				break;
				default:
				SemErr("The operator '" + t.val + "' is not supported for override");
				break;
				}
				break;
			}
			case 23: {
				Get();
				SemErr("The operator '" + t.val + "' is not supported for override"); 
				break;
			}
			case 21: {
				Get();
				Expect(21);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
				break;
			}
			case 22: {
				Get();
				Expect(22);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
				break;
			}
			}
		} else SynErr(113);
		FNCARGLIST(obj);
		while (StartOf(16)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(62);
		obj.Code = obj.Code.Trim(); 
	}

	void CONSTRUCTOR(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e, bool isInline) {
		var obj = new Function(parent);outObj = obj;
		obj.varType = new VarTypeObject(((oosClass)parent).Name);
		obj.encapsulation = e;
		pBaseLangObject blo;
		obj.IsInline = isInline;
		
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		obj.markArgListEnd(); 
		if (la.kind == 69) {
			Get();
			IDENTACCESS(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 3 || la.kind == 19 || la.kind == 21) {
				IDENTACCESS(out blo, obj);
				obj.addChild(blo); 
			}
		}
		obj.markBaseCallEnd(); 
		Expect(16);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(17);
	}

	void VFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VirtualFunction(parent); outObj = obj; pBaseLangObject blo; VarTypeObject vto; 
		if (la.kind == 26) {
			Get();
			obj.IsAsync = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.varType = vto; 
		} else if (la.kind == 25) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else SynErr(114);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(12);
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.argTypes.Add(vto); 
			while (la.kind == 20) {
				Get();
				VARTYPE(out vto, obj);
				obj.argTypes.Add(vto); 
			}
		}
		Expect(13);
		TERMINATOR();
	}

	void CODEINSTRUCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (StartOf(17)) {
			CODEINSTRUCTION_SC(out outObj, parent);
			TERMINATOR();
		} else if (StartOf(18)) {
			CODEINSTRUCTION_NSC(out outObj, parent);
		} else SynErr(115);
	}

	void BODY_ASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject parent, bool allowAlt = false) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		if (la.kind == 9) {
			Get();
			obj.Operation = AssignmentCharacters.SimpleAssign; 
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 16) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(116);
		} else if (allowAlt ) {
			if (la.kind == 11) {
				Get();
				obj.Operation = t.val == "++" ? AssignmentCharacters.PlusOne : AssignmentCharacters.MinusOne; 
			} else if (la.kind == 10) {
				Get();
				switch(t.val)
				{
				case "+=":
					obj.Operation = AssignmentCharacters.AdditionAssign;
					break;
				case "-=":
					obj.Operation = AssignmentCharacters.SubstractionAssign;
					break;
				case "*=":
					obj.Operation = AssignmentCharacters.MultiplicationAssign;
					break;
				case "/=":
					obj.Operation = AssignmentCharacters.DivisionAssign;
					break;
				default:
					throw new Exception();
				}
				
				if (StartOf(4)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 16) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(117);
			} else SynErr(118);
		} else SynErr(119);
	}

	void VARIABLEASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject ident, pBaseLangObject parent) {
		var obj = new AssignContainer(parent); obj.Name = (Ident)ident; ident.Parent = obj; outObj = obj; pBaseLangObject blo; 
		BODY_ASSIGNMENT(out blo, outObj, true);
		obj.assign = (VariableAssignment)blo; 
	}

	void AUTOVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.line, la.col, this.file); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		Expect(70);
		obj.varType = new VarTypeObject(VarType.Auto); 
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		BODY_ASSIGNMENT(out blo, outObj);
		obj.addChild(blo); 
	}

	void CODEINSTRUCTION_SC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null;
		VarTypeObject vto;
		
		if (la.kind == 84) {
			OP_THROW(out outObj, parent);
		} else if (la.kind == 85) {
			OP_RETURN(out outObj, parent);
		} else if (StartOf(1)) {
			VARTYPE(out vto, parent, true);
			if (StartOf(19)) {
				if (vto.IsObject) {
					if (la.kind == 9 || la.kind == 10 || la.kind == 11) {
						VARIABLEASSIGNMENT(out outObj, vto.ident, parent);
						vto.ident.Parent = outObj; 
					} else if (la.kind == 3) {
						NEWVARIABLE(out outObj, parent, vto);
					} else SynErr(120);
				} else {
					NEWVARIABLE(out outObj, parent, vto);
				}
			}
			if(outObj == null && vto.IsObject)
			{
			outObj = vto.ident;
			vto.ident.Parent = parent;
			if(vto.ident.IsPureIdent)
			{
				Warning("Pure-Ident encountered during parsing, please add some use to it");
			}
			}
			
		} else if (la.kind == 70) {
			AUTOVARIABLE(out outObj, parent);
		} else if (la.kind == 90) {
			OP_SQFCALL(out outObj, parent);
		} else if (parent.getFirstOf<Compiler.OOS_LanguageObjects.Interfaces.iBreakable>() != null) {
			OP_BREAK(out outObj, parent);
		} else SynErr(121);
	}

	void OP_THROW(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Throw(parent); outObj = obj; pBaseLangObject blo; 
		Expect(84);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void OP_RETURN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Return(parent, t.line, t.col, this.file); outObj = obj; pBaseLangObject blo; 
		Expect(85);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
		}
	}

	void OP_BREAK(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Break(parent); outObj = obj; 
		Expect(82);
	}

	void CODEINSTRUCTION_NSC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		switch (la.kind) {
		case 71: {
			OP_FOR(out outObj, parent);
			break;
		}
		case 76: {
			OP_WHILE(out outObj, parent);
			break;
		}
		case 74: {
			OP_FOREACH(out outObj, parent);
			break;
		}
		case 78: {
			OP_IFELSE(out outObj, parent);
			break;
		}
		case 86: {
			OP_SWITCH(out outObj, parent);
			break;
		}
		case 80: {
			OP_TRYCATCH(out outObj, parent);
			break;
		}
		default: SynErr(122); break;
		}
	}

	void OP_FOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new For(parent); outObj = obj; pBaseLangObject blo; 
		Expect(71);
		Expect(12);
		if (StartOf(17)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg1 = blo; 
		}
		TERMINATOR();
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.forArg2 = blo; 
		}
		TERMINATOR();
		if (StartOf(17)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg3 = blo; 
		}
		Expect(13);
		if (la.kind == 16) {
			Get();
			while (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(17);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(123);
	}

	void OP_WHILE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new While(parent); outObj = obj; pBaseLangObject blo; 
		Expect(76);
		Expect(12);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(13);
		if (la.kind == 16) {
			Get();
			while (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(17);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(124);
	}

	void OP_FOREACH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new ForEach(parent); outObj = obj; VarTypeObject vto = null; pBaseLangObject blo; 
		Expect(74);
		Expect(12);
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
		} else if (la.kind == 70) {
			Get();
			vto = new VarTypeObject(VarType.Auto); 
		} else SynErr(125);
		NEWVARIABLE(out blo, obj, vto);
		obj.Itterator = (Variable)blo; 
		Expect(75);
		IDENTACCESS(out blo, obj, false);
		obj.Variable = (Ident)blo; 
		Expect(13);
		if (la.kind == 16) {
			Get();
			while (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(17);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(126);
	}

	void OP_IFELSE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new IfElse(parent); outObj = obj; pBaseLangObject blo; 
		Expect(78);
		Expect(12);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(13);
		if (la.kind == 16) {
			Get();
			while (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(17);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(127);
		obj.markIfEnd(); 
		if (la.kind == 79) {
			Get();
			if (la.kind == 16) {
				Get();
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				}
				Expect(17);
			} else if (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(128);
		}
	}

	void OP_SWITCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Switch(parent); Case caseObj; outObj = obj; pBaseLangObject blo; 
		Expect(86);
		Expect(12);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(13);
		Expect(16);
		while (la.kind == 87 || la.kind == 88 || la.kind == 89) {
			if (la.kind == 87) {
				Get();
				caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
				EXPRESSION(out blo, caseObj);
				caseObj.addChild(blo); 
				Expect(69);
				while (la.kind == 87) {
					Get();
					EXPRESSION(out blo, caseObj);
					caseObj.addChild(blo); 
					Expect(69);
				}
				caseObj.markEnd(); 
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, caseObj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 82) {
					OP_BREAK(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 84) {
					OP_THROW(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 85) {
					OP_RETURN(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else SynErr(129);
			} else {
				if (la.kind == 88) {
					Get();
					caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
					Expect(69);
				} else {
					Get();
					caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
				}
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, caseObj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 82) {
					OP_BREAK(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 84) {
					OP_THROW(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 85) {
					OP_RETURN(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else SynErr(130);
			}
		}
		Expect(17);
	}

	void OP_TRYCATCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new TryCatch(parent);
		outObj = obj;
		pBaseLangObject blo;
		VarTypeObject vto;
		
		Expect(80);
		Expect(16);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(17);
		obj.markEnd(); 
		Expect(81);
		Expect(12);
		VARTYPE(out vto, obj);
		NEWVARIABLE(out blo, obj, vto);
		obj.variable = blo; 
		Expect(13);
		Expect(16);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(17);
	}

	void OP_NEWARRAY(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewArray(parent); outObj = obj; pBaseLangObject blo; 
		Expect(16);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 20) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(17);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _x,_x,_T,_x, _T,_x,_T,_x, _T,_x,_T,_x, _T,_T,_T,_x, _x,_x,_T,_x, _x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x},
		{_x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_T,_T,_T, _T,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_T,_T,_T, _T,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _T,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _T,_T,_x,_x, _x,_x,_T,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_T,_x, _T,_x,_T,_x, _T,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x}

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
			case 5: s = "T_EXPOP1 expected"; break;
			case 6: s = "T_EXPOP2 expected"; break;
			case 7: s = "T_EXPOP3 expected"; break;
			case 8: s = "T_OTHEROP expected"; break;
			case 9: s = "T_ASSIGNMENTCHAR expected"; break;
			case 10: s = "T_EXTENDEDASSIGNMENTCHARS expected"; break;
			case 11: s = "T_FASTASSIGNMENTCHARS expected"; break;
			case 12: s = "T_ROUNDBRACKETOPEN expected"; break;
			case 13: s = "T_ROUNDBRACKETCLOSE expected"; break;
			case 14: s = "T_SQUAREBRACKETOPEN expected"; break;
			case 15: s = "T_SQUAREBRACKETCLOSE expected"; break;
			case 16: s = "T_CODEBRACKETOPEN expected"; break;
			case 17: s = "T_CODEBRACKETCLOSE expected"; break;
			case 18: s = "T_INSTANCEACCESS expected"; break;
			case 19: s = "T_NAMESPACEACCESS expected"; break;
			case 20: s = "T_COMMA expected"; break;
			case 21: s = "T_TEMPLATEOPEN expected"; break;
			case 22: s = "T_TEMPLATECLOSE expected"; break;
			case 23: s = "T_SLASH expected"; break;
			case 24: s = "T_BACKSLASH expected"; break;
			case 25: s = "\"void\" expected"; break;
			case 26: s = "\"async\" expected"; break;
			case 27: s = "\"virtual\" expected"; break;
			case 28: s = "\"external\" expected"; break;
			case 29: s = "\"public\" expected"; break;
			case 30: s = "\"private\" expected"; break;
			case 31: s = "\"protected\" expected"; break;
			case 32: s = "\"scalar\" expected"; break;
			case 33: s = "\"int\" expected"; break;
			case 34: s = "\"double\" expected"; break;
			case 35: s = "\"float\" expected"; break;
			case 36: s = "\"bool\" expected"; break;
			case 37: s = "\"boolean\" expected"; break;
			case 38: s = "\"scalar[]\" expected"; break;
			case 39: s = "\"int[]\" expected"; break;
			case 40: s = "\"double[]\" expected"; break;
			case 41: s = "\"float[]\" expected"; break;
			case 42: s = "\"bool[]\" expected"; break;
			case 43: s = "\"boolean[]\" expected"; break;
			case 44: s = "\"true\" expected"; break;
			case 45: s = "\"false\" expected"; break;
			case 46: s = "\"!\" expected"; break;
			case 47: s = "\"static\" expected"; break;
			case 48: s = "\"using\" expected"; break;
			case 49: s = "\"enum\" expected"; break;
			case 50: s = "\"namespace\" expected"; break;
			case 51: s = "\"native\" expected"; break;
			case 52: s = "\"flags\" expected"; break;
			case 53: s = "\"disableConstructor\" expected"; break;
			case 54: s = "\"noObjectExtends\" expected"; break;
			case 55: s = "\"extends\" expected"; break;
			case 56: s = "\"assign\" expected"; break;
			case 57: s = "\"simple\" expected"; break;
			case 58: s = "\"endAssign\" expected"; break;
			case 59: s = "\"fnc\" expected"; break;
			case 60: s = "\"endFnc\" expected"; break;
			case 61: s = "\"operator\" expected"; break;
			case 62: s = "\"endOperator\" expected"; break;
			case 63: s = "\"class\" expected"; break;
			case 64: s = "\"virtualFunctionsOnly\" expected"; break;
			case 65: s = "\"implements\" expected"; break;
			case 66: s = "\"inline\" expected"; break;
			case 67: s = "\"interface\" expected"; break;
			case 68: s = "\"throwing\" expected"; break;
			case 69: s = "\":\" expected"; break;
			case 70: s = "\"auto\" expected"; break;
			case 71: s = "\"for\" expected"; break;
			case 72: s = "\"null\" expected"; break;
			case 73: s = "\"deref\" expected"; break;
			case 74: s = "\"foreach\" expected"; break;
			case 75: s = "\"in\" expected"; break;
			case 76: s = "\"while\" expected"; break;
			case 77: s = "\"new\" expected"; break;
			case 78: s = "\"if\" expected"; break;
			case 79: s = "\"else\" expected"; break;
			case 80: s = "\"try\" expected"; break;
			case 81: s = "\"catch\" expected"; break;
			case 82: s = "\"break\" expected"; break;
			case 83: s = "\"is\" expected"; break;
			case 84: s = "\"throw\" expected"; break;
			case 85: s = "\"return\" expected"; break;
			case 86: s = "\"switch\" expected"; break;
			case 87: s = "\"case\" expected"; break;
			case 88: s = "\"default\" expected"; break;
			case 89: s = "\"default:\" expected"; break;
			case 90: s = "\"SQF\" expected"; break;
			case 91: s = "\"as\" expected"; break;
			case 92: s = "??? expected"; break;
			case 93: s = "invalid VARTYPE"; break;
			case 94: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 95: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 96: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 97: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 98: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 99: s = "invalid ENCAPSULATION"; break;
			case 100: s = "invalid BOOLEAN"; break;
			case 101: s = "invalid VALUE"; break;
			case 102: s = "invalid EXPRESSION_HELPER"; break;
			case 103: s = "invalid OP_SQFCALL"; break;
			case 104: s = "invalid OP_SQFCALL"; break;
			case 105: s = "invalid OP_SQFCALL"; break;
			case 106: s = "invalid OP_SQFCALL"; break;
			case 107: s = "invalid OOS"; break;
			case 108: s = "invalid NAMESPACE"; break;
			case 109: s = "invalid CLASS"; break;
			case 110: s = "invalid CLASS"; break;
			case 111: s = "invalid NATIVEFUNCTION"; break;
			case 112: s = "invalid NATIVEOPERATOR"; break;
			case 113: s = "invalid NATIVEOPERATOR"; break;
			case 114: s = "invalid VFUNCTION"; break;
			case 115: s = "invalid CODEINSTRUCTION"; break;
			case 116: s = "invalid BODY_ASSIGNMENT"; break;
			case 117: s = "invalid BODY_ASSIGNMENT"; break;
			case 118: s = "invalid BODY_ASSIGNMENT"; break;
			case 119: s = "invalid BODY_ASSIGNMENT"; break;
			case 120: s = "invalid CODEINSTRUCTION_SC"; break;
			case 121: s = "invalid CODEINSTRUCTION_SC"; break;
			case 122: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 123: s = "invalid OP_FOR"; break;
			case 124: s = "invalid OP_WHILE"; break;
			case 125: s = "invalid OP_FOREACH"; break;
			case 126: s = "invalid OP_FOREACH"; break;
			case 127: s = "invalid OP_IFELSE"; break;
			case 128: s = "invalid OP_IFELSE"; break;
			case 129: s = "invalid OP_SWITCH"; break;
			case 130: s = "invalid OP_SWITCH"; break;

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
