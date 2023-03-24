using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Token : Symbol
    {
        public string Type { get; set; }
        public object Value { get; set; }
        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }

        // Optional properties
        public object StatementDenotation { get; set; }

        /*public Token(string type, string value, int line_nr, int column_nr)
        {
            this.Type = type;
            this.Value = GetValue(value);
            this.LineNumber = line_nr;
            this.ColumnNumber = column_nr;
        }
        public Token() { }*/

        private object GetValue(string value)
        {
            switch (Type)
            {
                case "Comment":
                    return value;
                case "Name":
                    return value;
                case "Number":
                    return Convert.ToInt32(value);
                case "String":
                    if (value == "true") return true;
                    else if (value == "false") return false;
                    return value;
                case "Punctuator":
                    return value;
                default:
                    return value;
            }
        }
    }
}
