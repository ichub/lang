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
        ListItemSeparator,
        ExpressionOpen,
        ExpressionClose,
        FuncOpen,
        FuncClose,
        ListOpen,
        ListClose,
        Number,
        Boolean,
        String,
        Variable,
    }
}
