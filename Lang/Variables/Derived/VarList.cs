using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    class VarList : Variable<List<Variable>>
    {
        public int Length { get { return this.Value.Count; } }

        public VarList()
            : base(new List<Variable>())
        {

        }

        public void Add(Variable variable)
        {
            this.Value.Add(variable);
        }

        public Variable Pop()
        {
            Variable variable = this.Value[this.Value.Count - 1];
            this.Value.Remove(variable);

            return variable;
        }

        public Variable GetAt(int index)
        {
            return this.Value.ToArray()[index];
        }

        public void Set(int index, Variable value)
        {
            this.Value[index] = value;
        }

        public static VarList Parse(Script script, Node parent, string input)
        {
            string[] parts = LangSpec.DivideExpressions(input, '|', '{', '}');

            VarList list = new VarList();

            for (int i = 0; i < parts.Length; i++)
            {
                list.Add(Node.Parse(script, parent, parts[i]).Value);
            }

            return list;
        }

        public override string ToString()
        {
            string str = "{ ";

            for (int i = 0; i < this.Value.Count; i++)
            {
                str += this.Value[i].ToString() + " ";

                if (i != this.Value.Count - 1)
                {
                    str += "| ";
                }
            }

            str += "}";

            return str;
        }
    }
}
