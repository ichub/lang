﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Variable
    {
        public bool Defined
        {
            get
            {
                return !(this is VarUndefined);
            }
        }

        public static Variable Undefined
        {
            get
            {
                return new VarUndefined();
            }
        }

        public static Variable Unset
        {
            get
            {
                return new VarUnset();
            }
        }

        public VariableType Type { get; protected set; }

        public override string ToString()
        {
            return this.Type.ToString();
        }
    }

    public class Variable<T> : Variable
    {
        public T Value { get; set; }

        public Variable(T value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.Value.ToString();
        }
    }
}
