using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarUserFunction : VarFunction
    {
        public VariableStore LocalVariables { get; private set; }
        public string[] VariableNames { get; private set; }
        
        private VarUserFunction(Func<Variable[], Variable> function)
            : base(function)
        {
            this.LocalVariables = VariableStore.Default;
        }

        public static VarUserFunction Create(Script parentScript, string functionLiteral)
        {
            var parts = LangSpec.GetFunctionLiteralParts(parentScript, functionLiteral);

            Func<Variable[], Variable> function = vars =>
                {
                    return parts.Item1.Evaluate();
                };

            VarUserFunction result = new VarUserFunction(function);

            for (int i = 0; i < parts.Item2.Length; i++)
            {
                result.LocalVariables.SetVariable(parts.Item2[i],  Variable.Unset);
            }

            result.VariableNames = parts.Item2;

            return result;
        }

        public Variable Invoke()
        {
            return this.Value.Invoke(null);
        }
    }
}
