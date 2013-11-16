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

        public VariableNode(Expression expression, Node parent, string literal)
            : base(expression, parent, literal)
        {
            this.variableName = literal;
        }

        protected override Node Evaluate()
        {
            base.Evaluate();

            this.value = this.Expression.Script.Variables.GetVariable(this.variableName);
            this.Evaluated = true;

            return this;
        }
    }
}
