using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.Loops
{
    public class CodeForLoop : CodeBracedBlock
    {
        private readonly CodeUnit _condition;

        public CodeForLoop(CodeUnit condition)
        {
            _condition = condition;
        }

        public CodeForLoop(CodeRawString condition)
        {
            _condition = condition;
        }

        public override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.For}{CodeMarkups.OpenBracket}");
            _condition.OnBuild(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}