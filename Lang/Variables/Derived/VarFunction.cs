using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VarFunction : Variable<Func<Node[], Variable>>
    {
        public VarFunction(Func<Node[], Variable> value = null) :
            base(value ?? (a => a[0].Value))
        {
            this.Type = VariableType.Function;
        }

        public virtual Variable Invoke(List<Node> parameters, Script script = null)
        {
            return this.Value.Invoke(parameters.ToArray());
        }

        public override string ToString()
        {
            return "Function";
        }
    }
}
