using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Assignment : Symbol
    {
        public Assignment(string id) : base(id) 
        {
            Id = id;
            BindingPower = 10;
            Assignment = true;
            Arity = "binary";
        }
    }
}
