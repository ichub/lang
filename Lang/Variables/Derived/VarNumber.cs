using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarNumber : Variable<float>
    {
        public VarNumber(float value) : 
            base(value)
        {
            this.Type = VariableType.Number;
        }
    }
}
