using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public static class LangSpec
    {
        public static char VariableSeparator { get; private set; }

        static LangSpec()
        {
            VariableSeparator = ',';
        }

        public static string[] FindVariables(string expression)
        {
            string bareExpression = expression.Replace(" ", "");

            bareExpression = bareExpression.Substring(1, bareExpression.Length - 2);

            List<string> variables = new List<string>();

            string accumulator = "";
            int parensCount = 0;

            for (int i = 0; i < bareExpression.Length; i++)
            {
                char currentChar = bareExpression[i];

                if (currentChar == '(')
                {
                    parensCount++;
                }
                else if (currentChar == ')')
                {
                    parensCount--;
                }

                if (parensCount == 0)
                {
                    if (currentChar == ',')
                    {
                        variables.Add(accumulator);
                        accumulator = "";
                        continue;
                    }
                }

                accumulator += currentChar;
            }

            variables.Add(accumulator);

            return variables.ToArray();
        }
    }
}
