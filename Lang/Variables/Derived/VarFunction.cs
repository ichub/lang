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
        public VariableType[] VariableTypes { get; private set; }
        public int AmountOfArgs { get; private set; }

        public VarFunction(Func<Variable[], Variable> value, VariableType[] variableTypes) :
            base(value)
        {
            this.Type = VariableType.Function;

            this.VariableTypes = variableTypes;
            this.AmountOfArgs = variableTypes.Length;
        }

        public Variable Apply(List<Node> parameters)
        {
            return this.Value.Invoke(parameters.Map(node => node.Value).ToArray());
        }
    }
}
