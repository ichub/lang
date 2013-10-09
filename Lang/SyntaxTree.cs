using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class SyntaxTree
    {
        public VariableStore Variables { get; private set; }

        private Node topNode;

        public SyntaxTree(string script)
        {
            this.Variables = VariableStore.Default;

            this.topNode = new Node(script, this);
        }

        public Variable Evaluate()
        {
            this.topNode.Evaluate();

            return this.topNode.Value;
        }
    }
}
