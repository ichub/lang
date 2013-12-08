using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    static class Parser
    {
        public static TokenString ParseString(string input)
        {
            return new TokenString(new List<Token>());
        }
    }
}
