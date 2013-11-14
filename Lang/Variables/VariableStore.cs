﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VariableStore
    {
        public static VariableStore Empty { get { return new VariableStore(); } }

        private Dictionary<string, Variable> variables;

        protected VariableStore()
        {
            this.variables = new Dictionary<string, Variable>();
        }

        public virtual void CreateVariable(string name)
        {
            this.variables.Add(name, Variable.Unset);
        }

        public virtual Variable GetVariable(string name)
        {
            if (this.variables.ContainsKey(name))
            {
                return this.variables[name];
            }

            return Variable.Undefined;
        }

        public virtual void SetVariable(string name, Variable value)
        {
            if (this.variables.ContainsKey(name))
            {
                this.variables[name] = value;
                return;
            }

            throw new Exception(String.Format("Variable {0} does not exist in the current context", name));
        }

        public virtual bool ContainsVariable(string name)
        {
            return this.variables.ContainsKey(name);
        }

        protected static void InsertDefaultVariables(VariableStore store)
        {
            store.variables = new Dictionary<string, Variable>
            {
                { 
                    "assign", 
                    new VarFunction
                    (
                        vars => 
                            {
                                VarString name = (VarString)vars[0].Value;

                                if (!store.ContainsVariable(name.Value))
                                {
                                    store.CreateVariable(name.Value);
                                }

                                store.SetVariable(name.Value, vars[1].Value);

                                return vars[1].Value;
                            }
                    )
                },
                { 
                    "return", 
                    new VarFunction
                    (
                        vars => 
                            {
                                return vars[0].Value;
                            }
                    )
                },
                { 
                    "print", 
                    new VarFunction
                    (
                        vars => 
                            {
                                Variable var = vars[0].Value;

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
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

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
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

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
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

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
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

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
                                VarBoolean first = (VarBoolean)vars[0].Value;
                                VarBoolean second = (VarBoolean)vars[1].Value;

                                return first & second;
                            }
                    )
                },
                 {
                    "not",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean boolean = (VarBoolean)vars[0].Value;

                                return !boolean;
                            }
                    )
                },
                {
                    "or",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarBoolean first = (VarBoolean)vars[0].Value;
                                VarBoolean second = (VarBoolean)vars[1].Value;

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
                                VarBoolean first = (VarBoolean)vars[0].Value;
                                VarBoolean second = (VarBoolean)vars[1].Value;

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
                                VarBoolean decider = (VarBoolean)vars[0].Value;

                                return decider.Value ? vars[1].Value : vars[2].Value;
                                
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
                    "less",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

                                return new VarBoolean(first.Value < second.Value);
                            }
                    )
                },
                {
                    "more",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

                                return new VarBoolean(first.Value > second.Value);
                            }
                    )
                },
                {
                    "equal",
                    new VarFunction
                    (
                        vars =>
                            {
                                VarNumber first = (VarNumber)vars[0].Value;
                                VarNumber second = (VarNumber)vars[1].Value;

                                return new VarBoolean(first.Value == second.Value);
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
