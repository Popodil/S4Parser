using Parser.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parser
{
    public class Token : Symbol
    {
        public object Value { get; set; }
        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }
        public TokenType TokenType { get; set; }

        public Token(object value, int lineNumber, int columnNumber, TokenType tokenType, string id) : base(id)
        {
            Value = value;
            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            TokenType = tokenType;
        }

        public Token(TokenType type, string value, int line_nr, int column_nr, string id = "") : base(id)
        {
            Value = SetTokenType(type,value);
            Id = Value.ToString();
            this.LineNumber = line_nr;
            this.ColumnNumber = column_nr;
        }

        public Token(Symbol symbol, string? id) : base(id)
        {
            SetSymbol(symbol);
        }

        public Token(string? id) : base(id){ }

        private object SetTokenType(TokenType type, string value)
        {
            TokenType = type;
            switch (type)
            {
                case TokenType.String:
                    return value;
                case TokenType.Number:
                    return Convert.ToDouble(value);
                case TokenType.Comment:
                    return value;
                case TokenType.Operator:
                    return value;
                case TokenType.Name:
                    return value;
                case TokenType.WhiteSpace:
                    return value;
                default:
                    return "";
            }
        }

        public void SetSymbol(Symbol symbol)
        {
            First = symbol.First;
            Second = symbol.Second;
            Arity = symbol.Arity;
            Assignment = symbol.Assignment;
            LeftBindingPower = symbol.LeftBindingPower;
            //Id = symbol.Id;
            //SymbolType = symbol.SymbolType;
            //Value = symbol.Id;
        }
        public override string ToString()
        {
            return $"Value: {Value}\r\n";
        }
    }
    public enum TokenType
    {
        String,
        Number,
        Comment,
        Operator,
        Name,
        WhiteSpace
    }
}
