using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Statement
    {
        private Parse parse;
        public Statement(Parse parse)
        {
            this.parse = parse;
        }
        public Base StatementFunc(Base first, Base main) 
        {
            if (main.token.Id == "let") return Led(first);
            else return Std(first);
        }

        private Base Led(Base first)
        {
            parse.Advance();
            Base main = new(parse.token);
            if (parse.token.Id == "=")
            {
                main.token = parse.token;
                parse.Advance("=");
                main.First = first;
                main.Second = parse.Expression(0);
                if (parse.token.Id == ",") parse.Advance(",");
            }
            parse.Advance(";");
            return main;
        }

        private Base Std(Base first)
        {
            Base second = parse.Expression(first.token.LeftBindingPower) ?? throw new Exception("Couldn't get second");
            first.AddBase(first, second);
            return first;
        }
    }
}
