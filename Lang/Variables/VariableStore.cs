using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VariableStore
    {
        public static VariableStore Default { get { return new VariableStore(); } }

        private Dictionary<string, Variable> variables;

        public Variable this[string name]
        {
            get
            {
                if (this.variables.ContainsKey(name))
                {
                    return this.variables[name];
                }

                return null;
            }
            set 
            {
                if (!this.variables.ContainsKey(name))
                {
                    this.variables.Add(name, value);
                }

                this.variables[name] = value;
            }
        }

        private VariableStore()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.variables = new Dictionary<string, Variable>
            {
                { 
                    "add", 
                    new VarFunction
                    (
                        vars => 
                            {
                                VarNumber first = (VarNumber)vars[0];
                                VarNumber second = (VarNumber)vars[1];

                                return first + second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "sub",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0];
                                VarNumber second = (VarNumber)vars[1];

                                return first - second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "mul",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0];
                                VarNumber second = (VarNumber)vars[1];

                                return first * second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "div",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0];
                                VarNumber second = (VarNumber)vars[1];

                                return first / second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "and",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean first = (VarBoolean)vars[0];
                                VarBoolean second = (VarBoolean)vars[1];

                                return first & second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "or",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean first = (VarBoolean)vars[0];
                                VarBoolean second = (VarBoolean)vars[1];

                                return first | second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "xor",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean first = (VarBoolean)vars[0];
                                VarBoolean second = (VarBoolean)vars[1];

                                return first ^ second;
                            },
                        new[] {VariableType.Number, VariableType.Number}
                    )
                },
                {
                    "ifthen",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean decider = (VarBoolean)vars[0];

                                return decider.Value ? vars[1] : vars[2];
                                
                            },
                        new[] {VariableType.Boolean, VariableType.Any, VariableType.Any}
                    )
                },
                {
                    "PI",
                    new VarNumber(Math.PI)
                }
            };
        }
    }
}
