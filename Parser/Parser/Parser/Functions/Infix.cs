using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Infix
    {
        private Parse parse;
        public Infix(Parse parse)
        {
            this.parse = parse;
        }
        public Base Led(Base first, Base main)
        {
            Base Second = parse.Expression(main.token.LeftBindingPower) ?? throw new Exception("Couldn't get second");
            main.AddBase(first, Second);
            return main;
        }
    }
}
