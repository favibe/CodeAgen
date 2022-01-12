using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public class CodeLine : CodeTabbable
    {
        private readonly List<CodeUnit> _units = new List<CodeUnit>();

        public CodeLine AddUnit(CodeUnit unit)
        {
            _units.Add(unit);
            return this;
        }
        
        public override void Build(ICodeOutput output)
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

                output.Write(CodeMarkups.Semicolon);
            }

            output.NextLine();
        }
    }
}