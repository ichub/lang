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
        public VarFunction Function { get; private set; }

        public ComplexNode(string literal, Node parent = null)
            : base(literal, parent)
        {
            string[] expressions = LangSpec.DivideIntoParts(literal);

            for (int i = 1; i < expressions.Length; i++)
            {
                this.Children.Add(Node.Parse(expressions[i]));
            }

            this.Function = (VarFunction)Node.Parse(expressions[0]).Value;

        }

        protected override Node Evaluate(Script script)
        {
            this.value = this.Function.Invoke(this.Children, script);

            this.Evaluated = true;

            return this;
        }
    }
}
