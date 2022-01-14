using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    public class CodeNamespace : CodeBracedBlock
    {
        private readonly CodeNameNamespace _name;
        public CodeNamespace(CodeNameNamespace name)
        {
            _name = name;
        }
        public override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Namespace}{CodeMarkups.Space}");
            _name.OnBuild(output);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}