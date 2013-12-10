using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public bool Valid { get { return this.Type != TokenType.Unknown; } }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
