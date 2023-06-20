using Parser.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Tree
    {
        public List<Base> bases = new();
        public void AddBase(Base toAdd)
        {
            bases.Add(toAdd);
        }

        public override string? ToString()
        {
            string str = "";
            foreach (Base item in bases)
            {
                str += $"{item}\r\n";
            }
            return str;
        }
    }
}
