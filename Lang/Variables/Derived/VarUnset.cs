using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarUnset : Variable
    {
        public VarUnset()
        {
            this.Type = VariableType.Unset;
        }
    }
}
