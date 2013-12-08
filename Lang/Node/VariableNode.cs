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

        public VariableNode(string literal, Node parent = null)
            : base(literal, parent)
        {
            this.variableName = literal;
        }

        protected override Node Evaluate(Script script)
        {
            this.value = script.Variables.GetVariable(this.variableName);
            this.Evaluated = true;

            return this;
        }
    }
}
