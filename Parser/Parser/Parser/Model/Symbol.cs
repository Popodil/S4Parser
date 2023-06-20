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
        public int LeftBindingPower { get; set; } = 0;
        public bool Assignment { get; set; }
        public SymbolType SymbolType { get; set; }
        public SymbolType? SecondType { get; set; }

        public Symbol (string id, int leftBindingPower = 0)
        {
            Id = id;
            LeftBindingPower = leftBindingPower;
        }
        public Symbol (string id, SymbolType type, int leftBindingPower = 0, SymbolType? secondType = SymbolType.none)
        {
            Id = id;
            SymbolType = type;
            LeftBindingPower = leftBindingPower;
            SecondType = secondType;
        }
    }

    public enum SymbolType
    {
        infix,
        infixR,
        prefix,
        assignment,
        statement,
        symbol,
        none
    }
}
