using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Script
    {
        public VariableStore Variables { get; private set; }

        private List<Expression> expressions;

        public Script(string script)
        {
            this.Variables = VariableStore.Default;

            this.expressions = LangSpec.GetExpressions(this, script);
        }

        public Variable Evaluate()
        {
            for (int i = 0; i < this.expressions.Count; i++)
            {
                var result = this.expressions[i].Evaluate();

                if (i == this.expressions.Count - 1)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
