using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedScripting
{
    class Version
    {
        private int _main;
        private int _side;
        private int _rev;
        public int Main { get { return this._main; } set { this._main = value; } }
        public int Side { get { return this._side; } set { this._side = value; } }
        public int Revision { get { return this._rev; } set { this._rev = value; } }
        public Version(string s)
        {
            string[] sArr = s.Split('.');
            switch(sArr.Length)
            {
                default:
                    throw new ArgumentException("Unknown Version Syntax");
                case 3:
                    this.Revision = Convert.ToInt32(sArr[2]);
                    goto case 2;
                case 2:
                    this.Side = Convert.ToInt32(sArr[1]);
                    goto case 1;
                case 1:
                    this.Main = Convert.ToInt32(sArr[0]);
                    break;
            }
        }
        public override string ToString()
        {
            return _main + "." + _side + "." + _rev;
        }
        public static bool operator <(Version x, Version y)
        {
            if (x._main < y._main)
                return true;
            else if (x._main > y._main)
                return false;

            if (x._side < y._side)
                return true;
            else if (x._side > y._side)
                return false;

            if (x._rev < y._rev)
                return true;
            return false;
        }
        public static bool operator >(Version x, Version y)
        {
            if (x._main > y._main)
                return true;
            else if (x._main < y._main)
                return false;

            if (x._side > y._side)
                return true;
            else if (x._side < y._side)
                return false;

            if (x._rev > y._rev)
                return true;
            return false;
        }
        public static bool operator ==(Version x, Version y)
        {
            return x._main == y._main && x._side == y._side && x._rev == y._rev;
        }
        public static bool operator !=(Version x, Version y)
        {
            return x._main != y._main && x._side != y._side && x._rev != y._rev;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
