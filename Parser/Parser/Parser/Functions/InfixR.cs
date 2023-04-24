using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class InfixR : Base
    {
        public InfixR(string id, int bindingPower, object led = null)
        {
            if (Parse.symbolTable.ContainsKey(id))
            {
                if (Parse.symbolTable[id].LeftBindingPower >= bindingPower)
                {
                    Parse.symbolTable[id].LeftBindingPower = bindingPower;
                }
            }
            else
            {
                Parse.symbolTable.Add(id, new Token()
                {
                    Id = id,
                    Value = id,
                    LeftBindingPower = bindingPower
                }) ;
            }
        }
    }
}
