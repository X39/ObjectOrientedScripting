using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    public class ArgList
    {
        public static bool matchesArglist(List<VarTypeObject> lArgs, List<VarTypeObject> rArgs, Variable lVar = null)
        {
            if (lArgs.Count != rArgs.Count)
                return false;
            bool flag = true;
            if (lVar == null || lVar.TemplateObject == null)
            {
                for (int i = 0; i < lArgs.Count; i++)
                {
                    var lIt = lArgs[i];
                    var rIt = rArgs[i];
                    if (!lIt.Equals(rIt))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else
            {
                Template toResolved = lVar.TemplateObject;
                Template toAnonymous = ((Interfaces.iTemplate)lVar.varType.ident.LastIdent.ReferencedObject).TemplateObject;
                if (toResolved.vtoList.Count != toAnonymous.vtoList.Count)
                    throw new Exception();
                for (int i = 0; i < lArgs.Count; i++)
                {
                    var lIt = resolveVarTypeObject(lArgs[i], toAnonymous, toResolved);
                    var rIt = rArgs[i];

                    if (!lIt.Equals(rIt))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
        public static VarTypeObject resolveVarTypeObject(VarTypeObject objToDeref, Template anonymous, Template resolved)
        {
            if (anonymous == null || resolved == null)
                return objToDeref;
            if (resolved.vtoList.Count != anonymous.vtoList.Count)
                throw new Exception();
            for (int i = 0; i < anonymous.vtoList.Count; i++)
            {
                if (objToDeref.Equals(anonymous.vtoList[i]))
                    return resolved.vtoList[i];
            }
            return objToDeref;
        }
    }
}
