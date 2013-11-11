using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Expression
    {
        public Script ParentScript { get; private set; }

        private Node topNode;

        public Expression(Script parentScript, string expression)
        {
            this.ParentScript = parentScript;

            this.topNode = Node.Create(this, null, expression);
        }

        public Variable Evaluate()
        {
            return this.topNode.Value;
        }
    }
}
