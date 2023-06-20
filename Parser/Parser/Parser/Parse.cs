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
        public Token token;
        public Tree tree = new();
        public Dictionary<string, Symbol> symbolTable;
        public List<Token> tokens;
        public int tokenIndex = 0;
        public Base temp;
        public Prefix prefix;
        public InfixR infixr;
        public Infix infix;
        public Statement statement;
        private bool end = false;

        public Parse()
        {
            prefix = new(this);
            infixr = new(this);
            infix = new(this);
            statement = new(this);
            symbolTable = new();
            Initialise();
        }

        private void Initialise()
        {
            JObject OperatorPrecedence = JObject.Parse(File.ReadAllText(@"D:\School\S4\Low Code\Parser\Initialise.json"));
            if (OperatorPrecedence == null)
            {
                throw (new Exception("json empty or unable to read"));
            }
            foreach (string Key in OperatorPrecedence.Properties().Select(p => p.Name).ToList())
            {
                SymbolType type = CheckSymbolType(Key);
                foreach (JProperty symbol in OperatorPrecedence[Key])
                {
                    Symbol s = new(symbol.Name, type, Convert.ToInt32(symbol.Value));
                    if (!symbolTable.ContainsKey(symbol.Name))
                        symbolTable.Add(symbol.Name, s);
                }
            }

            SymbolType CheckSymbolType(string type)
            {
                SymbolType symbolType = type switch
                {
                    "infix" => SymbolType.infix,
                    "infixr" => SymbolType.infixR,
                    "prefix" => SymbolType.prefix,
                    "assignement" => SymbolType.assignment,
                    "statement" => SymbolType.statement,
                    _ => SymbolType.symbol,
                };
                return symbolType;
            }
            Symbol sym = new("(", SymbolType.infix, 80, SymbolType.prefix);
            if (!symbolTable.ContainsKey(sym.Id))
                symbolTable.Add("(", sym);
            sym = new("[", SymbolType.infix, 0, SymbolType.prefix);
            if (!symbolTable.ContainsKey(sym.Id))
                symbolTable.Add("[", sym);
            sym = new("{", SymbolType.prefix, 0, SymbolType.statement);
            if (!symbolTable.ContainsKey(sym.Id))
                symbolTable.Add("{", sym);
        }

        /// <summary>
        /// Turns a list of tokens into a parsed JSON string using JValue.Parse
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Finalised JSON string</returns>
        public string MakeJSON(List<Token> tokens)
        {
            return JValue.Parse(JsonConvert.SerializeObject(tokens)).ToString(Formatting.Indented);
        }

        /// <summary>
        /// Turns a list of tokens into a parsed JSON string using custom parser
        /// </summary>
        /// <param name="tokens"></param>
        /// <exception cref="Exception">Finalised JSON string</exception>
        public Parse MakeParse(List<Token> tokens)
        {
            this.tokens = tokens;
            Advance();
            Statements();
            return this;
        }

        public void Advance(string? value = null)
        {
            if (tokenIndex == tokens.Count)
            {
                end = true;
                return;
            }

            //TODO fix "(" with expected ";"
            if (value != null)
            {
                if (value != token.Id)
                {
                    throw new Exception($"Expected: {value}");
                }
            }

            token = tokens[tokenIndex];
            tokenIndex++;

            if (token.TokenType == TokenType.Name)
            {
                Symbol? name = symbolTable["(name)"];
                if (name == null)
                {
                    throw new Exception("(name) symbol not found");
                }
                token.SetSymbol(name);
            }
            else if (token.TokenType == TokenType.Operator)
            {
                Symbol? Operator = symbolTable[token.Id];
                if (Operator == null)
                {
                    throw new Exception("Unkown operator");
                }
                token.SetSymbol(Operator);
            }
            else if (token.TokenType == TokenType.String || token.TokenType == TokenType.Number)
            {
                Symbol? literal = symbolTable["(literal)"];
                if (literal == null)
                {
                    throw new Exception("(literal) symbol not found");
                }
                token.SetSymbol(literal);
            }
            else if (token.Id == "(end)")
            {
                return;
            }
            else
            {
                throw new Exception("Unknown token");
            }


        }

        public Base Expression(int rightBindingPower)
        {
            Token t = token;
            Advance();
            Base previousBase = new(t);
            if (t.SymbolType == SymbolType.prefix || t.SecondType == SymbolType.prefix)
            {
                previousBase = Nud(new(token), previousBase);
            }
            while (rightBindingPower < token.LeftBindingPower)
            {
                t = token;
                Advance();
                previousBase = Led(previousBase, new(t));
            }
            return previousBase;
        }

        private void Sentence()
        {
            if (token.Id == "let")
            {
                Token T = token;
                Advance();
                temp = statement.StatementFunc(new(token), new(T));
                tree.AddBase(temp);
                return;
            }
            Expression(0);
            tree.AddBase(temp);
            Advance(";");
        }

        private Base Led(Base First, Base main)
        {
            switch (main.token.SymbolType)
            {
                case SymbolType.infix:
                    temp = infix.Led(First, main);
                    break;
                case SymbolType.infixR:
                    temp = infixr.Led(First, main);
                    break;
                default:
                    throw new Exception("Incorrect symbol");
            }
            return temp;
        }
        private Base Nud(Base First, Base Base)
        {
            temp = prefix.Nud(First, Base);
            return temp;
        }
        private void Statements()
        {
            while (!end)
            {
                Sentence();
            }
        }
    }
}