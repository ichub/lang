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
            string[] variables = Lang.LangSpec.FindVariables("((1234),((69, 3), 4))");
        }
    }
}