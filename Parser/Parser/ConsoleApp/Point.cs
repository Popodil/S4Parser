using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Point
    {
        public object? first;
        public string value;
        public object? second;

        public override string ToString()
        {
            //if (first != null) { return $"Value: {value}"; }
            return $"Value: {value}";
        }
    }
}
