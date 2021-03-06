﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class VarNumber : Variable<double>
    {
        public VarNumber(double value = 0) : 
            base(value)
        {
            this.Type = VariableType.Number;
        }

        public static VarNumber operator+(VarNumber first, VarNumber second)
        {
            return new VarNumber(first.Value + second.Value);
        }

        public static VarNumber operator-(VarNumber first, VarNumber second)
        {
            return new VarNumber(first.Value - second.Value);
        }

        public static VarNumber operator*(VarNumber first, VarNumber second)
        {
            return new VarNumber(first.Value * second.Value);
        }

        public static VarNumber operator/(VarNumber first, VarNumber second)
        {
            return new VarNumber(first.Value / second.Value);
        }
    }
}
