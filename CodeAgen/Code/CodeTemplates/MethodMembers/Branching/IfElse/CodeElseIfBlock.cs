using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching.IfElse
{
    public sealed class CodeElseIfBlock : CodeBracedBlock
    {
        private readonly CodeConditionChain _condition;

        public CodeElseIfBlock(CodeConditionChain condition, CodeTabbable code)
        {
            _condition = condition;
            code.Parent = this;

            AddUnit(code);
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Else}{CodeMarkups.Space}{CodeKeywords.If}{CodeMarkups.Space}{CodeMarkups.OpenBracket}");
            _condition.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            
            base.OnBuild(output);
        }
    }
}