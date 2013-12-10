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
        public const string ExpressionSeparator = ";";
        public const string VariableSeparator = ",";
        public const string FunctionVariableSeparator = ":";
        public const string ExpressionOpen = "(";
        public const string ExpressionClose = ")";
        public const string FuncOpen = "[";
        public const string FuncClose = "]";

        public static readonly string FunctionLiteralPattern;
        public static readonly string NumberLiteralPattern;
        public static readonly string BooleanLiteralPattern;
        public static readonly string VariablePattern;
        public static readonly string StringLiteralPattern;
        public static readonly string ListLiteralPattern;

        public static readonly Regex NumberLiteral;
        public static readonly Regex BooleanLiteral;
        public static readonly Regex StringLiteral;
        public static readonly Regex Variable;

        public static readonly Regex ExpressionSeparatorRegex;
        public static readonly Regex VariableSeparatorRegex;
        public static readonly Regex FunctionVariableSeparatorRegex;
        public static readonly Regex ExpressionOpenRegex;
        public static readonly Regex ExpressionCloseRegex;
        public static readonly Regex FuncOpenRegex;
        public static readonly Regex FuncCloseRegex;

        public static readonly Regex Whitespace;
        public static readonly Regex ListLiteral;
        public static readonly Regex FunctionLiteral;

        static LangSpec()
        {
            FunctionLiteralPattern = String.Format(@"^\{0}.*\{1}\{0}.*\{1}$", FuncOpen, FuncClose);
            VariablePattern = @"^([a-z]|[A-Z])+$";
            NumberLiteralPattern = @"^\-?[0-9]+(\.[0-9])*$";
            BooleanLiteralPattern = @"^(True)|(False)$";
            StringLiteralPattern = "^\"([a-z]|[A-Z])*\"$";
            ListLiteralPattern = "^{.*}$";

            ExpressionSeparatorRegex = new Regex("^" + Regex.Escape(ExpressionSeparator) + "$");
            VariableSeparatorRegex = new Regex("^" + Regex.Escape(VariableSeparator) + "$");
            FunctionVariableSeparatorRegex = new Regex("^" + Regex.Escape(FunctionVariableSeparator) + "$");
            ExpressionOpenRegex = new Regex("^" + Regex.Escape(ExpressionOpen) + "$");
            ExpressionCloseRegex = new Regex("^" + Regex.Escape(ExpressionClose) + "$");
            FuncOpenRegex = new Regex("^" + Regex.Escape(FuncOpen) + "$");
            FuncCloseRegex = new Regex("^" + Regex.Escape(FuncClose) + "$");

            FunctionLiteral = new Regex(FunctionLiteralPattern);
            NumberLiteral = new Regex(NumberLiteralPattern);
            BooleanLiteral = new Regex(BooleanLiteralPattern);
            StringLiteral = new Regex(StringLiteralPattern);
            Variable = new Regex(VariablePattern);
            ListLiteral = new Regex(ListLiteralPattern);

            Whitespace = new Regex(@"\s+|\\r|\\n");
        }

        public static string StripWhitespace(string input)
        {
            return Whitespace.Replace(input, String.Empty);
        }

        public static bool IsLiteral(string input)
        {
            return GetLiteral(input) != null;
        }

        public static bool IsVariable(string input)
        {
            return Variable.IsMatch(input);
        }

        public static bool IsFunctionInvocation(string input)
        {
            return input[0] == ExpressionOpen[0] && input[input.Length - 1] == ExpressionClose[0];
        }

        public static Variable GetLiteral(string input)
        {
            if (NumberLiteral.IsMatch(input))
            {
                return new VarNumber(double.Parse(input));
            }
            else if (BooleanLiteral.IsMatch(input))
            {
                return new VarBoolean(bool.Parse(input));
            }
            else if (StringLiteral.IsMatch(input))
            {
                return new VarString(input.Substring(1, input.Length - 2));
            }
            else if (FunctionLiteral.IsMatch(input))
            {
                return VarUserFunction.Parse(input);
            }
            else if (ListLiteral.IsMatch(input))
            {
                return VarList.Parse(input);
            }

            return null; // no literal match found
        }

        /// <summary>
        /// Divides a string into sections, defined by the separator, open and closing characters. Takes into account nested sections, assuming that those sections
        /// use the same seprator, open and close characters. An example:
        /// 
        /// DivideIntoParts("(1, 2, (3, 4))", ',', '(', ')'); Would return the following array:
        /// 
        /// { "1", "2", "(3, 4)" }
        /// </summary>
        public static string[] DivideIntoParts(string expression, string divider = VariableSeparator, string parenOpen = ExpressionOpen, string parenClose = ExpressionClose)
        {
            List<string> expressions = new List<string>();

            string accumulator = String.Empty;
            int parensCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (currentChar.ToString() == parenOpen)
                {
                    if (i == 0)
                    {
                        continue; // ignore first parens
                    }

                    parensCount++;
                }
                else if (currentChar.ToString() == parenClose)
                {
                    if (i == expression.Length - 1)
                    {
                        continue; // ignore last parens
                    }

                    parensCount--;
                }

                if (parensCount == 0)
                {
                    if (currentChar.ToString() == divider)
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
            return script.Split(ExpressionSeparator[0]).Where(a => a != String.Empty).ToArray();
        }

        public static Tuple<Expression, string[]> GetFunctionLiteralParts(string functionLiteral)
        {
            string[] parts = functionLiteral.Split(new[] { FuncOpen[0], FuncClose[0] }).Where(a => a != String.Empty).ToArray();
            string[] names = parts[0].Split(FunctionVariableSeparator[0]).Where(a => a != String.Empty).ToArray();

            Expression expression = new Expression(parts[1]);

            return new Tuple<Expression, string[]>(expression, names);
        }
    }
}
