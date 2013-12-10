using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    enum TokenType
    {
        Unknown,
        ExpressionSeparator,
        VariableSeparator,
        FunctionVariableSeparator,
        ExpressionOpen,
        ExpressionClose,
        FuncOpen,
        FuncClose,
        Number,
        Boolean,
        String,
        Variable,
    }
}
