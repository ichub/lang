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
        public static char VariableSeparator { get; private set; }
        public static string NumberLiteralPattern { get; private set; }
        public static string BooleanLiteralPattern { get; private set; }

        public static Regex NumberLiteral { get; private set; }
        public static Regex BooleanLiteral { get; private set; }

        static LangSpec()
        {
            VariableSeparator = ',';
            NumberLiteralPattern = @"\-?[0-9]+(\.[0-9])*";
            BooleanLiteralPattern = @"(True)|(False)";

            NumberLiteral = new Regex(NumberLiteralPattern);
            BooleanLiteral = new Regex(BooleanLiteralPattern);
        }

        public static Variable GetLiteral(string script)
        {
            string parsed = script.Replace(" ", "");

            if (NumberLiteral.IsMatch(parsed))
            {
                return new VarNumber(double.Parse(parsed));
            }
            else if (BooleanLiteral.IsMatch(parsed))
            {
                return new VarBoolean(bool.Parse(parsed));
            }

            return null; // no literal match found
        }

        public static string[] DivideExpressions(string expression)
        {
            string bareExpression = expression.Replace(" ", "");

            List<string> expressions = new List<string>();

            string accumulator = "";
            int parensCount = 0;

            for (int i = 0; i < bareExpression.Length; i++)
            {
                char currentChar = bareExpression[i];

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
                    if (i == bareExpression.Length - 1)
                    {
                        continue; // ignore last parens
                    }

                    parensCount--;
                }

                if (parensCount == 0)
                {
                    if (currentChar == VariableSeparator)
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
    }
}
