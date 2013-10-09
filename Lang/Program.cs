using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class Program
    {
        static void Main(string[] args)
        {
            SyntaxTree script = new SyntaxTree("True");
            Console.WriteLine(script.Evaluate());
            Console.ReadLine();
        }
    }
}