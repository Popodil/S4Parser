using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Symbol : OriginalSymbol
    {
        public string Id { get; set; }
        public int LeftBindingPower { get; set; }
        public bool Assignment { get; set; }
    }
}
