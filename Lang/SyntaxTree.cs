using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class Node
    {
        public List<Node> Children { get; private set; }
        public Variable Value { get; private set; }
        public string Expression { get; private set; }

        private bool evaluated;

        public Node(string expression)
        {
            this.Children = new List<Node>();
            this.Expression = expression;
        }

        private void PopulateChildren()
        {

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

        private void CheckOperationValidity()
        {
            if (!this.ChildrenEvaluated())
            {
                throw new InvalidOperationException("Can't evaluate a node if all children aren't evaluated");
            }

            VarFunction function = this.Children[0].Value as VarFunction;

            if (function == null)
            {
                throw new Exception("First argument has to be a function");
            }

            if (this.Children.Count - 1 != function.AmountOfArgs)
            {
                throw new Exception("Amount of arguments doesn't match function");
            }

            for (int i = 1; i < this.Children.Count; i++)
            {
                if (this.Children[i].Value.Type != function.VariableTypes[i])
                {
                    throw new Exception(String.Format(
                        "Parameter number {0} should have been a {1} but it was a {2} in expression {3}", 
                        i, 
                        function.VariableTypes[i], 
                        this.Children[i].Value.Type,
                        this.Expression));
                }
            }
           
        }

        public Variable Evaluate()
        {
            this.CheckOperationValidity();

            VarFunction function = (VarFunction)this.Children[0].Value;

            return function.Apply(this.Children.GetRange(1, this.Children.Count - 1));
        }
    }
}
