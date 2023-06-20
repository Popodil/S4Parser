using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Prefix
    {
        private Parse parse;
        public Prefix(Parse parse)
        {
            this.parse = parse;
        }

        public Base Nud(Base first, Base main)
        {
            if (main.token.Id == "(")
            {
                return SpecialNud(main);
            }
            else if (main.token.Id == "[")
            {
                return DefaultNud(first, main);
            }
            else if (main.token.Id == "{")
            {
                return DefaultNud(first, main);
            }
            else
            {
                return DefaultNud(first, main);
            }
        }

        private Base SpecialNud(Base main)
        {
            Base second = parse.Expression(0) ?? throw new Exception("Couldn't get second");
            parse.Advance(")");
            return main;
        }

        private Base DefaultNud(Base first, Base main)
        {
            Base second = parse.Expression(main.token.LeftBindingPower) ?? throw new Exception("Couldn't get second");
            main.AddBase(first, second);
            return main;
        }
    }
}
