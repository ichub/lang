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
        public int Arity { get; protected set; }

        public VarFunction(int arity, Func<Node[], Variable> value = null) :
            base(value ?? (a => a[0].Value))
        {
            this.Arity = arity;
            this.Type = VariableType.Function;
        }

        public virtual Variable Invoke(List<Node> parameters, Script script = null)
        {
            if (parameters.Count != this.Arity)
            {
                throw new Exception("Number of arguments does not match the function");
            }

            return this.Value.Invoke(parameters.ToArray());
        }

        public override string ToString()
        {
            return "Function";
        }
    }
}
