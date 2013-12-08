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

        public TokenString(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public static TokenString operator+(TokenString tokenString, Token token)
        {
            tokenString.tokens.Add(token);

            return tokenString;
        }
    }
}
