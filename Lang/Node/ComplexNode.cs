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
                this.Children.Add(Node.Create(this.Expression, this, expressions[i]));
            }
        }

        protected override Node Evaluate()
        {
            base.Evaluate();

            List<Node> children = this.Children.GetRange(1, this.Children.Count - 1);
            VarFunction function = (VarFunction)this.Children[0].Value;

            if (function is VarUserFunction)
            {
                VarUserFunction userFunction = (VarUserFunction)function;

                this.Expression.ParentScript.Variables.PushScope(userFunction.LocalVariables);

                System.Diagnostics.Debug.WriteLine(this.Expression.ParentScript.Variables.Depth);

                for (int i = 0; i < userFunction.VariableNames.Length; i++)
                {
                    this.Expression.ParentScript.Variables.SetVariable(userFunction.VariableNames[i], children[i].Value);
                }

                this.value = userFunction.Invoke();

                this.Expression.ParentScript.Variables.PopScope();

                System.Diagnostics.Debug.WriteLine(this.Expression.ParentScript.Variables.Depth);
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
