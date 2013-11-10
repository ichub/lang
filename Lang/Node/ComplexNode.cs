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
        public ComplexNode(Expression tree, Node parent, string expression)
            : base(tree, parent, expression)
        {
            string[] expressions = LangSpec.DivideExpressions(expression);

            for (int i = 0; i < expressions.Length; i++)
            {
                this.Children.Add(Node.Create(this.Tree, this, expressions[i]));
            }
        }

        public override Node Evaluate()
        {
            base.Evaluate();

            List<Node> children = this.Children.GetRange(1, this.Children.Count - 1);
            VarFunction function = (VarFunction)this.Children[0].Value;

            if (function is VarUserFunction)
            {
                VarUserFunction userFunction = (VarUserFunction)function;
                this.Tree.Variables.PushUserFunction(userFunction.LocalVariables);

                for (int i = 0; i < userFunction.VariableNames.Length; i++)
                {
                    this.Tree.Variables[userFunction.VariableNames[i]] = children[i].Evaluate().Value;
                }

                userFunction.Invoke();

                this.Tree.Variables.PopUserFunction();

            }
            else
            {
                this.value = function.Invoke(children);
            }

            this.Evaluated = true;

            return this;
        }
    }
}
