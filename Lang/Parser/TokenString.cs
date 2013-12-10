using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class TokenString
    {
        private List<Token> tokens;

        public TokenString(List<Token> tokens = null)
        {
            this.tokens = tokens ?? new List<Token>();
        }

        public static TokenString operator+(TokenString tokenString, Token token)
        {
            tokenString.tokens.Add(token);

            return tokenString;
        }

        public override string ToString()
        {
            string acc = "";

            foreach (Token token in this.tokens)
            {
                acc += token.Value + " ";
            }

            return acc;
        }

        public void Print()
        {
            foreach (Token token in this.tokens)
            {
                Console.WriteLine(token.Type + " : " + token.Value);
            }
        }
    }
}
