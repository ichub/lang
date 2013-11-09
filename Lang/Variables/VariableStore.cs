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
        private Stack<VariableStore> localVariables;

        public Variable this[string name]
        {
            get
            {
                if (this.localVariables.Count > 0)
                {
                    if (this.localVariables.Peek()[name].Defined)
                    {
                        return this.localVariables.Peek()[name];
                    }
                }
                else if (this.variables.ContainsKey(name))
                {
                    return this.variables[name];
                }

                return Variable.Undefined;
            }
            set
            {
                if (this.localVariables.Count > 0)
                {
                    if (this.localVariables.Peek()[name].Defined)
                    {
                        this.localVariables.Peek()[name] = value;
                        return;
                    }
                }
                if (!this.variables.ContainsKey(name))
                {
                    this.variables.Add(name, value);
                }

                this.variables[name] = value;
            }
        }

        private VariableStore()
        {
            this.localVariables = new Stack<VariableStore>();

            this.Initialize();
        }

        public void PushUserFunction(VariableStore variables)
        {
            this.localVariables.Push(variables);
        }

        public void PopUserFunction()
        {
            this.localVariables.Pop();
        }

        private void Initialize()
        {
            this.variables = new Dictionary<string, Variable>
            {
                { 
                    "assign", 
                    new VarFunction
                    (
                        vars => 
                            {
                                VarString name = (VarString)vars[0];

                                this[name.Value] = vars[1];

                                return vars[1];
                            }
                    )
                },
                { 
                    "return", 
                    new VarFunction
                    (
                        vars => 
                            {
                                return vars[0];
                            }
                    )
                },
                { 
                    "print", 
                    new VarFunction
                    (
                        vars => 
                            {
                                Variable var = vars[0];

                                Console.WriteLine(var);
                                return var;
                            }
                    )
                },
                { 
                    "add", 
                    new VarFunction
                    (
                        vars => 
                            {
                                VarNumber first = (VarNumber)vars[0];
                                VarNumber second = (VarNumber)vars[1];

                                return first + second;
                            }
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
                            }
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
                            }
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
                            }
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
                            }
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
                            }
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
                            }
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
                                
                            }
                    )
                },
                {
                    "randfloat",
                    new VarFunction
                    (
                        vars =>
                            {
                                return new VarNumber(Program.Random.NextDouble());
                            }
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
