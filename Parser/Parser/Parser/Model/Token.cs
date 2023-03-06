using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Token
    {
        public string type { get; private set; }
        public object value { get; private set; }
        public int line_nr { get; private set; }
        public int column_nr { get; private set; }

        public Token(string type, string value, int line_nr, int column_nr)
        {
            this.type = type;
            this.value = GetValue(value);
            this.line_nr = line_nr;
            this.column_nr = column_nr;
        }

        private object GetValue(string value)
        {
            switch (type)
            {
                case "Comment":
                    return value;
                case "Name":
                    return value;
                case "Number":
                    return Convert.ToInt32(value);
                case "String":
                    return value;
                case "Punctuator":
                    return value;
                default:
                    return value;
            }
        }
    }
}
