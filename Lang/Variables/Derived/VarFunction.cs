using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VarFunction : Variable<Func<Variable[], Variable>>
    {
        public VariableType[] ArgTypes { get; private set; }
        public int AmountOfArgs { get; private set; }

        public VarFunction(Func<Variable[], Variable> value, VariableType[] argTypes) :
            base(value)
        {
            this.Type = VariableType.Function;

            this.ArgTypes = argTypes;
            this.AmountOfArgs = argTypes.Length;
        }

        public Variable Invoke(List<Node> parameters)
        {
            return this.Value.Invoke(parameters.Map(node => node.Value).ToArray());
        }

        public override string ToString()
        {
            return "Function";
        }
    }
}
