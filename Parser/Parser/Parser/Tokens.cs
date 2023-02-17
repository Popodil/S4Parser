using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    public class Tokens
    {
        private List<object> tokens = new();
        private string rx_crlf = "/\n |\r\n ?/";
        private string rx_token = "/(\\u0020+)|(\\/\\/.*)|([a-zA-Z][a-zA-Z_0-9]*)|(\\d+(?:\\.\\d+)?(?:[eE][+\\-]?\\d+)?)|(\"(?:[^\"\\\\]|\\\\(?:[nr\"\\\\]|u[0-9a-fA-F]{4}))*\")|([(){}\\[\\]?.,:;~*\\/]|&&?|\\|\\|?|[+\\-<>]=?|[!=](?:==)?)/y";
        public List<object> ConvertTokens(string InputString)
        {
            List<string> lines = Regex.Split(InputString, rx_crlf).ToList();
            foreach (string line in lines)
            {
                
            }

            return tokens;
        }
    }
}