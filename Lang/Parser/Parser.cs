using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lang
{
    static class Parser
    {
        private static List<Tuple<TokenType, Regex>> tokenAssociations;

        public static void Initialize()
        {
            // todo: make list characters be parsed

            tokenAssociations = new List<Tuple<TokenType, Regex>>()
            {
                Tuple.Create(TokenType.ExpressionSeparator, LangSpec.ExpressionSeparatorRegex),
                Tuple.Create(TokenType.VariableSeparator, LangSpec.VariableSeparatorRegex),
                Tuple.Create(TokenType.FunctionVariableSeparator, LangSpec.FunctionVariableSeparatorRegex),
                Tuple.Create(TokenType.ListItemSeparator, LangSpec.ListItemSeparatorRegex),

                Tuple.Create(TokenType.ExpressionOpen, LangSpec.ExpressionOpenRegex),
                Tuple.Create(TokenType.ExpressionClose, LangSpec.ExpressionCloseRegex),

                Tuple.Create(TokenType.FuncOpen, LangSpec.FuncOpenRegex),
                Tuple.Create(TokenType.FuncClose, LangSpec.FuncCloseRegex),

                Tuple.Create(TokenType.ListOpen, LangSpec.ListOpenRegex),
                Tuple.Create(TokenType.ListClose, LangSpec.ListCloseRegex),

                Tuple.Create(TokenType.Variable, LangSpec.Variable),
                Tuple.Create(TokenType.Number, LangSpec.NumberLiteral),
                Tuple.Create(TokenType.Boolean, LangSpec.BooleanLiteral),
                Tuple.Create(TokenType.String, LangSpec.StringLiteral),
            };
        }

        public static TokenString ParseString(string input)
        {
            input = RemoveWhitespace(input);

            int relativeTokenEnd = 0;
            int tokenEnd = 0;

            TokenString tokens = new TokenString();

            while (tokenEnd < input.Length)
            {
                tokens += GetNextToken(input.Substring(tokenEnd), out relativeTokenEnd);

                tokenEnd += relativeTokenEnd;
            }

            return tokens;
        }

        private static Token GetNextToken(string input, out int index)
        {
            Token token = null;
            int i = 1;

            do
            {
                if (input[0].IsPartOfWord() || input[0] == '-')
                {
                    while (input[i].IsPartOfWord()) { i++; } // read to end of words
                }

                token = GetToken(input.Substring(0, i++));
            }
            while (!token.Valid && i < input.Length);

            index = i - 1;
            return token;
        }

        private static Token GetToken(string input)
        {
            foreach (var tuple in tokenAssociations)
            {
                if (tuple.Item2.IsMatch(input))
                {
                    return new Token(tuple.Item1, input);
                }
            }

            return new Token(TokenType.Unknown, input);
        }

        private static bool IsPartOfWord(this char character)
        {
            return (Char.IsLetterOrDigit(character) || character == '"');
        }

        private static string RemoveWhitespace(string input)
        {
            return LangSpec.Whitespace.Replace(input, "");
        }
    }
}
