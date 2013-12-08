using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarString : Variable<string>
    {
        public VarString(string value = "")
            : base(value)
        {
            this.Type = VariableType.String;
        }
    }
}
