using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class ScopedVariableStore : VariableStore
    {
        public int Depth { get { return this.scopes.Count; } }

        private Stack<VariableStore> scopes;

        public ScopedVariableStore()
        {
            this.scopes = new Stack<VariableStore>();

            VariableStore.InsertDefaultVariables(this);
        }

        public void PushScope(VariableStore scope)
        {
            this.scopes.Push(scope);
        }

        public void PopScope()
        {
            this.scopes.Pop();
        }

        public override bool ContainsVariable(string name)
        {
            bool scopeContainsVariable = false;

            if (this.scopes.Count > 0)
            {
                scopeContainsVariable = this.scopes.Peek().ContainsVariable(name);
            }

            return base.ContainsVariable(name) || scopeContainsVariable;
        }

        public Variable GetVariable(string name, VariableScope scope = VariableScope.All)
        {
            Variable global = base.GetVariable(name);
            Variable local = Variable.Undefined;

            if (this.scopes.Count > 0)
            {
                local = this.scopes.Peek().GetVariable(name);
            }

            switch (scope)
            {
                case VariableScope.All:
                    return local.Defined ? local : global;
                case VariableScope.Local:
                    return local;
                case VariableScope.Global:
                    return global;
                default:
                    return Variable.Undefined;
            }
        }

        public void SetVariable(string name, Variable value, VariableScope scope = VariableScope.All)
        {
            if (!this.ContainsVariable(name))
            {
                throw new Exception(String.Format("Variable {0} does ot exist in the current context", name));
            }

            if (scope != VariableScope.Global) // local or all
            {
                if (scope == VariableScope.All) // all
                {
                    if (this.scopes.Count > 0) // contains non-global variables
                    {
                        if (this.scopes.Peek().ContainsVariable(name)) // if topmost non-global store contains the variable
                        {
                            this.scopes.Peek().SetVariable(name, value);
                            return;
                        }
                    }
                }
            }

            base.SetVariable(name, value); // just global or all with no local variables
        }
    }
}
