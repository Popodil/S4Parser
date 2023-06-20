using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class InfixR
    {
        private Parse parse;
        public InfixR(Parse parse)
        {
            this.parse = parse;
        }
        public Base Led(Base first, Base main)
        {
            Base Second = parse.Expression(main.token.LeftBindingPower - 1) ?? throw new Exception("Couldn't get second");
            main.AddBase(first, Second);
            return main;
        }
    }
}
