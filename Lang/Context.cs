using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Script
    {
        private Node syntaxTree;

        public Script(string script)
        {
            this.syntaxTree = new Node(script, VariableStore.Default);
        }

        public Variable Execute()
        {
            this.syntaxTree.Evaluate();

            return this.syntaxTree.Value;
        }
    }
}
