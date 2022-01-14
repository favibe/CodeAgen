using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public class CodeLoopDoWhile : CodeLoop
    {
        private readonly CodeUnit _condition;

        public CodeLoopDoWhile(CodeUnit condition)
        {
            _condition = condition;
        }

        public CodeLoopDoWhile(CodeRawString condition)
        {
            _condition = condition;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(CodeKeywords.Do);
            output.NextLine();
            base.OnBuild(output);
            output.SetTab(Level);
            output.Write($"{CodeKeywords.While}{CodeMarkups.OpenBracket}");
            _condition.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}