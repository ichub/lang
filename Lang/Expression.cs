using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Expression
    {
        public Variable Value
        {
            get
            {
                return this.topNode.Value;
            }
        }

        public Script Script
        {
            get
            {
                return this.script;
            }
            set
            {
                this.script = value;
                this.topNode.Script = value;
            }
        }

        public bool Orphan { get { return this.Script == null; } }

        private Node topNode;
        private Script script;

        public Expression(string expression)
        {
            this.topNode = Node.Parse(expression);
            this.topNode.Script = this.Script;
        }
    }
}
