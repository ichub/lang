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
            while (true)
            {
                Console.Write("> ");
                string scriptPath = Console.ReadLine();

                Script sc = new Script(ReadFile(scriptPath));

                Console.WriteLine();
                Console.WriteLine(sc.Evaluate());
            }
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