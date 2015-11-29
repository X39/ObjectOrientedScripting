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
	public const int _T_EXPOP = 5;
	public const int _T_OTHEROP = 6;
	public const int _T_ASSIGNMENTCHAR = 7;
	public const int _T_EXTENDEDASSIGNMENTCHARS = 8;
	public const int _T_FASTASSIGNMENTCHARS = 9;
	public const int _T_ROUNDBRACKETOPEN = 10;
	public const int _T_ROUNDBRACKETCLOSE = 11;
	public const int _T_SQUAREBRACKETOPEN = 12;
	public const int _T_SQUAREBRACKETCLOSE = 13;
	public const int _T_CODEBRACKETOPEN = 14;
	public const int _T_CODEBRACKETCLOSE = 15;
	public const int _T_INSTANCEACCESS = 16;
	public const int _T_NAMESPACEACCESS = 17;
	public const int _T_COMMA = 18;
	public const int _T_TEMPLATEOPEN = 19;
	public const int _T_TEMPLATECLOSE = 20;
	public const int _T_SLASH = 21;
	public const int _T_BACKSLASH = 22;
	public const int maxT = 78;

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
		
		Expect(10);
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			NEWVARIABLE(out blo, obj, vto);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				VARTYPE(out vto, obj);
				NEWVARIABLE(out blo, obj, vto);
				obj.addChild(blo); 
			}
		}
		Expect(11);
	}

	void VARTYPE(out VarTypeObject e, pBaseLangObject parent, bool identFlag = false) {
		e = null; 
		if (StartOf(2)) {
			switch (la.kind) {
			case 30: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
			case 31: {
				Get();
				e = new VarTypeObject(VarType.Scalar); 
				break;
			}
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
				e = new VarTypeObject(VarType.Bool); 
				break;
			}
			case 35: {
				Get();
				e = new VarTypeObject(VarType.Bool); 
				break;
			}
			}
			if (la.kind == 12) {
				Get();
				Expect(13);
				switch(e.varType)
				{
				case VarType.Scalar:
				e = new VarTypeObject(VarType.ScalarArray);
				break;
				case VarType.Bool:
				e = new VarTypeObject(VarType.BoolArray);
				break;
				default:
				SemErr("Cannot Arrayify VarTypes which are not scalar/bool");
				break;
				} 
			}
		} else if (la.kind == 3 || la.kind == 17 || la.kind == 19) {
			pBaseLangObject blo; 
			IDENTACCESS(out blo, parent, identFlag);
			e = new VarTypeObject((Ident)blo); 
			if (la.kind == 19) {
				Template te; 
				TEMPLATE(out te, ((Ident)blo).LastIdent);
				e.TemplateObject = te; 
			}
		} else SynErr(79);
	}

	void NEWVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, VarTypeObject vto, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.line, la.col, this.file);
		obj.encapsulation = e;
		outObj = obj;
		pBaseLangObject blo;
		obj.varType = vto;
		
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		if (la.kind == 7 || la.kind == 8 || la.kind == 9) {
			BODY_ASSIGNMENT(out blo, outObj);
			obj.addChild(blo); 
		}
	}

	void SPECIAL_NEWVARIABLEFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e, bool fVFncOnly = false) {
		outObj = null;
		pBaseLangObject blo;
		bool isAsync = false;
		bool isVirtual = false;
		VarTypeObject vto = null;
		
		if (StartOf(1)) {
			VARTYPE(out vto, parent);
			if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN)) {
				FUNCTION(out blo, parent, e, vto);
				outObj = blo;
				if(!((Function)blo).IsVirtual && fVFncOnly)
				{
				SemErr("Non-Virtual function on VirtualOnly class");
				} 
				
			} else if (la.kind == 3) {
				NEWVARIABLE(out blo, parent, vto, e);
				outObj = blo; 
				TERMINATOR();
			} else SynErr(80);
		} else if (la.kind == 23) {
			Get();
			vto = new VarTypeObject(VarType.Void); 
			FUNCTION(out blo, parent, e, vto);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 24) {
			Get();
			isAsync = true; 
			if (la.kind == 25) {
				Get();
				isVirtual = true; 
			}
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 23) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(81);
			FUNCTION(out blo, parent, e, vto, isAsync, isVirtual);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 25) {
			Get();
			isVirtual = true; 
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 23) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(82);
			FUNCTION(out blo, parent, e, vto, isAsync, isVirtual);
			outObj = blo;
			if(!((Function)blo).IsVirtual && fVFncOnly)
			{
			SemErr("Non-Virtual function on VirtualOnly class");
			}
			
		} else if (la.kind == 26) {
			Get();
			if (la.kind == 24) {
				Get();
				isAsync = true; 
			}
			if (StartOf(1)) {
				VARTYPE(out vto, parent);
			} else if (la.kind == 23) {
				Get();
				vto = new VarTypeObject(VarType.Void); 
			} else SynErr(83);
			FUNCTION(out blo, parent, e, vto, isAsync, false, true);
			outObj = blo; 
			TERMINATOR();
		} else SynErr(84);
	}

	void FUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e, VarTypeObject vto, bool isAsync = false, bool isVirtual = false, bool isExternal = false) {
		var obj = new Function(parent); outObj = obj;
		obj.encapsulation = e;
		obj.IsAsync = isAsync;
		obj.IsVirtual = isVirtual;
		obj.varType = vto;
		obj.IsExternal = isExternal;
		pBaseLangObject blo;
		if(isExternal && isVirtual) SemErr("External function is marked as virtual");
		if(e == Encapsulation.Static && isVirtual) SemErr("Static function is marked as virtual");
		
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		obj.markArgListEnd(); 
		if(!isExternal) { 
		Expect(14);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
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
		Expect(19);
		VARTYPE(out vto, obj);
		obj.vtoList.Add(vto); 
		while (la.kind == 18) {
			Get();
			VARTYPE(out vto, obj);
			obj.vtoList.Add(vto); 
		}
		Expect(20);
	}

	void IDENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		Expect(3);
		outObj = new Ident(parent, t.val, t.line, t.col, this.file); 
	}

	void IDENTACCESS(out pBaseLangObject outObj, pBaseLangObject parent, bool allowBody = true) {
		pBaseLangObject blo; pBaseLangObject ident; outObj = null; bool isGlobalIdent = false; 
		if (la.kind == 17) {
			Get();
			isGlobalIdent = true; 
		}
		if (la.kind == 19) {
			CAST(out outObj, parent);
		}
		IDENT(out ident, (outObj == null ? parent : outObj));
		try{ ((Ident)ident).IsGlobalIdentifier = isGlobalIdent; } catch (Exception ex) { SemErr(ex.Message); } if(outObj == null) outObj = ident; else outObj.addChild(ident); 
		if(allowBody) { 
		if (la.kind == 10 || la.kind == 12) {
			if (la.kind == 10) {
				BODY_FUNCTIONCALL(out blo, ident);
				((Ident)ident).addChild(blo); 
			} else {
				BODY_ARRAYACCESS(out blo, ident);
				((Ident)ident).addChild(blo); 
			}
		}
		} 
		if (la.kind == 16 || la.kind == 17) {
			if (la.kind == 16) {
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
		Expect(19);
		VARTYPE(out vto, obj);
		obj.varType = vto; 
		Expect(20);
	}

	void BODY_FUNCTIONCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new FunctionCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(10);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
	}

	void BODY_ARRAYACCESS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new ArrayAccess(parent); outObj = obj; pBaseLangObject blo; 
		Expect(12);
		VALUE(out blo, obj);
		obj.addChild(blo); 
		Expect(13);
	}

	void ENCAPSULATION(out Encapsulation e) {
		e = Encapsulation.NA; 
		if (la.kind == 27) {
			Get();
			e = Encapsulation.Public; 
		} else if (la.kind == 28) {
			Get();
			e = Encapsulation.Private; 
		} else if (la.kind == 29) {
			Get();
			e = Encapsulation.Protected; 
		} else SynErr(85);
	}

	void BOOLEAN(out bool flag) {
		flag = la.val == "true"; Get(); return; /*fix for weirdo coco bug ...*/ 
		if (la.kind == 36) {
			Get();
			flag = true; 
		} else if (la.kind == 37) {
			Get();
			flag = false; 
		} else SynErr(86);
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
		} else if (la.kind == 36 || la.kind == 37) {
			BOOLEAN(out flag);
			
		} else SynErr(87);
	}

	void EXPRESSION_HELPER(out pBaseLangObject outObj, pBaseLangObject parent, bool flag) {
		var obj = new Expression(parent, t.line, t.col, this.file); outObj = obj; pBaseLangObject blo; obj.negate = flag; 
		if (la.kind == 38) {
			Get();
			obj.negate = true; 
		}
		if (la.kind == 63) {
			OP_NEWINSTANCE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.val == "true" || la.val == "false" ) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (StartOf(5)) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (la.kind == 3 || la.kind == 17 || la.kind == 19) {
			IDENTACCESS(out blo, obj);
			obj.lExpression = blo; 
			if (la.kind == 69) {
				OP_INSTANCEOF(out blo, obj, blo);
				obj.lExpression = blo; 
			}
		} else if (la.kind == 76) {
			OP_SQFCALL(out blo, obj);
			obj.lExpression = blo; 
		} else SynErr(88);
		while (StartOf(6)) {
			if (la.kind == 5) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 21) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 19) {
				Get();
				obj.expOperator = t.val; 
			} else {
				Get();
				obj.expOperator = t.val; 
			}
			EXPRESSION(out blo, obj);
			obj.rExpression = blo; 
		}
	}

	void OP_NEWINSTANCE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewInstance(parent); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; 
		Expect(63);
		IDENTACCESS(out blo, obj, false);
		obj.Name = (Ident)blo; 
		if (la.kind == 19) {
			Template te; 
			TEMPLATE(out te, ((Ident)blo).LastIdent);
			obj.TemplateObject = te; 
		}
		BODY_FUNCTIONCALL(out blo2, ((Ident)blo).LastIdent);
		((Ident)blo).LastIdent.addChild(blo2); 
	}

	void OP_INSTANCEOF(out pBaseLangObject outObj, pBaseLangObject parent, pBaseLangObject identAccess) {
		var obj = new InstanceOf(parent); outObj = obj; pBaseLangObject blo; obj.LIdent = identAccess; identAccess.Parent = obj; 
		Expect(69);
		IDENTACCESS(out blo, obj);
		obj.RIdent = (Ident)blo; 
	}

	void OP_SQFCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new SqfCall(parent); outObj = obj; pBaseLangObject blo; VarTypeObject vto; 
		Expect(76);
		if (la.kind == 10) {
			Get();
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 14) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(89);
			while (la.kind == 18) {
				Get();
				if (StartOf(4)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 14) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(90);
			}
			Expect(11);
		}
		IDENT(out blo, outObj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 10) {
			Get();
			obj.markEnd(); 
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 14) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(91);
			while (la.kind == 18) {
				Get();
				if (StartOf(4)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 14) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(92);
			}
			Expect(11);
		}
		if (la.kind == 77) {
			obj.HasAs = true; 
			Get();
			VARTYPE(out vto, obj);
			obj.ReferencedType = vto; 
		}
	}

	void EXPRESSION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; bool negate = false; 
		if (la.kind == 38) {
			Get();
			negate = true; 
		}
		if (la.kind == 10) {
			Get();
			EXPRESSION_HELPER(out outObj, parent, negate);
			Expect(11);
		} else if (StartOf(7)) {
			EXPRESSION_HELPER(out outObj, parent, negate);
		} else SynErr(93);
	}

	void OOS() {
		if(this.BaseObject == null) throw new Exception("BaseObject was never set"); var obj = this.BaseObject; pBaseLangObject blo; 
		if (la.kind == 40) {
			OP_USING();
			while (la.kind == 40) {
				OP_USING();
			}
		}
		while (StartOf(8)) {
			switch (la.kind) {
			case 42: {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 55: {
				CLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 43: {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 41: {
				ENUM(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 58: {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 39: {
				Get();
				if (StartOf(9)) {
					SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (la.kind == 51) {
					NATIVEFUNCTION(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(94);
				break;
			}
			}
		}
	}

	void OP_USING() {
		List<string> identList = new List<string>(); 
		Expect(40);
		if (la.kind == 17) {
			Get();
		}
		Expect(3);
		identList.Add(t.val); 
		while (la.kind == 17) {
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
				Scanner scanner = new Scanner(currentFile);
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
		
	}

	void NAMESPACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Namespace(parent); outObj = obj; pBaseLangObject blo; 
		Expect(42);
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
		
		Expect(14);
		while (StartOf(8)) {
			switch (la.kind) {
			case 42: {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 55: {
				CLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 41: {
				ENUM(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 43: {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 58: {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
				break;
			}
			case 39: {
				Get();
				if (StartOf(9)) {
					SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (la.kind == 51) {
					NATIVEFUNCTION(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(95);
				break;
			}
			}
		}
		Expect(15);
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
		
		Expect(55);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 44) {
			Get();
			while (la.kind == 45 || la.kind == 46 || la.kind == 56) {
				if (la.kind == 45) {
					Get();
					flag_disableConstructor = true; 
				} else if (la.kind == 46) {
					Get();
					flag_noObjectExtends = true; 
				} else {
					Get();
					flag_virtualFunctionsOnly = true; 
				}
			}
		}
		if (la.kind == 47) {
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
		
		if (la.kind == 57) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParentClass((Ident)blo); 
			while (la.kind == 18) {
				Get();
				IDENTACCESS(out blo, obj, false);
				obj.addParentClass((Ident)blo); 
			}
		}
		obj.markEnd(); 
		Expect(14);
		while (StartOf(10)) {
			e = Encapsulation.Private; 
			if (StartOf(11)) {
				if (la.kind == 27 || la.kind == 28 || la.kind == 29) {
					ENCAPSULATION(out e);
				} else {
					Get();
					e = Encapsulation.Static; 
				}
			}
			if (la.kind == 41) {
				ENUM(out blo, obj);
				obj.addChild(blo); 
			} else if (peekCompare(_T_IDENT, _T_ROUNDBRACKETOPEN) && la.val.Equals(obj.Name.OriginalValue) ) {
				CONSTRUCTOR(out blo, obj, e);
				obj.addChild(blo); hasConstructor = true; if(flag_disableConstructor)  { SemErr("Constructors are disabled in flags for this class"); } 
			} else if (StartOf(9)) {
				SPECIAL_NEWVARIABLEFUNCTION(out blo, obj, e, flag_virtualFunctionsOnly);
				obj.addChild(blo); 
			} else SynErr(96);
		}
		Expect(15);
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
		
		Expect(43);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 19) {
			TEMPLATE(out te, obj);
			obj.TemplateObject = te; flag = true; obj.VTO = new VarTypeObject(obj.Name); 
		}
		if (la.kind == 44) {
			Get();
			while (la.kind == 45 || la.kind == 46) {
				if (la.kind == 45) {
					Get();
					flag_disableConstructor = true; 
				} else {
					Get();
					flag_noObjectExtends = true; 
				}
			}
		}
		if (la.kind == 47) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParent((Ident)blo); 
		}
		if(obj.parentIdents.Count == 0 && !flag_noObjectExtends) { var ident = new Ident(obj, "nobject", obj.Name.Line, obj.Name.Pos, obj.Name.File); ident.IsGlobalIdentifier = true; obj.addParent(ident); } 
		if(!flag) obj.VTO = new VarTypeObject(obj.Name); 
		flag = false; 
		Expect(14);
		while (StartOf(12)) {
			if (la.kind == 48) {
				NATIVEASSIGN(out blo, obj);
				obj.addChild(blo); flag = true; if(flag_disableConstructor)  { SemErr("Constructors are disabled in flags for this class"); } 
			} else if (la.kind == 51) {
				NATIVEFUNCTION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 41) {
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
		
		Expect(15);
	}

	void ENUM(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosEnum(parent); outObj = obj; pBaseLangObject blo; oosEnum.EnumEntry entry; 
		Expect(41);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(14);
		entry = new oosEnum.EnumEntry(obj); obj.addChild(entry); 
		IDENT(out blo, entry);
		entry.Name = (Ident)blo; 
		if (la.kind == 7) {
			Get();
			VALUE(out blo, entry);
			entry.Value = (Value) blo; 
		}
		while (la.kind == 18) {
			entry = new oosEnum.EnumEntry(obj); obj.addChild(entry); 
			Get();
			IDENT(out blo, entry);
			entry.Name = (Ident)blo; 
			if (la.kind == 7) {
				Get();
				VALUE(out blo, entry);
				entry.Value = (Value) blo; 
			}
		}
		Expect(15);
	}

	void INTERFACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosInterface(parent); outObj = obj; pBaseLangObject blo; 
		Expect(58);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; obj.VTO = new VarTypeObject((Ident)blo); 
		Expect(14);
		while (StartOf(13)) {
			VFUNCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void NATIVEFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeFunction(parent, t.line, t.col, this.file);
		outObj = obj;
		pBaseLangObject blo;
		VarTypeObject vto;
		
		Expect(51);
		if (la.kind == 49) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.VTO = vto; 
		} else if (la.kind == 23) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else SynErr(97);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		while (StartOf(14)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(52);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEASSIGN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeAssign(parent, t.line, t.col, this.file);
		outObj = obj;
		obj.Name = new Ident(obj, ((Native)parent).Name.OriginalValue, ((Native)parent).Name.Line, ((Native)parent).Name.Pos, this.file);
		
		Expect(48);
		if (la.kind == 49) {
			Get();
			obj.IsSimple = true; 
		}
		FNCARGLIST(obj);
		while (StartOf(15)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(50);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEOPERATOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeOperator(parent, t.line, t.col, this.file);
		outObj = obj;
		VarTypeObject vto;
		
		Expect(53);
		if (la.kind == 49) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.VTO = vto; 
		} else if (la.kind == 23) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else SynErr(98);
		if (la.kind == 12) {
			Get();
			Expect(13);
			obj.OperatorType = OverridableOperator.ArrayAccess; 
		} else if (StartOf(6)) {
			if (la.kind == 5) {
				Get();
				switch(t.val) {
				case "===":
				obj.OperatorType = OverridableOperator.ExplicitEquals;
				break;
				default:
				SemErr("The operator '" + t.val + "' is not supported for override");
				break;
				}
			} else if (la.kind == 21) {
				Get();
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			} else if (la.kind == 19) {
				Get();
				Expect(19);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			} else {
				Get();
				Expect(20);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			}
		} else SynErr(99);
		FNCARGLIST(obj);
		while (StartOf(16)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(54);
		obj.Code = obj.Code.Trim(); 
	}

	void CONSTRUCTOR(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent);outObj = obj;
		obj.varType = new VarTypeObject(((oosClass)parent).Name);
		obj.encapsulation = e;
		pBaseLangObject blo;
		
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		FNCARGLIST(obj);
		obj.markArgListEnd(); 
		if (la.kind == 59) {
			Get();
			IDENTACCESS(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 3 || la.kind == 17 || la.kind == 19) {
				IDENTACCESS(out blo, obj);
				obj.addChild(blo); 
			}
		}
		obj.markBaseCallEnd(); 
		Expect(14);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void VFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VirtualFunction(parent); outObj = obj; pBaseLangObject blo; VarTypeObject vto; 
		if (la.kind == 24) {
			Get();
			obj.IsAsync = true; 
		}
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.varType = vto; 
		} else if (la.kind == 23) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else SynErr(100);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(10);
		if (StartOf(1)) {
			VARTYPE(out vto, obj);
			obj.argTypes.Add(vto); 
			while (la.kind == 18) {
				Get();
				VARTYPE(out vto, obj);
				obj.argTypes.Add(vto); 
			}
		}
		Expect(11);
		TERMINATOR();
	}

	void CODEINSTRUCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (StartOf(17)) {
			CODEINSTRUCTION_SC(out outObj, parent);
			TERMINATOR();
		} else if (StartOf(18)) {
			CODEINSTRUCTION_NSC(out outObj, parent);
		} else SynErr(101);
	}

	void BODY_ASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject parent, bool allowAlt = false) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		if (la.kind == 7) {
			Get();
			obj.Operation = AssignmentCharacters.SimpleAssign; 
			if (StartOf(4)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 14) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(102);
		} else if (allowAlt ) {
			if (la.kind == 9) {
				Get();
				obj.Operation = t.val == "++" ? AssignmentCharacters.PlusOne : AssignmentCharacters.MinusOne; 
			} else if (la.kind == 8) {
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
				} else if (la.kind == 14) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(103);
			} else SynErr(104);
		} else SynErr(105);
	}

	void VARIABLEASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject ident, pBaseLangObject parent) {
		var obj = new AssignContainer(parent); obj.Name = (Ident)ident; ident.Parent = obj; outObj = obj; pBaseLangObject blo; 
		BODY_ASSIGNMENT(out blo, outObj, true);
		obj.assign = (VariableAssignment)blo; 
	}

	void AUTOVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.line, la.col, this.file); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		Expect(60);
		obj.varType = new VarTypeObject(VarType.Auto); 
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		BODY_ASSIGNMENT(out blo, outObj);
		obj.addChild(blo); 
	}

	void CODEINSTRUCTION_SC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null;
		VarTypeObject vto;
		
		if (la.kind == 70) {
			OP_THROW(out outObj, parent);
		} else if (la.kind == 71) {
			OP_RETURN(out outObj, parent);
		} else if (StartOf(1)) {
			VARTYPE(out vto, parent, true);
			if (StartOf(19)) {
				if (vto.IsObject) {
					if (la.kind == 7 || la.kind == 8 || la.kind == 9) {
						VARIABLEASSIGNMENT(out outObj, vto.ident, parent);
						vto.ident.Parent = outObj; 
					} else if (la.kind == 3) {
						NEWVARIABLE(out outObj, parent, vto);
					} else SynErr(106);
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
			
		} else if (la.kind == 60) {
			AUTOVARIABLE(out outObj, parent);
		} else if (la.kind == 76) {
			OP_SQFCALL(out outObj, parent);
		} else SynErr(107);
	}

	void OP_THROW(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Throw(parent); outObj = obj; pBaseLangObject blo; 
		Expect(70);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void OP_RETURN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Return(parent, t.line, t.col, this.file); outObj = obj; pBaseLangObject blo; 
		Expect(71);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
		}
	}

	void CODEINSTRUCTION_NSC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (la.kind == 61) {
			OP_FOR(out outObj, parent);
		} else if (la.kind == 62) {
			OP_WHILE(out outObj, parent);
		} else if (la.kind == 64) {
			OP_IFELSE(out outObj, parent);
		} else if (la.kind == 72) {
			OP_SWITCH(out outObj, parent);
		} else if (la.kind == 66) {
			OP_TRYCATCH(out outObj, parent);
		} else SynErr(108);
	}

	void OP_FOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new For(parent); outObj = obj; pBaseLangObject blo; 
		Expect(61);
		Expect(10);
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
		Expect(11);
		if (la.kind == 14) {
			Get();
			while (StartOf(20)) {
				if (StartOf(3)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(15);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(109);
	}

	void OP_WHILE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new While(parent); outObj = obj; pBaseLangObject blo; 
		Expect(62);
		Expect(10);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(11);
		if (la.kind == 14) {
			Get();
			while (StartOf(20)) {
				if (StartOf(3)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(15);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(110);
	}

	void OP_IFELSE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new IfElse(parent); outObj = obj; pBaseLangObject blo; 
		Expect(64);
		Expect(10);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(11);
		if (la.kind == 14) {
			Get();
			while (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(15);
		} else if (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(111);
		obj.markIfEnd(); 
		if (la.kind == 65) {
			Get();
			if (la.kind == 14) {
				Get();
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				}
				Expect(15);
			} else if (StartOf(3)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(112);
		}
	}

	void OP_SWITCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Switch(parent); Case caseObj; outObj = obj; pBaseLangObject blo; 
		Expect(72);
		Expect(10);
		EXPRESSION(out blo, obj);
		obj.expression = blo; 
		Expect(11);
		Expect(14);
		while (la.kind == 73 || la.kind == 74 || la.kind == 75) {
			if (la.kind == 73) {
				Get();
				caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
				EXPRESSION(out blo, caseObj);
				caseObj.addChild(blo); 
				Expect(59);
				while (la.kind == 73) {
					Get();
					EXPRESSION(out blo, caseObj);
					caseObj.addChild(blo); 
					Expect(59);
				}
				caseObj.markEnd(); 
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, caseObj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 68) {
					OP_BREAK(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 70) {
					OP_THROW(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 71) {
					OP_RETURN(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else SynErr(113);
			} else {
				if (la.kind == 74) {
					Get();
					caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
					Expect(59);
				} else {
					Get();
					caseObj = new Case(obj, t.line, t.col, this.file); obj.addChild(caseObj); 
				}
				while (StartOf(3)) {
					CODEINSTRUCTION(out blo, caseObj);
					caseObj.addChild(blo); 
				}
				if (la.kind == 68) {
					OP_BREAK(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 70) {
					OP_THROW(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else if (la.kind == 71) {
					OP_RETURN(out blo, caseObj);
					caseObj.addChild(blo); 
					TERMINATOR();
				} else SynErr(114);
			}
		}
		Expect(15);
	}

	void OP_TRYCATCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new TryCatch(parent);
		outObj = obj;
		pBaseLangObject blo;
		VarTypeObject vto;
		
		Expect(66);
		Expect(14);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
		obj.markEnd(); 
		Expect(67);
		Expect(10);
		VARTYPE(out vto, obj);
		NEWVARIABLE(out blo, obj, vto);
		obj.variable = blo; 
		Expect(11);
		Expect(14);
		while (StartOf(3)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void OP_NEWARRAY(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewArray(parent); outObj = obj; pBaseLangObject blo; 
		Expect(14);
		if (StartOf(4)) {
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(15);
	}

	void OP_BREAK(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Break(parent); outObj = obj; 
		Expect(68);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		OOS();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _T,_x,_T,_x, _x,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_T, _T,_T,_T,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _T,_x,_x,_T, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_T, _T,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _x,_x,_x,_x, _T,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _T,_x,_T,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _T,_x,_T,_x, _T,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x,_x}

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
			case 6: s = "T_OTHEROP expected"; break;
			case 7: s = "T_ASSIGNMENTCHAR expected"; break;
			case 8: s = "T_EXTENDEDASSIGNMENTCHARS expected"; break;
			case 9: s = "T_FASTASSIGNMENTCHARS expected"; break;
			case 10: s = "T_ROUNDBRACKETOPEN expected"; break;
			case 11: s = "T_ROUNDBRACKETCLOSE expected"; break;
			case 12: s = "T_SQUAREBRACKETOPEN expected"; break;
			case 13: s = "T_SQUAREBRACKETCLOSE expected"; break;
			case 14: s = "T_CODEBRACKETOPEN expected"; break;
			case 15: s = "T_CODEBRACKETCLOSE expected"; break;
			case 16: s = "T_INSTANCEACCESS expected"; break;
			case 17: s = "T_NAMESPACEACCESS expected"; break;
			case 18: s = "T_COMMA expected"; break;
			case 19: s = "T_TEMPLATEOPEN expected"; break;
			case 20: s = "T_TEMPLATECLOSE expected"; break;
			case 21: s = "T_SLASH expected"; break;
			case 22: s = "T_BACKSLASH expected"; break;
			case 23: s = "\"void\" expected"; break;
			case 24: s = "\"async\" expected"; break;
			case 25: s = "\"virtual\" expected"; break;
			case 26: s = "\"external\" expected"; break;
			case 27: s = "\"public\" expected"; break;
			case 28: s = "\"private\" expected"; break;
			case 29: s = "\"protected\" expected"; break;
			case 30: s = "\"scalar\" expected"; break;
			case 31: s = "\"int\" expected"; break;
			case 32: s = "\"double\" expected"; break;
			case 33: s = "\"float\" expected"; break;
			case 34: s = "\"bool\" expected"; break;
			case 35: s = "\"boolean\" expected"; break;
			case 36: s = "\"true\" expected"; break;
			case 37: s = "\"false\" expected"; break;
			case 38: s = "\"!\" expected"; break;
			case 39: s = "\"static\" expected"; break;
			case 40: s = "\"using\" expected"; break;
			case 41: s = "\"enum\" expected"; break;
			case 42: s = "\"namespace\" expected"; break;
			case 43: s = "\"native\" expected"; break;
			case 44: s = "\"flags\" expected"; break;
			case 45: s = "\"disableConstructor\" expected"; break;
			case 46: s = "\"noObjectExtends\" expected"; break;
			case 47: s = "\"extends\" expected"; break;
			case 48: s = "\"assign\" expected"; break;
			case 49: s = "\"simple\" expected"; break;
			case 50: s = "\"endAssign\" expected"; break;
			case 51: s = "\"fnc\" expected"; break;
			case 52: s = "\"endFnc\" expected"; break;
			case 53: s = "\"operator\" expected"; break;
			case 54: s = "\"endOperator\" expected"; break;
			case 55: s = "\"class\" expected"; break;
			case 56: s = "\"virtualFunctionsOnly\" expected"; break;
			case 57: s = "\"implements\" expected"; break;
			case 58: s = "\"interface\" expected"; break;
			case 59: s = "\":\" expected"; break;
			case 60: s = "\"auto\" expected"; break;
			case 61: s = "\"for\" expected"; break;
			case 62: s = "\"while\" expected"; break;
			case 63: s = "\"new\" expected"; break;
			case 64: s = "\"if\" expected"; break;
			case 65: s = "\"else\" expected"; break;
			case 66: s = "\"try\" expected"; break;
			case 67: s = "\"catch\" expected"; break;
			case 68: s = "\"break\" expected"; break;
			case 69: s = "\"is\" expected"; break;
			case 70: s = "\"throw\" expected"; break;
			case 71: s = "\"return\" expected"; break;
			case 72: s = "\"switch\" expected"; break;
			case 73: s = "\"case\" expected"; break;
			case 74: s = "\"default\" expected"; break;
			case 75: s = "\"default:\" expected"; break;
			case 76: s = "\"SQF\" expected"; break;
			case 77: s = "\"as\" expected"; break;
			case 78: s = "??? expected"; break;
			case 79: s = "invalid VARTYPE"; break;
			case 80: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 81: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 82: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 83: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 84: s = "invalid SPECIAL_NEWVARIABLEFUNCTION"; break;
			case 85: s = "invalid ENCAPSULATION"; break;
			case 86: s = "invalid BOOLEAN"; break;
			case 87: s = "invalid VALUE"; break;
			case 88: s = "invalid EXPRESSION_HELPER"; break;
			case 89: s = "invalid OP_SQFCALL"; break;
			case 90: s = "invalid OP_SQFCALL"; break;
			case 91: s = "invalid OP_SQFCALL"; break;
			case 92: s = "invalid OP_SQFCALL"; break;
			case 93: s = "invalid EXPRESSION"; break;
			case 94: s = "invalid OOS"; break;
			case 95: s = "invalid NAMESPACE"; break;
			case 96: s = "invalid CLASS"; break;
			case 97: s = "invalid NATIVEFUNCTION"; break;
			case 98: s = "invalid NATIVEOPERATOR"; break;
			case 99: s = "invalid NATIVEOPERATOR"; break;
			case 100: s = "invalid VFUNCTION"; break;
			case 101: s = "invalid CODEINSTRUCTION"; break;
			case 102: s = "invalid BODY_ASSIGNMENT"; break;
			case 103: s = "invalid BODY_ASSIGNMENT"; break;
			case 104: s = "invalid BODY_ASSIGNMENT"; break;
			case 105: s = "invalid BODY_ASSIGNMENT"; break;
			case 106: s = "invalid CODEINSTRUCTION_SC"; break;
			case 107: s = "invalid CODEINSTRUCTION_SC"; break;
			case 108: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 109: s = "invalid OP_FOR"; break;
			case 110: s = "invalid OP_WHILE"; break;
			case 111: s = "invalid OP_IFELSE"; break;
			case 112: s = "invalid OP_IFELSE"; break;
			case 113: s = "invalid OP_SWITCH"; break;
			case 114: s = "invalid OP_SWITCH"; break;

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
