using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace Parser
{
    // Based on: https://crockford.com/javascript/tdop/tdop.html
    public class Parse
    {
        private List<Token> tokens = new();
        /// <summary>
        /// Turns a list of tokens into a parsed JSON string
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Finalised JSON string</returns>
        public string MakeJSON(List<Token> tokens)
        {
            this.tokens = tokens;
            string str = "";
            for (int i = 0; i < this.tokens.Count; i++)
            {
                switch (tokens[i].type)
                {
                    case "Comment":

                        break;
                    case "Name":

                        break;
                    case "Number":

                        break;
                    case "String":

                        break;
                    case "Punctuator":

                        break;
                    default:
                        break;
                }
            }
            //return JValue.Parse(JsonConvert.SerializeObject(tokens)).ToString(Formatting.Indented);
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of the currently selected token</param>
        /// <returns></returns>
        private List<int> CheckPunc(int index)
        {
            return new List<int>() { 0,1,2};
        }
    }
}