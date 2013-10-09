using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VarBoolean : Variable<bool>
    {
        public VarBoolean(bool value) :
            base(value)
        {
            this.Type = VariableType.Boolean;
        }

        public static VarBoolean operator&(VarBoolean first, VarBoolean second)
        {
            return new VarBoolean(first.Value & second.Value);
        }

        public static VarBoolean operator|(VarBoolean first, VarBoolean second)
        {
            return new VarBoolean(first.Value | second.Value);
        }

        public static VarBoolean operator ^(VarBoolean first, VarBoolean second)
        {
            return new VarBoolean(first.Value ^ second.Value);
        }

        public static VarBoolean operator !(VarBoolean boolean)
        {
            return new VarBoolean(!boolean.Value);
        }
    }
}
