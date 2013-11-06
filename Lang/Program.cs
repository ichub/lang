using System;
using System.Collections.Generic;
using System.IO;
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
            string script = ReadFile(@"C:\Users\Ivan\Dev\Lang\Lang\test.txt");
            Script s = new Script(script);

            Variable a = s.Evaluate();

            Console.WriteLine(a);
            Console.ReadLine();
        }

        static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
