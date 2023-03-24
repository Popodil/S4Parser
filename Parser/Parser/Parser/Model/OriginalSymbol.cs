using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class OriginalSymbol
    {
        public object First { get; set; }
        public object Second { get; set; }
        public object Arity { get; set; }

        public object LeftDenotation(object left)
        {
            First = left;
            return this;
        }

        public object NullDenotation()
        {
            throw new NotImplementedException();
        }
    }
}
