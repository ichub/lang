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

        private string script;

        public Script(string script)
        {
            this.Variables = VariableStore.Default;

            this.script = script;
        }

        public Variable Evaluate()
        {
            string[] expressionLiterals = LangSpec.GetExpressions(this.script);

            for (int i = 0; i < expressionLiterals.Length; i++)
            {
                Expression expression = new Expression(this, expressionLiterals[i]);

                Variable result = expression.Evaluate();

                if (i == expressionLiterals.Length - 1)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
