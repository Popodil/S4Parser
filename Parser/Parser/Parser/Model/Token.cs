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

        public Token(string type, object value, int line_nr, int column_nr)
        {
            this.type = type;
            this.value = value;
            this.line_nr = line_nr;
            this.column_nr = column_nr;
        }
    }
}
