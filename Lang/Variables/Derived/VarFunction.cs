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
        public VarFunction(Func<Variable[], Variable> value) :
            base(value)
        {
            this.Type = VariableType.Function;
        }

        public virtual Variable Invoke(List<Node> parameters)
        {
            return this.Value.Invoke(parameters.Select(node => node.Value).ToArray());
        }

        public override string ToString()
        {
            return "Function";
        }
    }
}
