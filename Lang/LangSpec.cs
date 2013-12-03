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
        private static char expressionSeparator;
        private static char variableSeparator;
        private static char functionVariableSeparator;
        private static string functionLiteralPattern;
        private static string numberLiteralPattern;
        private static string booleanLiteralPattern;
        private static string variablePattern;
        private static string stringLiteralPattern;

        private static Regex functionLiteral;
        private static Regex numberLiteral;
        private static Regex booleanLiteral;
        private static Regex stringLiteral;
        private static Regex variable;
        private static Regex whiteSpace;

        static LangSpec()
        {
            expressionSeparator = ';';
            variableSeparator = ',';
            functionVariableSeparator = ':';
            functionLiteralPattern = @"^\[.*\]\[.*\]$";
            variablePattern = @"^([a-z]|[A-Z])+$";
            numberLiteralPattern = @"^\-?[0-9]+(\.[0-9])*$";
            booleanLiteralPattern = @"^(True)|(False)$";
            stringLiteralPattern = "^\".*\"$";

            functionLiteral = new Regex(functionLiteralPattern);
            numberLiteral = new Regex(numberLiteralPattern);
            booleanLiteral = new Regex(booleanLiteralPattern);
            stringLiteral = new Regex(stringLiteralPattern);
            variable = new Regex(variablePattern);

            whiteSpace = new Regex(@"\s+|\\r|\\n");
        }

        public static string StripWhitespace(string input)
        {
            return whiteSpace.Replace(input, "");
        }

        public static bool IsLiteral(Script script, string input)
        {
            return GetLiteral(script, input) != null;
        }

        public static bool IsVariable(string input)
        {
            return variable.IsMatch(input);
        }

        public static bool IsFunctionInvocation(string input)
        {
            input = StripWhitespace(input);

            return input[0] == '(' && input[input.Length - 1] == ')';
        }

        public static Variable GetLiteral(Script script, string input)
        {
            input = StripWhitespace(input);

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

            return null; // no literal match found
        }

        public static string[] DivideExpressions(string expression)
        {
            expression = StripWhitespace(expression);

            List<string> expressions = new List<string>();

            string accumulator = "";
            int parensCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (currentChar == '(')
                {
                    if (i == 0)
                    {
                        continue; // ignore first parens
                    }

                    parensCount++;
                }
                else if (currentChar == ')')
                {
                    if (i == expression.Length - 1)
                    {
                        continue; // ignore last parens
                    }

                    parensCount--;
                }

                if (parensCount == 0)
                {
                    if (currentChar == variableSeparator)
                    {
                        expressions.Add(accumulator);
                        accumulator = "";
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
            string stripped = whiteSpace.Replace(script, "");

            return stripped.Split(expressionSeparator).Where(a => a != String.Empty).ToArray();
        }

        public static Tuple<Expression, string[]> GetFunctionLiteralParts(Script script, string functionLiteral)
        {
            string[] parts = functionLiteral.Split(new[] {'[', ']'}).Where(a => a != String.Empty).ToArray();
            string[] names = parts[0].Split(functionVariableSeparator).Where(a => a != String.Empty).ToArray();

            Expression expression = new Expression(script, parts[1]);

            return new Tuple<Expression, string[]>(expression, names);
        }
    }
}
