using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public class CodeBracedBlock : CodeTabbable
    {
        private readonly List<CodeTabbable> _units = new List<CodeTabbable>();

        public virtual CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            _units.Add(unit);
            return this;
        }

        protected override void PreBuild()
        {
            foreach (var unit in _units)
            {
                unit.Level = Level + 1;
            }
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(CodeMarkups.OpenCurlyBracket);
            output.NextLine();
            
            foreach (var unit in _units)
            {
                unit.Build(output);
            }

            output.SetTab(Level);
            output.Write(CodeMarkups.CloseCurlyBracket);
            output.NextLine();
        }
    }
}