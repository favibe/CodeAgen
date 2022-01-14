using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public class CodeWhileLoop : CodeBracedBlock
    {
        private readonly CodeUnit _condition;

        public CodeWhileLoop(CodeUnit condition)
        {
            _condition = condition;
        }
        
        public CodeWhileLoop(CodeRawString condition)
        {
            _condition = condition;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.While}{CodeMarkups.OpenBracket}");
            _condition.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}