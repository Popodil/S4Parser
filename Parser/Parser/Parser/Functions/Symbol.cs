using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Symbol : Base
    {
        public int BindingPower { get; set; }
        public Symbol(string id, int bindingPower = 0) 
        {
            Id = id;
            BindingPower = bindingPower;

            if (!Parse.symbolTable.ContainsKey(Id))
                Parse.symbolTable.Add(Id, this);
            Console.WriteLine($"Key: {Parse.symbolTable[Id].Id} | Value: {Parse.symbolTable[Id].Value}");
        }
    }
}
