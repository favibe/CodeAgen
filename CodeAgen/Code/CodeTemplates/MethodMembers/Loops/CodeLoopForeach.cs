using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public class CodeLoopForeach : CodeLoop
    {
        private readonly CodeUnit _condition;

        public CodeLoopForeach(CodeUnit condition)
        {
            _condition = condition;
        }

        public CodeLoopForeach(CodeRawString condition)
        {
            _condition = condition;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Foreach}{CodeMarkups.OpenBracket}");
            _condition.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}