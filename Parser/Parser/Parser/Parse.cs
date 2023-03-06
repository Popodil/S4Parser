using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace Parser
{
    public class Parse
    {
        /// <summary>
        /// Turns a list of tokens into a parsed JSON string
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public string MakeJSON(List<Token> tokens)
        {
            return JValue.Parse(JsonConvert.SerializeObject(tokens)).ToString(Formatting.Indented);
        }
    }
}