using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Functions
{
    public class Constant : Base
    {
        public Constant(string id, object value) 
        {
            Id = id;
            Value = value;

            Parse.symbolTable.Add(Id, this);
            Console.WriteLine($"Key: {Parse.symbolTable[Id].Id}, Value: {Parse.symbolTable[Id].Value}");
        }
    }
}
