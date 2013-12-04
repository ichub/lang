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

        public Script Script { get; protected set; }
        public Node Parent { get; protected set; }
        public List<Node> Children { get; protected set; }

        public bool Evaluated { get; protected set; }

        protected Variable value;
        protected string expression;

        protected Node(Script script, Node parent, string literal)
        {
            this.Children = new List<Node>();

            this.Parent = parent;
            this.Script = script;

            this.expression = literal;
        }

        protected virtual Node Evaluate()
        {
            return this;
        }

        public static Node Parse(Script script, Node parent, string expression)
        {
            if (LangSpec.IsLiteral(script, parent, expression))
            {
                return new LiteralNode(script, parent, expression);
            }
            else if (LangSpec.IsFunctionInvocation(expression))
            {
                return new ComplexNode(script, parent, expression);
            }
            else if (LangSpec.IsVariable(expression))
            {
                return new VariableNode(script, parent, expression);
            }
            else
            {
                throw new Exception("Node could not be created due to invalid input");
            }
        }
    }
}
