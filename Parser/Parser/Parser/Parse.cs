using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parser
{
    // Based on: https://crockford.com/javascript/tdop/tdop.html
    public class Parse
    {
        //private List<Token> tokens = new();
        private Dictionary<string, int> symbols = new();
        /// <summary>
        /// Turns a list of tokens into a parsed JSON string
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Finalised JSON string</returns>
        public string MakeJSON(List<Token> tokens)
        {
            return JValue.Parse(JsonConvert.SerializeObject(tokens)).ToString(Formatting.Indented);
        }

        public void MakeParse(List<Token> tokens)
        {
            object scope;
            Dictionary<string, Token> symbolTable = new Dictionary<string, Token>();
            Token token;
            int tokenNumber;

            object Itself()
            {
                return this;
            }

            void OriginalScope()
            {

            }

            void NewScope(int id)
            {

            }



            void Advance(string id = null)
            {
                string a;
                object o;
                Token t;
                object v;

                if (string.IsNullOrEmpty(id) && !string.Equals(token.Id, id))
                {
                    throw new Exception("Expected '" + id + "'.");
                }
                if (tokenNumber >= tokens.Count)
                {
                    token = symbolTable["(end)"];
                    // return;
                }

                t = tokens[tokenNumber];
                tokenNumber++;
                v = t.Value;
                a = t.Type;

                if (a == "name")
                {
                    // Todo o = scope.Find(v)
                }
                else if (a == "punctuator")
                {
                    o = symbolTable[v.ToString()];
                    if (o == null)
                    {
                        throw new Exception("Unknown operator.");
                    }
                }
                else if (a == "string" || a == "number")
                {
                    o = symbolTable["(literal)"];
                    a = "literal";
                }
                else
                {
                    throw new Exception("Unexpected token.");
                }

                token = new Token
                {
                    LineNumber = t.LineNumber,
                    ColumnNumber = t.ColumnNumber,
                    Value = v,
                    Arity = a
                };
                // Return token
            }

            object Expression(int RightBindingPower)
            {
                object left;
                Token t = token;
                Advance();
                left = t.NullDenotation();
                while (RightBindingPower < token.LeftBindingPower)
                {
                    t = token;
                    Advance();
                    left = t.LeftDenotation(left);
                }
                return left;
            }

            object Statement()
            {
                Token n = token;
                Token v;

                if (n.StatementDenotation != null)
                {
                    Advance();
                    //scope.reserve(n);
                    return n.StatementDenotation;
                }
                v = (Token)Expression(0); //Todo cast klopt mogelijk niet
                if (!v.Assignment && v.Id != "(")
                {
                    throw new Exception("Bad expression statement.");
                }
                Advance(";");
                return v;
            }

            List<object> Statements()
            {
                List<object> a = new List<object>();
                object s;

                while (true)
                {
                    if (token.Id == "}" || token.Id == "(end)")
                    {
                        break;
                    }
                    s = Statement();
                    if (s != null)
                    {
                        a.Add(s);
                    }
                }
                return a;
            }

            object Block()
            {
                Token t = token;
                Advance("{");
                return t.StatementDenotation;
            }

            Token Symbol(object id, int bindingPower = 0)
            {
                Token s = null;
                if (symbolTable.ContainsKey(id.ToString())) s = symbolTable[id.ToString()];

                if (s != null)
                {
                    if (bindingPower >= s.LeftBindingPower)
                    {
                        s.LeftBindingPower = bindingPower;
                    }
                }
                else
                {
                    s = new Token();
                    s.Id = id.ToString();
                    s.Value = id;
                    s.LeftBindingPower = bindingPower;
                    symbolTable.Add(id.ToString(), s);
                }
                return s;
            }

            Token Constant(object s, object v)
            {
                Token x = Symbol(s);
                // TODO scope.reserve(this);
                x.Arity = "literal";
                x.Value = v;
                return x;
            }

            Token Infix(string id, int bindingPower, object led)
            {
                Token s = Symbol(id, bindingPower);
                s.Second = Expression(bindingPower);
                s.Arity = "binary";
                return s;
            }

            Token Infixr(string id, int bindingPower, object led)
            {
                Token s = Symbol(id, bindingPower);
                s.Second = Expression(bindingPower - 1);
                s.Arity = "binary";
                return s;
            }

            Token Assignment(string id)
            {
                Token s = Symbol(id, 10);
                s.Second = Expression(9);
                s.Assignment = true;
                s.Arity = "binary";
                return s;
            }

            object Prefix(string id, object nud)
            {
                Token s = Symbol(id);
                //Todo scope.reserve(this)
                s.Arity = "unary";
                s.First = Expression(70);

                return s;
            }

            Token Stmt(object s, object f)
            {
                Token x = Symbol(s);
                x.StatementDenotation = f;
                return x;
            }

            Symbol("(end)");
            Symbol("(name)");
            Symbol(":");
            Symbol(";");
            Symbol(")");
            Symbol("]");
            Symbol("}");
            Symbol(",");
            Symbol("else");

            Constant("true", true);
            Constant("false", false);
            Constant("null", null);
            Constant("pi", 3.141592653589793);
            //Constant("Object", new object obj);
            //Constant("Array", new List<object> list);

            //Symbol("(literal)").NullDenotation = Itself();

            /*Symbol("this").NullDenotation = function() {
                scope.reserve(this);
                this.arity = "this";
                return this;
            };*/

            /*Assignment("=");
            Assignment("+=");
            Assignment("-=");*/
        }
    }
}