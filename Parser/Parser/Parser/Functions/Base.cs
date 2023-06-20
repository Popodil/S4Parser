using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Base
    {
        public Token token;
        public Base? First = null;
        public Base? Second = null;

        public Base(Token token)
        {
            this.token = token;
        }

        public void AddBase(Base first, Base second)
        {
            First = first;
            Second = second;
        }

        public bool HasFirst()
        {
            if (First == null) return false;
            return true;
        }

        public override string ToString()
        {
            string str = $"Value: {token}\r\n";
            if (First != null) str += $"First: {First}\r\n";
            if (Second != null) str += $"Second: {Second}\r\n";
            return str;
        }
    }
}
