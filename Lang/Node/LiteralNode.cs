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
        public LiteralNode(Script script, Node parent, string literal)
            : base(script, parent, literal) { }

        protected override Node Evaluate()
        {
            this.value = LangSpec.GetLiteral(this.Script, this.expression);
            this.Evaluated = true;

            return this;
        }
    }
}
