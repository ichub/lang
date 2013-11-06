using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class Program
    {
        public static Random Random { get; private set; }

        static Program()
        {
            Random = new Random();
        }

        static void Main(string[] args)
        {
            //while (true)
            //{
            //    Console.Write("> ");

            //    string script = Console.ReadLine();

            //    SyntaxTree tree = new SyntaxTree(script);

            //    Console.Write("> ");
            //    Console.WriteLine(tree.Evaluate());
            //    Console.WriteLine();
            //}

            while (true)
            {
                string script = Console.ReadLine();

                SyntaxTree tree = new SyntaxTree(script);
                Variable var = tree.Evaluate();

                Console.WriteLine(var);
            }
        }
    }
}