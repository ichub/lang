﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarUserFunction : VarFunction
    {
        public VariableStore LocalVariables { get; private set; }

        public string FunctionLiteral { get; private set; }
        public string[] VariableNames { get; private set; }

        private Script script;

        private VarUserFunction(int arity = 0, Func<Node[], Variable> function = null)
            : base(arity, function ?? (a => a[0].Value))
        {
            this.LocalVariables = VariableStore.Empty;

            this.LocalVariables.CreateVariable("this");
            this.LocalVariables.SetVariable("this", this);
        }

        public static VarUserFunction Parse(string functionLiteral)
        {
            var parts = LangSpec.GetFunctionLiteralParts(functionLiteral);

            VarUserFunction result = new VarUserFunction();

            Func<Node[], Variable> function = vars =>
                {
                    parts.Item1.Script = result.script;
                    return parts.Item1.Value;
                };

            result.Value = function;

            for (int i = 0; i < parts.Item2.Length; i++)
            {
                result.LocalVariables.CreateVariable(parts.Item2[i]);
            }

            result.VariableNames = parts.Item2;
            result.FunctionLiteral = functionLiteral;
            result.Arity = parts.Item2.Length;

            return result;
        }

        public override Variable Invoke(List<Node> parameters, Script script = null)
        {
            if (parameters.Count != this.Arity)
            {
                throw new Exception("Amount of parameters does not match function arity");
            }

            this.script = script;
            script.Variables.PushScope(this.LocalVariables);

            for (int i = 0; i < this.VariableNames.Length; i++)
            {
                script.Variables.SetVariable(this.VariableNames[i], parameters[i].Value);
            }

            Variable result = this.Value.Invoke(null);

            script.Variables.PopScope();

            return result;
        }
    }
}
