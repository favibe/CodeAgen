using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code line with tabulations
    /// </summary>
    public class CodeLine : CodeTabbable
    {
        private readonly List<CodeUnit> _units = new List<CodeUnit>();
        private bool _semicolonEnabled;

        public CodeLine()
        {
            
        }
        
        public CodeLine(CodeUnit code)
        {
            _units.Add(code);
        }
        
        public CodeLine(string code)
        {
            _units.Add(new CodeRawString(code));
        }
        
        public CodeLine AddUnit(CodeUnit unit)
        {
            _units.Add(unit);
            return this;
        }

        public CodeLine SetSemicolon(bool isEnabled)
        {
            _semicolonEnabled = isEnabled;
            return this;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);

            var unitsCount = _units.Count;

            if (unitsCount > 0)
            {
                _units[0].Build(output);

                for (int index = 1; index < unitsCount; index++)
                {
                    _units[index].Build(output);
                }

                if (_semicolonEnabled)
                {
                    output.Write(CodeMarkups.Semicolon);
                }
            }

            output.NextLine();
        }
    }
}