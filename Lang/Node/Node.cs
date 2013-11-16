using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Node
    {
        public Variable Value
        {
            get
            {
                this.Evaluate();

                return this.value;
            }
        }

        public Expression Expression { get; protected set; }
        public Node Parent { get; protected set; }
        public List<Node> Children { get; protected set; }

        public bool Evaluated { get; protected set; }

        protected Variable value;
        protected string expression;

        protected Node(Expression expression, Node parent, string literal)
        {
            this.Children = new List<Node>();

            this.Expression = expression;
            this.Parent = parent;

            this.expression = literal;
        }

        protected virtual Node Evaluate()
        {
            return this;
        }

        public static Node Create(Expression tree, Node parent, string expression)
        {
            if (LangSpec.IsLiteral(tree.Script, expression))
            {
                return new LiteralNode(tree, parent, expression);
            }
            else if (LangSpec.IsFunctionInvocation(expression))
            {
                return new ComplexNode(tree, parent, expression);
            }
            else if (LangSpec.IsVariable(expression))
            {
                return new VariableNode(tree, parent, expression);
            }
            else
            {
                throw new Exception("Node could not be created due to invalid input");
            }
        }
    }
}
