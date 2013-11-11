using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    /// <summary>
    /// A node representing a value referenced by a variable
    /// </summary>
    class VariableNode : Node
    {
        private string variableName;

        public VariableNode(Expression tree, Node parent, string expression)
            : base(tree, parent, expression)
        {
            this.variableName = expression;
        }

        protected override Node Evaluate()
        {
            base.Evaluate();

            this.value = this.Expression.Variables.GetVariable(this.variableName);
            this.Evaluated = true;

            return this;
        }
    }
}
