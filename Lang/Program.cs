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
            Script script = new Script("(add, 3, (add, 1, 2))");
            Console.WriteLine(script.Execute());
            Console.ReadLine();
        }
    }
}