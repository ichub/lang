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
            Parser.Initialize();
            
            var a = Parser.ParseString(script);
            a.Print();

            this.Variables = new ScopedVariableStore();
            this.script = LangSpec.StripWhitespace(script);

            string[] expressionLiterals = LangSpec.GetExpressions(this.script);

            this.expressions = expressionLiterals.Select(lit =>
                {
                    var exp = new Expression(lit);
                    exp.Script = this;
                    return exp;
                })
                .ToList();
        }

        public Variable Evaluate()
        {
            for (int i = 0; i < this.expressions.Count; i++)
            {
                Variable result = expressions[i].Value;

                if (i == expressions.Count - 1)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
