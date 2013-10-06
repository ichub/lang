using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lang
{
    public static class Script
    {
        public static string VariableNamePattern { get; private set; }
        public static string StatementPattern { get; private set; }

        static Script()
        {
            VariableNamePattern = @"([a-z]|[A-Z])+";
            StatementPattern = String.Format(@"\(\s*{0}(\s*{0})*\s*\)", VariableNamePattern);
        }

        static Variable ExecuteStatement(string statement)
        {
            return null;
        }

        static Variable ExecuteExpression(string expression)
        {
            return null;
        }
    }
}
