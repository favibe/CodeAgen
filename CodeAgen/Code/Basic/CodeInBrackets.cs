using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public class CodeInBrackets : CodeUnit
    {
        private readonly CodeUnit _unit;

        public CodeInBrackets(CodeUnit unit)
        {
            _unit = unit;
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.Write(CodeMarkups.OpenBracket);
            _unit.Build(output);
            output.Write(CodeMarkups.CloseBracket);
        }
    }
}