using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    /// <summary>
    /// A node representing a literal expression
    /// </summary>
    class LiteralNode : Node
    {
        public LiteralNode(string literal, Node parent = null)
            : base(literal, parent) { }

        protected override Node Evaluate(Script script)
        {
            this.value = LangSpec.GetLiteral(this.expression);
            this.Evaluated = true;

            return this;
        }
    }
}
