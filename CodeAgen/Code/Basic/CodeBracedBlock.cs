using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code block enclosed in curly brackets
    /// </summary>
    public class CodeBracedBlock : CodeTabbable
    {
        private readonly List<CodeTabbable> _units = new List<CodeTabbable>();

        /// <summary>
        /// Add code unit to code block
        /// </summary>
        /// <param name="unit">Code unit</param>
        /// <returns></returns>
        public virtual CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            _units.Add(unit);
            return this;
        }

        protected override void PreBuild()
        {
            // Adding one level to child code units
            
            foreach (var unit in _units)
            {
                unit.Level = Level + 1;
            }
        }

        protected override void OnBuild(ICodeOutput output)
        {
            // Print curly brackets and code inside
            
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