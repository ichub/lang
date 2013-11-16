using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Expression
    {
        public Script Script { get; private set; }

        private Node topNode;

        public Expression(Script script, string expression)
        {
            this.Script = script;

            this.topNode = Node.Create(this, null, expression);
        }

        public Variable Evaluate()
        {
            return this.topNode.Value;
        }
    }
}
