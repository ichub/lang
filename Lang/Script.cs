using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Script
    {
        public ScopedVariableStore Variables { get; private set; }

        private List<Expression> expressions;

        private string script;

        public Script(string script)
        {
            this.Variables = new ScopedVariableStore();
            this.script = LangSpec.StripWhitespace(script);

            string[] expressionLiterals = LangSpec.GetExpressions(this.script);

            this.expressions = expressionLiterals.Select(lit => new Expression(this, lit)).ToList();
        }

        public Variable Evaluate()
        {
            for (int i = 0; i < this.expressions.Count; i++)
            {
                Variable result = expressions[i].Evaluate();

                if (i == expressions.Count - 1)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
