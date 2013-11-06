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
        public LiteralNode(SyntaxTree tree, Node parent, string expression)
            : base(tree, parent, expression)
        {

        }

        public override void Evaluate()
        {
            base.Evaluate();

            this.value = LangSpec.GetLiteral(this.expression);
            this.Evaluated = true;
        }
    }
}
