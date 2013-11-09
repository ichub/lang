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
                if (this.Evaluated)
                {
                    return this.value;
                }

                throw new Exception("Node has to be evaluated before its value can be accessed");
            }
        }

        public Expression Tree { get; protected set; }
        public Node Parent { get; protected set; }
        public List<Node> Children { get; protected set; }

        public bool Evaluated { get; protected set; }

        protected Variable value;
        protected string expression;

        protected Node(Expression tree, Node parent, string expression)
        {
            this.Children = new List<Node>();

            this.Tree = tree;
            this.Parent = parent;

            this.expression = expression;
        }

        public virtual Node Evaluate()
        {
            if (!this.Evaluated)
            {
                foreach (Node child in this.Children)
                {
                    child.Evaluate();
                }
            }

            return this;
        }

        public static Node Create(Expression tree, Node parent, string expression)
        {
            if (LangSpec.IsLiteral(tree.ParentScript, expression))
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
