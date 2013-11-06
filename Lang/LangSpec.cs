﻿using System;
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
        private static string numberLiteralPattern;
        private static string booleanLiteralPattern;
        private static string variablePattern;
        private static string stringLiteralPattern;

        private static Regex numberLiteral;
        private static Regex booleanLiteral;
        private static Regex stringLiteral;
        private static Regex variable;
        private static Regex whiteSpace;

        static LangSpec()
        { 
            expressionSeparator = ';';
            variableSeparator = ',';
            variablePattern = @"^([a-z]|[A-Z])+$";
            numberLiteralPattern = @"^\-?[0-9]+(\.[0-9])*$";
            booleanLiteralPattern = @"^(True)|(False)$";
            stringLiteralPattern = "^\".*\"$";

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

        public static bool IsLiteral(string input)
        {
            return GetLiteral(input) != null;
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

        public static Variable GetLiteral(string script)
        {
            script = StripWhitespace(script);

            if (numberLiteral.IsMatch(script))
            {
                return new VarNumber(double.Parse(script));
            }
            else if (booleanLiteral.IsMatch(script))
            {
                return new VarBoolean(bool.Parse(script));
            }
            else if (stringLiteral.IsMatch(script))
            {
                return new VarString(script.Substring(1, script.Length - 2));
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
    }
}
