using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    public class NamespaceResolver
    {
        private string origString;
        private List<pBaseLangObject> objectTree;
        public int LayerCount { get { return objectTree.Count; } }
        public pBaseLangObject Reference { get { return objectTree.Last(); } }
        public bool IsValid { get; internal set; }
        public static Base BaseClass { get; set; }
        public static implicit operator NamespaceResolver(Ident input)
        {
            return createNSR(input);
        }
        public static implicit operator NamespaceResolver(string input)
        {
            return createNSR(input);
        }
        private NamespaceResolver(string origString)
        {
            this.origString = origString;
            objectTree = new List<pBaseLangObject>();
            var sArr = this.origString.Split(new char[] { ':', '.' });
            pBaseLangObject curObject = BaseClass;
            objectTree.Add(BaseClass);
            bool flag = false;
            foreach (var s in sArr)
            {
                flag = false;
                if (string.IsNullOrEmpty(s))
                    continue;
                foreach (var it in curObject.getAllChildrenOf<Interfaces.iName>())
                {
                    if (it.Name != null && it.Name.OriginalValue == s)
                    {
                        objectTree.Add((pBaseLangObject)it);
                        curObject = (pBaseLangObject)it;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            this.IsValid = flag;
        }

        public bool isInNamespace(NamespaceResolver nsr)
        {
            throw new NotImplementedException();
        }
        public bool isSame(NamespaceResolver nsr)
        {
            if (nsr.LayerCount != this.LayerCount)
                return false;
            bool flag = true;
            for (int i = 0; i < this.LayerCount; i++)
            {
                var it1 = this.objectTree[i];
                var it2 = nsr.objectTree[i];
                if (!it1.Equals(it2))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        public static NamespaceResolver createNSR(Ident ident, bool takeFirst = false)
        {
            return createNSR(ident.FullyQualifiedName, takeFirst);
        }
        //public static NamespaceResolver createNSR(string fqn, string origVal, bool takeFirst = false)
        //{
        //    NamespaceResolver nsr = new NamespaceResolver(fqn);
        //    if (takeFirst)
        //        return nsr;
        //    while (!nsr.IsValid)
        //    {
        //        if (nsr.Reference == null)
        //            return null;
        //        if(nsr.LayerCount > 1)
        //        {
        //
        //        }
        //        if (nsr.Reference.Parent is Interfaces.iName)
        //            nsr = new HelperClasses.NamespaceResolver(((Interfaces.iName)nsr.Reference.Parent).Name.FullyQualifiedName + "." + origVal);
        //        else
        //            return null;
        //    }
        //    return nsr;
        //}
        public static NamespaceResolver createNSR(string fqn, bool takeFirst = false)
        {
            NamespaceResolver nsr = new NamespaceResolver(fqn);
            if (takeFirst)
                return nsr;
            int index = fqn.LastIndexOf(':');
            string prQN = index > 0 ? fqn.Substring(fqn.LastIndexOf(':') + 1) : fqn;
            Interfaces.iName lastRef = nsr.Reference is Interfaces.iName ? (Interfaces.iName)nsr.Reference : null;
            if (lastRef == null)
                return null;
            while (!nsr.IsValid)
            {
                if (nsr.Reference == null)
                    return null;
                if (((pBaseLangObject)lastRef).Parent is Interfaces.iName)
                {
                    lastRef = (Interfaces.iName)((pBaseLangObject)lastRef).Parent;
                    nsr = new HelperClasses.NamespaceResolver(lastRef.Name.FullyQualifiedName + "." + prQN);
                }
                else if (((pBaseLangObject)lastRef).Parent is Base)
                {
                    nsr = new HelperClasses.NamespaceResolver(prQN);
                    if (nsr.IsValid)
                    {
                        return nsr;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return nsr;
        }



        public static List<Interfaces.iFunction> getFunctionReferenceOfFQN(NamespaceResolver nsr)
        {
            List<Interfaces.iFunction> retList = new List<Interfaces.iFunction>();
            if (nsr == null)
                return retList;
            if (!nsr.IsValid)
                return retList;
            var obj = nsr.objectTree.Last().Parent;
            if (nsr.objectTree.Last() is Interfaces.iClass)
                obj = nsr.objectTree.Last();
            foreach (var it in obj.getAllChildrenOf<Interfaces.iFunction>())
            {
                if (nsr.isSame(it.Name))
                {
                    retList.Add(it);
                }
            }
            return retList;
        }
        public static Interfaces.iClass getClassReferenceOfFQN(NamespaceResolver nsr)
        {
            if (nsr == null)
                return null;
            if (!nsr.IsValid)
                return null;
            return (Interfaces.iClass)nsr.objectTree.Last();
        }
        public static Variable getVariableReferenceOfFQN(NamespaceResolver nsr, bool localVariables = true, pBaseLangObject blo = null)
        {
            if (nsr == null)
                return null;
            if (!nsr.IsValid && !localVariables)
                return null;
            if (!nsr.IsValid)
            {
                if (blo == null)
                    return null;
                pBaseLangObject curObj = blo;
                List<Variable> privateVarList = new List<Variable>();
                while (true)
                {
                    curObj = (pBaseLangObject)curObj.getFirstOf<Interfaces.iCodeBlock>(false);
                    if (curObj == null)
                        break;
                    privateVarList.AddRange(curObj.getAllChildrenOf<Variable>());
                }
                foreach (var it in privateVarList)
                {
                    if (nsr.origString.EndsWith(':' + it.Name.OriginalValue))
                        return it;
                }
                return null;
            }
            else
            {
                if (nsr.objectTree.Last() is Variable)
                    return (Variable)nsr.objectTree.Last();
                else
                    return null;
            }
        }
        public override string ToString()
        {
            return "NSR->" + this.origString;
        }
    }
}
