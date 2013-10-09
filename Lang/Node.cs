using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Node
    {
        public SyntaxTree ParentTree { get; private set; }
        public List<Node> Children { get; private set; }
        public Node Parent { get; private set; }
        public Variable Value { get; private set; }
        public string Expression { get; private set; }

        private bool evaluated;

        public Node(string expression, SyntaxTree parentTree, Node parent = null)
        {
            this.Parent = parent;
            this.ParentTree = parentTree;
            this.Children = new List<Node>();
            this.Expression = expression;

            string[] expressions = LangSpec.DivideExpressions(expression);


            if (expressions.Length == 1)
            {
                Variable literal = LangSpec.GetLiteral(expressions[0]);

                if (literal != null)
                {
                    this.Value = literal;
                    this.evaluated = true;
                    return;
                }

                Variable variable = this.ParentTree.Variables[expressions[0]];

                if (variable != null)
                {
                    this.Value = variable;
                    this.evaluated = true;
                    return;
                }
            }

            for (int i = 0; i < expressions.Length; i++)
            {
                this.Children.Add(new Node(expressions[i], this.ParentTree, this));
            }
        }

        private bool ChildrenEvaluated()
        {
            foreach (Node child in this.Children)
            {
                if (!child.evaluated)
                {
                    return false;
                }
            }

            return true;
        }

        public void Evaluate()
        {
            if (!this.ChildrenEvaluated())
            {
                foreach (Node child in this.Children)
                {
                    child.Evaluate();
                }
            }

            if (!this.evaluated)
            {
                VarFunction function = (VarFunction)this.Children[0].Value;

                this.Value = function.Invoke(this.Children.GetRange(1, this.Children.Count - 1));

                this.evaluated = true;
            }
        }
    }
}
