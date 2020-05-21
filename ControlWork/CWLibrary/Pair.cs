using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWLibrary
{
    [Serializable]
    public class Pair<T, U> : IComparable where T : IComparable
    {
        public T t;
        public U u;

        public Pair(T t, U u)
        {
            this.t = t;
            this.u = u;
        }

        public int CompareTo(object obj)
        {
            var otherObj = (Pair<T, U>)obj;
            return t.CompareTo(otherObj.t);
        }

        public override string ToString()
        {
            return $"{t} {u}";
        }
    }
}
