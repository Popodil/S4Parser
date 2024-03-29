﻿using System.Text.RegularExpressions;

namespace Parser
{
    // Based on: https://crockford.com/javascript/tdop/tdop.html
    public class Tokenizer
    {
        private List<Token> tokens;
        private readonly string rx_crlf = "\n|\r\n|\r";
        private readonly string rx_token = "(\\\\u0020+)|(\\/\\/[^\\n]*)|([a-zA-Z][a-zA-Z_0-9]*)|(\\d+(?:\\.\\d+)?(?:[eE][+\\-]?\\d+)?)|(\"(?:[^\"\\\\]|\\\\(?:[nr\"\\\\]|u[0-9a-fA-F]{4}))*\")|([(){}\\[\\]?.,:;~*\\/]|&&?|\\|\\|?|[+\\-<>]=?|[!=](?:==)?)";
        public List<Token> ConvertTokens(string InputString)
        {
            tokens = new();
            List<string> lines = Regex.Split(InputString, rx_crlf).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], rx_token);
                foreach (Match match in matches.Cast<Match>())
                {
                    Group group1 = match.Groups[1];
                    if (group1.ToString() != "" && group1.ToString() != null)
                    {
                        //tokens.Add(new Token(TokenType.WhiteSpace, group1.ToString(), i + 1, group1.Index));
                    }

                    Group group2 = match.Groups[2];
                    if (group2.ToString() != "" && group2.ToString() != null)
                    {
                        tokens.Add(new Token(TokenType.Comment, group2.ToString(), i + 1, group2.Index));
                    }

                    Group group3 = match.Groups[3];
                    if (group3.ToString() != "" && group3.ToString() != null)
                    {
                        tokens.Add(new Token(TokenType.Name, group3.ToString(), i + 1, group3.Index));
                    }

                    Group group4 = match.Groups[4];
                    if (group4.ToString() != "" && group4.ToString() != null)
                    {
                        tokens.Add(new Token(TokenType.Number, group4.ToString(), i + 1, group4.Index));
                    }

                    Group group5 = match.Groups[5];
                    if (group5.ToString() != "" && group5.ToString() != null)
                    {
                        tokens.Add(new Token(TokenType.String, group5.ToString(), i + 1, group5.Index));
                    }

                    Group group6 = match.Groups[6];
                    if (group6.ToString() != "" && group6.ToString() != null)
                    {
                        tokens.Add(new Token(TokenType.Operator, group6.ToString(), i + 1, group6.Index));
                    }
                }
            }

            tokens ??= new();
            return tokens;
        }
    }
}