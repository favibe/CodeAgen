using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public class CodeLoopFor : CodeLoop
    {
        private readonly CodeUnit _condition;

        public CodeLoopFor(CodeUnit condition)
        {
            _condition = condition;
        }

        public CodeLoopFor(CodeRawString condition)
        {
            _condition = condition;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.For}{CodeMarkups.OpenBracket}");
            _condition.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}