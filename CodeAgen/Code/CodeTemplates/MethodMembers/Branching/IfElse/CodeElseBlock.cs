using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching.IfElse
{
    public sealed class CodeElseBlock : CodeBracedBlock
    {
        public CodeElseBlock(CodeTabbable code)
        {
            code.Parent = this;

            AddUnit(code);
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(CodeKeywords.Else);
            output.NextLine();
            
            base.OnBuild(output);
        }
    }
}