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
        private readonly List<CodeUnit> _units;
        public bool IncreaseTabulation { get; set; }

        public CodeFragment()
        {
            _units = new List<CodeUnit>();
        }

        public CodeFragment(params CodeLine[] lines)
        {
            // TODO: покрыть тестами
            
            _units = new List<CodeUnit>();

            for (int i = 0; i < lines.Length; i++)
            {
                _units.Add(lines[i]);
                lines[i].Parent = this;
            }
        }

        public CodeFragment(List<CodeUnit> units)
        {
            _units = units;

            foreach (var unit in _units)
            {
                if (unit is CodeTabbable tabbable)
                {
                    tabbable.Parent = this;
                }
            }
        }
        
        public CodeFragment AddUnit(CodeUnit unit)
        {
            if (unit is CodeTabbable tabbable)
            {
                tabbable.Parent = this;
            }
            
            _units.Add(unit);

            return this;
        }

        public CodeFragment Clear()
        {
            _units.Clear();
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
            if (!IncreaseTabulation)
            {
                return Level;
            }
            else
            {
                return Level + 1;
            }
        }
    }
}