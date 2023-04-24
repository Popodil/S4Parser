using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Functions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Parser
{
    // Based on: https://crockford.com/javascript/tdop/tdop.html
    public class Parse
    {
        public static Parse instance;

        public Parse() 
        { 
            instance = this; 
        }
        public static Dictionary<string, Token> symbolTable = new();
        /// <summary>
        /// Turns a list of tokens into a parsed JSON string using JValue.Parse
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Finalised JSON string</returns>
        public static string MakeJSON(List<Token> tokens)
        {
            return JValue.Parse(JsonConvert.SerializeObject(tokens)).ToString(Formatting.Indented);
        }

        /// <summary>
        /// Turns a list of tokens into a parsed JSON string using custom parser
        /// </summary>
        /// <param name="tokens"></param>
        /// <exception cref="Exception">Finalised JSON string</exception>
        public static void MakeParse(List<Token> tokens)
        {
            object scope;
            Token token;
            int tokenNumber;

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
                    // TODO return;
                }

                t = tokens[tokenNumber];
                tokenNumber++;
                v = t.Value;
                a = t.Type;

                if (a == "name")
                {
                    //TODO o = scope.Find(v)
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
                //TODO Return token
            }

            object Expression(int RightBindingPower)
            {
                object left;
                Token t = token;
                Advance();
                left = t.Nud;
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
                    //TODO scope.reserve(n);
                    return n.StatementDenotation;
                }
                v = (Token)Expression(0); //TODO cast klopt mogelijk niet
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
                if (symbolTable.ContainsKey(id.ToString()))
                {
                    s = symbolTable[id.ToString()];
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
                //TODO scope.reserve(this);
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
                //TODO scope.reserve(this)
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

            void Initialise()
            {
                new Functions.Symbol("(end)");
                new Functions.Symbol("(name)");
                new Functions.Symbol(":");
                new Functions.Symbol(";");
                new Functions.Symbol(")");
                new Functions.Symbol("]");
                new Functions.Symbol("}");
                new Functions.Symbol(",");
                new Functions.Symbol("else");

                new Constant("true", true);
                new Constant("false", false);
                new Constant("null", null);
                new Constant("pi", 3.141592653589793);
                new Constant("Object", new object());
                new Constant("Array", new List<object>());

                new Functions.Symbol("(literal)").NullDenotation(instance);

                new Functions.Symbol("this") { 
                    //TODO scope.reserve(this);
                    Arity = "this"
                };

                //new Assignment("=") { Second = Expression(9) };
                //new Assignment("+=") { Second = Expression(9) };
                //new Assignment("-=") { Second = Expression(9) };



                new InfixR("&&", 30);
            }
            Initialise(); 
        }
    }
}