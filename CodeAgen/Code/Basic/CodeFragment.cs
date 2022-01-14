using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code fragment to store one or more code units
    /// </summary>
    public class CodeFragment : CodeTabbable
    {
        private readonly List<CodeUnit> _units = new List<CodeUnit>();

        public CodeFragment AddUnit(CodeUnit unit)
        {
            if (unit is CodeTabbable tabbable)
            {
                tabbable.Parent = this;
            }
            
            _units.Add(unit);

            return this;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);

            foreach (var unit in _units)
            {
                unit.Build(output);
            }

            output.SetTab(Level);
        }

        protected override int GetNextTabLevel()
        {
            return Level;
        }
    }
}