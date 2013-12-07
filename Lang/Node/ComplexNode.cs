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
        public ComplexNode(Script script, Node parent, string literal)
            : base(script, parent, literal)
        {
            string[] expressions = LangSpec.DivideIntoParts(literal);

            for (int i = 0; i < expressions.Length; i++)
            {
                this.Children.Add(Node.Parse(this.Script, this, expressions[i]));
            }
        }

        protected override Node Evaluate()
        {
            List<Node> parameters = this.Children.GetRange(1, this.Children.Count - 1);
            VarFunction function = (VarFunction)this.Children[0].Value;

            this.value = function.Invoke(parameters);

            this.Evaluated = true;

            return this;
        }
    }
}
