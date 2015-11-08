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
	public const int _T_STATICCASTOPERATOR = 19;
	public const int _T_DYNAMICCASTOPERATOR = 20;
	public const int _T_TEMPLATEOPEN = 21;
	public const int _T_TEMPLATECLOSE = 22;
	public const int _T_SLASH = 23;
	public const int _T_BACKSLASH = 24;
	public const int maxT = 77;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	public Base BaseObject { get; set;}
	
	public Parser(Scanner scanner) {
		this.scanner = scanner;
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

	void TEMPLATE(out Template obj, pBaseLangObject parent) {
		obj = new Template(parent, t.line, t.col); pBaseLangObject blo; VarType e; 
		Expect(21);
		if (StartOf(1)) {
			IDENTACCESS(out blo, obj);
			obj.vtoList.Add(new VarTypeObject((Ident)blo)); 
		} else if (StartOf(2)) {
			VARTYPE(out e);
			obj.vtoList.Add(new VarTypeObject(e)); 
		} else SynErr(78);
		while (la.kind == 18) {
			Get();
			if (StartOf(1)) {
				IDENTACCESS(out blo, obj);
				obj.vtoList.Add(new VarTypeObject((Ident)blo)); 
			} else if (StartOf(2)) {
				VARTYPE(out e);
				obj.vtoList.Add(new VarTypeObject(e)); 
			} else SynErr(79);
		}
		Expect(22);
	}

	void IDENTACCESS(out pBaseLangObject outObj, pBaseLangObject parent, bool allowBody = true) {
		pBaseLangObject blo; pBaseLangObject ident; outObj = null; bool isGlobalIdent = false; 
		if (la.kind == 17) {
			Get();
			isGlobalIdent = true; 
		}
		if (la.kind == 19 || la.kind == 20) {
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

	void VARTYPE(out VarType e) {
		e = VarType.Void; 
		switch (la.kind) {
		case 28: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 29: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 30: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 31: {
			Get();
			e = VarType.Scalar; 
			break;
		}
		case 32: {
			Get();
			e = VarType.Bool; 
			break;
		}
		case 33: {
			Get();
			e = VarType.Bool; 
			break;
		}
		case 34: {
			Get();
			e = VarType.String; 
			break;
		}
		case 35: {
			Get();
			e = VarType.Object; 
			break;
		}
		default: SynErr(80); break;
		}
		if (la.kind == 12) {
			Get();
			Expect(13);
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

	void IDENT(out pBaseLangObject outObj, pBaseLangObject parent) {
		Expect(3);
		outObj = new Ident(parent, t.val, t.line, t.col); 
	}

	void CAST(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Cast(parent); outObj = obj; VarType vt; pBaseLangObject ident; 
		if (la.kind == 19) {
			Get();
			obj.isStaticCast = true; 
			if (StartOf(2)) {
				VARTYPE(out vt);
				obj.varType = new VarTypeObject(vt); 
			} else if (StartOf(1)) {
				IDENTACCESS(out ident, obj);
				obj.varType = new VarTypeObject((Ident)ident); 
			} else SynErr(81);
			Expect(19);
		} else if (la.kind == 20) {
			Get();
			obj.isStaticCast = false; 
			if (StartOf(2)) {
				VARTYPE(out vt);
				obj.varType = new VarTypeObject(vt); 
			} else if (StartOf(1)) {
				IDENTACCESS(out ident, obj);
				obj.varType = new VarTypeObject((Ident)ident); 
			} else SynErr(82);
			Expect(20);
		} else SynErr(83);
	}

	void BODY_FUNCTIONCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new FunctionCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(10);
		if (StartOf(3)) {
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
		if (la.kind == 25) {
			Get();
			e = Encapsulation.Public; 
		} else if (la.kind == 26) {
			Get();
			e = Encapsulation.Private; 
		} else if (la.kind == 27) {
			Get();
			e = Encapsulation.Protected; 
		} else SynErr(84);
	}

	void BOOLEAN(out bool flag) {
		flag = la.val == "true"; Get(); return; 
		if (la.kind == 36) {
			Get();
			flag = true; 
		} else if (la.kind == 37) {
			Get();
			flag = false; 
		} else SynErr(85);
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
		} else if (la.kind == 36 || la.kind == 37) {
			BOOLEAN(out flag);
			obj.varType = VarType.Bool; obj.value = (flag ? "true" : "false"); 
		} else SynErr(86);
	}

	void EXPRESSION_HELPER(out pBaseLangObject outObj, pBaseLangObject parent, bool flag) {
		var obj = new Expression(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; pBaseLangObject blo2; obj.negate = flag; 
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
		} else if (StartOf(4)) {
			VALUE(out blo, obj);
			obj.lExpression = blo; 
		} else if (StartOf(1)) {
			IDENTACCESS(out blo, obj);
			obj.lExpression = blo; 
			if (la.kind == 69) {
				OP_INSTANCEOF(out blo, obj, blo);
				obj.lExpression = blo; 
			}
		} else if (la.kind == 76) {
			OP_SQFCALL(out blo, obj);
			obj.lExpression = blo; 
		} else SynErr(87);
		while (StartOf(5)) {
			if (la.kind == 5) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 23) {
				Get();
				obj.expOperator = t.val; 
			} else if (la.kind == 21) {
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
		Expect(69);
		IDENTACCESS(out blo, obj);
		obj.RIdent = (Ident)blo; 
	}

	void OP_SQFCALL(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new SqfCall(parent); outObj = obj; pBaseLangObject blo; 
		Expect(76);
		if (la.kind == 10) {
			Get();
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(11);
		}
		IDENT(out blo, outObj);
		try{ obj.Name = (Ident)blo;} catch (Exception ex) { SemErr(ex.Message); } 
		if (la.kind == 10) {
			Get();
			obj.markEnd(); 
			EXPRESSION(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(11);
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
		} else if (StartOf(6)) {
			EXPRESSION_HELPER(out outObj, parent, negate);
		} else SynErr(88);
	}

	void OOS() {
		if(this.BaseObject == null) throw new Exception("BaseObject was never set"); var obj = this.BaseObject; pBaseLangObject blo; 
		if (la.kind == 40) {
			OP_USING();
			while (la.kind == 40) {
				OP_USING();
			}
		}
		while (StartOf(7)) {
			if (la.kind == 42) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 53) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 43) {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 56) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else if (StartOf(8)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(89);
			}
		}
	}

	void OP_USING() {
		List<string> identList = new List<string>(); bool isLocal = false; 
		Expect(40);
		if (la.kind == 41) {
			isLocal = true; 
			Get();
			Expect(3);
			identList.Add(t.val); 
			while (la.kind == 17) {
				Get();
				Expect(3);
				identList.Add(t.val); 
			}
			Expect(41);
		} else if (la.kind == 21) {
			Get();
			Expect(3);
			identList.Add(t.val); 
			while (la.kind == 17) {
				Get();
				Expect(3);
				identList.Add(t.val); 
			}
			Expect(22);
		} else SynErr(90);
		if (errDist >= minErrDist)
		{
		   string lookupPath = isLocal ? Wrapper.Compiler.ProjectFile.ProjectPath : Wrapper.Compiler.stdLibPath;
		   string currentFile = lookupPath;
		   bool flag = true;
		   foreach (var it in identList)
		   {
		       string tmp = lookupPath + it;
		       if (Directory.Exists(tmp))
		       {
		           lookupPath += it + '\\';
		       }
               else if (Directory.EnumerateFiles(lookupPath).Any(file =>
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
		           if (identList.Last() != it)
		           {
		               flag = false;
		               SemErr("Invalid Operation, hit file before end: " + lookupPath);
		               break;
		           }
		       }
		       else
		       {
		           flag = false;
		           SemErr("Invalid Operation, path could not be dereferenced: " + lookupPath);
		           break;
		       }
		   }
		   if (flag)
		   {
		       if (!UsedFiles.Contains(currentFile))
		       {
		           UsedFiles.Add(currentFile);
		           Scanner scanner = new Scanner(currentFile);
		           Base baseObject = new Base();
		           Parser p = new Parser(scanner);
		           p.BaseObject = this.BaseObject;
		           p.Parse();
		           if (p.errors.count > 0)
		           {
		               this.errors.count += p.errors.count;
		               Logger.Instance.log(Logger.LogLevel.ERROR, "In file '" + lookupPath + "'");
		           }
		       }
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
		while (StartOf(7)) {
			if (la.kind == 42) {
				NAMESPACE(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 53) {
				CLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 43) {
				NATIVECLASS(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 56) {
				INTERFACE(out blo, obj);
				obj.addChild(blo); 
			} else {
				Get();
				if (peekCompare(-1, -1, _T_TERMINATOR) ) {
					NEWVARIABLE(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
					TERMINATOR();
				} else if (StartOf(8)) {
					FUNCTION(out blo, obj, Encapsulation.Static);
					obj.addChild(blo); 
				} else SynErr(91);
			}
		}
		Expect(15);
	}

	void CLASS(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosClass(parent); outObj = obj; pBaseLangObject blo; Encapsulation e = Encapsulation.Private; bool hasConstructor = false; 
		Expect(53);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 54) {
			Get();
			IDENTACCESS(out blo, obj, false);
			obj.addParentClass((Ident)blo); 
			while (la.kind == 18) {
				Get();
				IDENTACCESS(out blo, obj, false);
				obj.addParentClass((Ident)blo); 
			}
		}
		obj.markExtendsEnd(); 
		if (la.kind == 55) {
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
		while (StartOf(9)) {
			e = Encapsulation.Private; 
			if (StartOf(10)) {
				if (la.kind == 25 || la.kind == 26 || la.kind == 27) {
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
			} else if (StartOf(8)) {
				FUNCTION(out blo, obj, e);
				obj.addChild(blo); 
			} else SynErr(92);
		}
		Expect(15);
		if(!hasConstructor) {
		            var constructor = new Function(obj);
		constructor.encapsulation = Encapsulation.Public;
		            try
		            {
		              constructor.Name = new Ident(constructor, obj.Name.OriginalValue, obj.Name.Line, obj.Name.Pos);
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
		var obj = new Native(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; Template te; bool flag = false; 
		Expect(43);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		if (la.kind == 21) {
			TEMPLATE(out te, obj);
			obj.TemplateObject = te; flag = true; obj.VTO = new VarTypeObject(obj.Name, true, te); 
		}
		if(!flag) obj.VTO = new VarTypeObject(obj.Name, true); 
		Expect(14);
		NATIVEASSIGN(out blo, obj);
		obj.addChild(blo); 
		while (la.kind == 44 || la.kind == 47 || la.kind == 51) {
			if (la.kind == 44) {
				NATIVEASSIGN(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 47) {
				NATIVEFUNCTION(out blo, obj);
				obj.addChild(blo); 
			} else {
				NATIVEOPERATOR(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(15);
	}

	void INTERFACE(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new oosInterface(parent); outObj = obj; pBaseLangObject blo; 
		Expect(56);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; obj.VTO = new VarTypeObject((Ident)blo, true); 
		Expect(14);
		while (StartOf(11)) {
			VFUNCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void NEWVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.col, la.line); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		if (StartOf(2)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (StartOf(12)) {
			bool isStrict = false; 
			if (la.kind == 49) {
				Get();
				isStrict = true; 
			}
			IDENTACCESS(out blo, obj);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, outObj);
				obj.varType.TemplateObject = te; 
			}
		} else SynErr(93);
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		if (la.kind == 7 || la.kind == 8 || la.kind == 9) {
			BODY_ASSIGNMENT(out blo, outObj);
			obj.addChild(blo); 
		}
	}

	void FUNCTION(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; VarType v; 
		if (la.kind == 57) {
			Get();
			obj.IsAsync = true; 
		}
		if (la.kind == 58) {
			Get();
			obj.Override = true; 
		}
		if (StartOf(2)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (la.kind == 48) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else if (StartOf(12)) {
			bool isStrict = false; 
			if (la.kind == 49) {
				Get();
				isStrict = true; 
			}
			IDENTACCESS(out blo, obj, false);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, outObj);
				obj.varType.TemplateObject = te; 
			}
		} else SynErr(94);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(10);
		if (StartOf(13)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
		obj.markArgListEnd(); 
		Expect(14);
		while (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void NATIVEASSIGN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeAssign(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; 
		obj.Name = new Ident(obj, ((Native)parent).Name.OriginalValue, ((Native)parent).Name.Line, ((Native)parent).Name.Pos); 
		Expect(44);
		if (la.kind == 45) {
			Get();
			obj.IsSimple = true; 
		}
		Expect(10);
		if (StartOf(13)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
		while (StartOf(15)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(46);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeFunction(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; VarType v; 
		Expect(47);
		if (la.kind == 45) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(2)) {
			VARTYPE(out v);
			obj.VTO = new VarTypeObject(v); 
		} else if (la.kind == 48) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else if (StartOf(12)) {
			bool isStrict = false; 
			if (la.kind == 49) {
				Get();
				isStrict = true; 
			}
			IDENTACCESS(out blo, obj);
			obj.VTO = new VarTypeObject((Ident)blo, isStrict); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, outObj);
				obj.VTO.TemplateObject = te; 
			}
		} else SynErr(95);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(10);
		if (StartOf(13)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
		while (StartOf(16)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(50);
		obj.Code = obj.Code.Trim(); 
	}

	void NATIVEOPERATOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NativeOperator(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; VarType v; 
		Expect(51);
		if (la.kind == 45) {
			Get();
			obj.IsSimple = true; 
		}
		if (StartOf(2)) {
			VARTYPE(out v);
			obj.VTO = new VarTypeObject(v); 
		} else if (la.kind == 48) {
			Get();
			obj.VTO = new VarTypeObject(VarType.Void); 
		} else if (StartOf(12)) {
			bool isStrict = false; 
			if (la.kind == 49) {
				Get();
				isStrict = true; 
			}
			IDENTACCESS(out blo, obj, false);
			obj.VTO = new VarTypeObject((Ident)blo, isStrict); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, outObj);
				obj.VTO.TemplateObject = te; 
			}
		} else SynErr(96);
		if (la.kind == 12) {
			Get();
			Expect(13);
			obj.OperatorType = OverridableOperator.ArrayAccess; 
		} else if (StartOf(5)) {
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
			} else if (la.kind == 23) {
				Get();
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			} else if (la.kind == 21) {
				Get();
				Expect(21);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			} else {
				Get();
				Expect(22);
				SemErr("The operator '" + t.val + "' is not supported for override"); 
			}
		} else SynErr(97);
		Expect(10);
		if (StartOf(13)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
		while (StartOf(17)) {
			Get();
			obj.Code += t.val + (la.val == ";" ? "" : " "); 
		}
		Expect(52);
		obj.Code = obj.Code.Trim(); 
	}

	void CONSTRUCTOR(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e) {
		var obj = new Function(parent); obj.varType = new VarTypeObject(((oosClass)parent).Name, true); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(10);
		if (StartOf(13)) {
			NEWVARIABLE(out blo, obj);
			obj.addChild(blo); 
			while (la.kind == 18) {
				Get();
				NEWVARIABLE(out blo, obj);
				obj.addChild(blo); 
			}
		}
		Expect(11);
		obj.markArgListEnd(); 
		if (la.kind == 59) {
			Get();
			IDENTACCESS(out blo, obj);
			obj.addChild(blo); 
			while (StartOf(1)) {
				IDENTACCESS(out blo, obj);
				obj.addChild(blo); 
			}
		}
		obj.markBaseCallEnd(); 
		Expect(14);
		while (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void VFUNCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new VirtualFunction(parent); outObj = obj; pBaseLangObject blo; VarType v; 
		if (la.kind == 57) {
			Get();
			obj.IsAsync = true; 
		}
		if (StartOf(2)) {
			VARTYPE(out v);
			obj.varType = new VarTypeObject(v); 
		} else if (la.kind == 48) {
			Get();
			obj.varType = new VarTypeObject(VarType.Void); 
		} else if (StartOf(12)) {
			bool isStrict = false; 
			if (la.kind == 49) {
				Get();
				isStrict = true; 
			}
			IDENTACCESS(out blo, obj, false);
			obj.varType = new VarTypeObject((Ident)blo, isStrict); 
			if (la.kind == 21) {
				Template te; 
				TEMPLATE(out te, outObj);
				obj.varType.TemplateObject = te; 
			}
		} else SynErr(98);
		IDENT(out blo, obj);
		obj.Name = (Ident)blo; 
		Expect(10);
		if (StartOf(18)) {
			if (StartOf(2)) {
				VARTYPE(out v);
				obj.argTypes.Add(new VarTypeObject(v)); 
			} else {
				IDENTACCESS(out blo, obj, false);
				obj.argTypes.Add(new VarTypeObject((Ident)blo)); 
			}
			while (la.kind == 18) {
				Get();
				if (StartOf(2)) {
					VARTYPE(out v);
					obj.argTypes.Add(new VarTypeObject(v)); 
				} else if (StartOf(1)) {
					IDENTACCESS(out blo, obj, false);
					obj.argTypes.Add(new VarTypeObject((Ident)blo)); 
				} else SynErr(99);
			}
		}
		Expect(11);
		TERMINATOR();
	}

	void CODEINSTRUCTION(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; 
		if (StartOf(19)) {
			CODEINSTRUCTION_SC(out outObj, parent);
			TERMINATOR();
		} else if (StartOf(20)) {
			CODEINSTRUCTION_NSC(out outObj, parent);
		} else SynErr(100);
	}

	void BODY_ASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject parent, bool allowAlt = false) {
		var obj = new VariableAssignment(parent); outObj = obj; pBaseLangObject blo; 
		if (la.kind == 7) {
			Get();
			obj.Operation = AssignmentCharacters.SimpleAssign; 
			if (StartOf(3)) {
				EXPRESSION(out blo, obj);
				obj.addChild(blo); 
			} else if (la.kind == 14) {
				OP_NEWARRAY(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(101);
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
				
				if (StartOf(3)) {
					EXPRESSION(out blo, obj);
					obj.addChild(blo); 
				} else if (la.kind == 14) {
					OP_NEWARRAY(out blo, obj);
					obj.addChild(blo); 
				} else SynErr(102);
			} else SynErr(103);
		} else SynErr(104);
	}

	void VARIABLEASSIGNMENT(out pBaseLangObject outObj, pBaseLangObject ident, pBaseLangObject parent) {
		var obj = new AssignContainer(parent); obj.Name = (Ident)ident; ident.Parent = obj; outObj = obj; pBaseLangObject blo; 
		BODY_ASSIGNMENT(out blo, outObj, true);
		obj.assign = (VariableAssignment)blo; 
	}

	void AUTOVARIABLE(out pBaseLangObject outObj, pBaseLangObject parent, Encapsulation e = Encapsulation.NA) {
		var obj = new Variable(parent, la.col, la.line); obj.encapsulation = e; outObj = obj; pBaseLangObject blo; 
		Expect(60);
		obj.varType = new VarTypeObject(VarType.Auto); 
		IDENT(out blo, outObj);
		obj.Name = (Ident)blo; 
		BODY_ASSIGNMENT(out blo, outObj);
		obj.addChild(blo); 
	}

	void CODEINSTRUCTION_SC(out pBaseLangObject outObj, pBaseLangObject parent) {
		outObj = null; pBaseLangObject blo; 
		if (la.kind == 70) {
			OP_THROW(out outObj, parent);
		} else if (la.kind == 71) {
			OP_RETURN(out outObj, parent);
		} else if ((peekString(0, "scalar", "int", "double", "float", "bool", "string", "object") && peekCompare(-1, _T_IDENT)) || peekCompare(_T_IDENT, _T_IDENT) ) {
			NEWVARIABLE(out outObj, parent);
			if (la.kind == 7 || la.kind == 8 || la.kind == 9) {
				BODY_ASSIGNMENT(out blo, outObj);
				outObj.addChild(blo); 
			}
		} else if (la.kind == 60) {
			AUTOVARIABLE(out outObj, parent);
		} else if (StartOf(1)) {
			IDENTACCESS(out blo, parent);
			if (la.kind == 7 || la.kind == 8 || la.kind == 9) {
				VARIABLEASSIGNMENT(out outObj, blo, parent);
			}
			if(outObj == null) outObj = blo; 
		} else if (la.kind == 76) {
			OP_SQFCALL(out outObj, parent);
		} else SynErr(105);
	}

	void OP_THROW(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Throw(parent); outObj = obj; pBaseLangObject blo; 
		Expect(70);
		EXPRESSION(out blo, obj);
		obj.addChild(blo); 
	}

	void OP_RETURN(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new Return(parent, t.line, t.col); outObj = obj; pBaseLangObject blo; 
		Expect(71);
		if (StartOf(3)) {
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
		} else SynErr(106);
	}

	void OP_FOR(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new For(parent); outObj = obj; pBaseLangObject blo; 
		Expect(61);
		Expect(10);
		if (StartOf(19)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg1 = blo; 
		}
		TERMINATOR();
		if (StartOf(3)) {
			EXPRESSION(out blo, obj);
			obj.forArg2 = blo; 
		}
		TERMINATOR();
		if (StartOf(19)) {
			CODEINSTRUCTION_SC(out blo, obj);
			obj.forArg3 = blo; 
		}
		Expect(11);
		if (la.kind == 14) {
			Get();
			while (StartOf(21)) {
				if (StartOf(14)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(15);
		} else if (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(107);
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
			while (StartOf(21)) {
				if (StartOf(14)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				} else {
					OP_BREAK(out blo, obj);
					obj.addChild(blo); 
					TERMINATOR();
				}
			}
			Expect(15);
		} else if (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(108);
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
			while (StartOf(14)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			}
			Expect(15);
		} else if (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		} else SynErr(109);
		obj.markIfEnd(); 
		if (la.kind == 65) {
			Get();
			if (la.kind == 14) {
				Get();
				while (StartOf(14)) {
					CODEINSTRUCTION(out blo, obj);
					obj.addChild(blo); 
				}
				Expect(15);
			} else if (StartOf(14)) {
				CODEINSTRUCTION(out blo, obj);
				obj.addChild(blo); 
			} else SynErr(110);
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
				caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); 
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
				while (StartOf(14)) {
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
				} else SynErr(111);
			} else {
				if (la.kind == 74) {
					Get();
					caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); 
					Expect(59);
				} else {
					Get();
					caseObj = new Case(obj, t.line, t.col); obj.addChild(caseObj); 
				}
				while (StartOf(14)) {
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
				} else SynErr(112);
			}
		}
		Expect(15);
	}

	void OP_TRYCATCH(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new TryCatch(parent); outObj = obj; pBaseLangObject blo; 
		Expect(66);
		Expect(14);
		while (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
		Expect(67);
		Expect(10);
		NEWVARIABLE(out blo, obj);
		obj.variable = blo; 
		Expect(11);
		obj.markEnd(); 
		Expect(14);
		while (StartOf(14)) {
			CODEINSTRUCTION(out blo, obj);
			obj.addChild(blo); 
		}
		Expect(15);
	}

	void OP_NEWARRAY(out pBaseLangObject outObj, pBaseLangObject parent) {
		var obj = new NewArray(parent); outObj = obj; pBaseLangObject blo; 
		Expect(14);
		if (StartOf(3)) {
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
		var obj = new Break(parent); outObj = obj; pBaseLangObject blo; 
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
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _T,_x,_T,_x, _x,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x},
		{_x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _x,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_T,_T, _T,_T,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _x,_x,_x,_x, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_T,_x, _T,_x,_T,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_T, _T,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_T,_x, _T,_x,_T,_x, _T,_x,_T,_T, _T,_x,_x,_x, _T,_x,_x}

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
			case 19: s = "T_STATICCASTOPERATOR expected"; break;
			case 20: s = "T_DYNAMICCASTOPERATOR expected"; break;
			case 21: s = "T_TEMPLATEOPEN expected"; break;
			case 22: s = "T_TEMPLATECLOSE expected"; break;
			case 23: s = "T_SLASH expected"; break;
			case 24: s = "T_BACKSLASH expected"; break;
			case 25: s = "\"public\" expected"; break;
			case 26: s = "\"private\" expected"; break;
			case 27: s = "\"protected\" expected"; break;
			case 28: s = "\"scalar\" expected"; break;
			case 29: s = "\"int\" expected"; break;
			case 30: s = "\"double\" expected"; break;
			case 31: s = "\"float\" expected"; break;
			case 32: s = "\"bool\" expected"; break;
			case 33: s = "\"boolean\" expected"; break;
			case 34: s = "\"string\" expected"; break;
			case 35: s = "\"object\" expected"; break;
			case 36: s = "\"true\" expected"; break;
			case 37: s = "\"false\" expected"; break;
			case 38: s = "\"!\" expected"; break;
			case 39: s = "\"static\" expected"; break;
			case 40: s = "\"using\" expected"; break;
			case 41: s = "\"\"\" expected"; break;
			case 42: s = "\"namespace\" expected"; break;
			case 43: s = "\"native\" expected"; break;
			case 44: s = "\"assign\" expected"; break;
			case 45: s = "\"simple\" expected"; break;
			case 46: s = "\"endAssign\" expected"; break;
			case 47: s = "\"fnc\" expected"; break;
			case 48: s = "\"void\" expected"; break;
			case 49: s = "\"strict\" expected"; break;
			case 50: s = "\"endFnc\" expected"; break;
			case 51: s = "\"operator\" expected"; break;
			case 52: s = "\"endOperator\" expected"; break;
			case 53: s = "\"class\" expected"; break;
			case 54: s = "\"extends\" expected"; break;
			case 55: s = "\"implements\" expected"; break;
			case 56: s = "\"interface\" expected"; break;
			case 57: s = "\"async\" expected"; break;
			case 58: s = "\"override\" expected"; break;
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
			case 77: s = "??? expected"; break;
			case 78: s = "invalid TEMPLATE"; break;
			case 79: s = "invalid TEMPLATE"; break;
			case 80: s = "invalid VARTYPE"; break;
			case 81: s = "invalid CAST"; break;
			case 82: s = "invalid CAST"; break;
			case 83: s = "invalid CAST"; break;
			case 84: s = "invalid ENCAPSULATION"; break;
			case 85: s = "invalid BOOLEAN"; break;
			case 86: s = "invalid VALUE"; break;
			case 87: s = "invalid EXPRESSION_HELPER"; break;
			case 88: s = "invalid EXPRESSION"; break;
			case 89: s = "invalid OOS"; break;
			case 90: s = "invalid OP_USING"; break;
			case 91: s = "invalid NAMESPACE"; break;
			case 92: s = "invalid CLASS"; break;
			case 93: s = "invalid NEWVARIABLE"; break;
			case 94: s = "invalid FUNCTION"; break;
			case 95: s = "invalid NATIVEFUNCTION"; break;
			case 96: s = "invalid NATIVEOPERATOR"; break;
			case 97: s = "invalid NATIVEOPERATOR"; break;
			case 98: s = "invalid VFUNCTION"; break;
			case 99: s = "invalid VFUNCTION"; break;
			case 100: s = "invalid CODEINSTRUCTION"; break;
			case 101: s = "invalid BODY_ASSIGNMENT"; break;
			case 102: s = "invalid BODY_ASSIGNMENT"; break;
			case 103: s = "invalid BODY_ASSIGNMENT"; break;
			case 104: s = "invalid BODY_ASSIGNMENT"; break;
			case 105: s = "invalid CODEINSTRUCTION_SC"; break;
			case 106: s = "invalid CODEINSTRUCTION_NSC"; break;
			case 107: s = "invalid OP_FOR"; break;
			case 108: s = "invalid OP_WHILE"; break;
			case 109: s = "invalid OP_IFELSE"; break;
			case 110: s = "invalid OP_IFELSE"; break;
			case 111: s = "invalid OP_SWITCH"; break;
			case 112: s = "invalid OP_SWITCH"; break;

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