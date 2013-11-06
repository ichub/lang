using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    /// <summary>
    /// A node representing a function invocation
    /// </summary>
    class ComplexNode : Node
    {
        public ComplexNode(SyntaxTree tree, Node parent, string expression)
            : base(tree, parent, expression)
        {
            string[] expressions = LangSpec.DivideExpressions(expression);

            for (int i = 0; i < expressions.Length; i++)
            {
                this.Children.Add(Node.Create(this.Tree, this, expressions[i]));
            }
        }

        public override void Evaluate()
        {
            base.Evaluate();

            VarFunction function = (VarFunction)this.Children[0].Value;

            this.value = function.Invoke(this.Children.GetRange(1, this.Children.Count - 1));
            this.Evaluated = true;
        }
    }
}
