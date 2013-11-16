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
        public Script Script { get; private set; }

        public string FunctionLiteral { get; private set; }
        public string[] VariableNames { get; private set; }
        
        private VarUserFunction(Func<Node[], Variable> function)
            : base(function)
        {
            this.LocalVariables = VariableStore.Empty;
        }

        public static VarUserFunction Create(Script script, string functionLiteral)
        {
            var parts = LangSpec.GetFunctionLiteralParts(script, functionLiteral);

            Func<Node[], Variable> function = vars =>
                {
                    return parts.Item1.Evaluate();
                };

            VarUserFunction result = new VarUserFunction(function);

            for (int i = 0; i < parts.Item2.Length; i++)
            {
                result.LocalVariables.CreateVariable(parts.Item2[i]);
            }

            result.VariableNames = parts.Item2;
            result.FunctionLiteral = functionLiteral;
            result.Script = script;

            return result;
        }

        public override Variable Invoke(List<Node> parameters)
        {
            this.Script.Variables.PushScope(this.LocalVariables);

            for (int i = 0; i < this.VariableNames.Length; i++)
            {
                this.Script.Variables.SetVariable(this.VariableNames[i], parameters[i].Value);
            }

            Variable value = this.Value.Invoke(null);

            this.Script.Variables.PopScope();

            return value;
        }
    }
}
