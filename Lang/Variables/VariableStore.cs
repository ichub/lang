using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VariableStore
    {
        public static VariableStore Default { get; private set; }

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
        static VariableStore()
        {
            Default = new VariableStore();
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
                    "subtract",
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
                }
            };
        }
    }
}
