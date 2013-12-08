using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Node
    {
        public Script Script
        {
            get
            {
                return this.script;
            }
            set
            {
                this.script = value;

                foreach (Node child in this.Children)
                {
                    child.Script = value;
                }
            }
        }
        
        public Variable Value
        {
            get
            {
                this.Evaluate(this.Script);

                return this.value;
            }
        }

        public bool Orphan { get { return this.Script == null; } }
        public Node Parent { get; protected set; }
        public List<Node> Children { get; protected set; }

        public bool Evaluated { get; protected set; }

        protected Script script;
        protected Variable value;
        protected string expression;

        protected Node(string literal, Node parent = null)
        {
            this.Children = new List<Node>();

            this.Parent = parent;

            this.expression = literal;
        }

        protected virtual Node Evaluate(Script script)
        {
            return this;
        }

        public static Node Parse(string expression)
        {
            if (LangSpec.IsLiteral(expression))
            {
                return new LiteralNode(expression);
            }
            else if (LangSpec.IsFunctionInvocation(expression))
            {
                return new ComplexNode(expression);
            }
            else if (LangSpec.IsVariable(expression))
            {
                return new VariableNode(expression);
            }
            else
            {
                throw new Exception("Node could not be created due to invalid input");
            }
        }
    }
}
