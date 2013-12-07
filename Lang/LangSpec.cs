using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lang
{
    public static class LangSpec
    {
        private const char expressionSeparator = ';';
        private const char variableSeparator = ',';
        private const char functionVariableSeparator = ':';
        private const char expressionOpen = '(';
        private const char expressionClose = ')';
        private const char funcOpen = '[';
        private const char funcClose = ']';

        private static string functionLiteralPattern;
        private static string numberLiteralPattern;
        private static string booleanLiteralPattern;
        private static string variablePattern;
        private static string stringLiteralPattern;
        private static string listLiteralPattern;

        private static Regex functionLiteral;
        private static Regex numberLiteral;
        private static Regex booleanLiteral;
        private static Regex stringLiteral;
        private static Regex variable;
        private static Regex whiteSpace;
        private static Regex listLiteral;

        static LangSpec()
        {
            functionLiteralPattern = @"^\[.*\]\[.*\]$";
            variablePattern = @"^([a-z]|[A-Z])+$";
            numberLiteralPattern = @"^\-?[0-9]+(\.[0-9])*$";
            booleanLiteralPattern = @"^(True)|(False)$";
            stringLiteralPattern = "^\"([a-z][A-Z])*\"$";
            listLiteralPattern = "^{.*}$";

            functionLiteral = new Regex(functionLiteralPattern);
            numberLiteral = new Regex(numberLiteralPattern);
            booleanLiteral = new Regex(booleanLiteralPattern);
            stringLiteral = new Regex(stringLiteralPattern);
            variable = new Regex(variablePattern);
            listLiteral = new Regex(listLiteralPattern);

            whiteSpace = new Regex(@"\s+|\\r|\\n");
        }

        public static string StripWhitespace(string input)
        {
            return whiteSpace.Replace(input, String.Empty);
        }

        public static bool IsLiteral(Script script, Node parent, string input)
        {
            return GetLiteral(script, parent, input) != null;
        }

        public static bool IsVariable(string input)
        {
            return variable.IsMatch(input);
        }

        public static bool IsFunctionInvocation(string input)
        {
            return input[0] == expressionOpen && input[input.Length - 1] == expressionClose;
        }

        public static Variable GetLiteral(Script script, Node parent, string input)
        {
            if (numberLiteral.IsMatch(input))
            {
                return new VarNumber(double.Parse(input));
            }
            else if (booleanLiteral.IsMatch(input))
            {
                return new VarBoolean(bool.Parse(input));
            }
            else if (stringLiteral.IsMatch(input))
            {
                return new VarString(input.Substring(1, input.Length - 2));
            }
            else if (functionLiteral.IsMatch(input))
            {
                return VarUserFunction.Parse(script, input);
            }
            else if (listLiteral.IsMatch(input))
            {
                return VarList.Parse(script, parent, input);
            }

            return null; // no literal match found
        }

        /// <summary>
        /// Divides a string into sections, defined by the separator, open and closing characters. Takes into account nested sections, assuming that those sections
        /// use the same seprator, open and close characters. An example:
        /// 
        /// DivideIntoParts("(1, 2, (3, 4)", ',', '(', ')'); Would return the following array:
        /// 
        /// { "1", "2", "(3, 4)" }
        /// </summary>
        public static string[] DivideIntoParts(string expression, char divider = variableSeparator, char parenOpen = expressionOpen, char parenClose = expressionClose)
        {
            List<string> expressions = new List<string>();

            string accumulator = String.Empty;
            int parensCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (currentChar == parenOpen)
                {
                    if (i == 0)
                    {
                        continue; // ignore first parens
                    }

                    parensCount++;
                }
                else if (currentChar == parenClose)
                {
                    if (i == expression.Length - 1)
                    {
                        continue; // ignore last parens
                    }

                    parensCount--;
                }

                if (parensCount == 0)
                {
                    if (currentChar == divider)
                    {
                        expressions.Add(accumulator);
                        accumulator = String.Empty;
                        continue;
                    }
                }

                accumulator += currentChar;
            }

            expressions.Add(accumulator);

            return expressions.ToArray();
        }

        public static string[] GetExpressions(string script)
        {
            return script.Split(expressionSeparator).Where(a => a != String.Empty).ToArray();
        }

        public static Tuple<Expression, string[]> GetFunctionLiteralParts(Script script, string functionLiteral)
        {
            string[] parts = functionLiteral.Split(new[] { funcOpen, funcClose }).Where(a => a != String.Empty).ToArray();
            string[] names = parts[0].Split(functionVariableSeparator).Where(a => a != String.Empty).ToArray();

            Expression expression = new Expression(script, parts[1]);

            return new Tuple<Expression, string[]>(expression, names);
        }
    }
}
