using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarUndefined : Variable
    {
        public VarUndefined()
        {
            this.Type = VariableType.Undefined;
        }
    }
}
