using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    /// <summary>
    /// Code template for namespace
    /// </summary>
    public class CodeNamespace : CodeBracedBlock
    {
        private readonly CodeNameNamespace _name;
        public CodeNamespace(CodeNameNamespace name)
        {
            _name = name;
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Namespace}{CodeMarkups.Space}");
            _name.Build(output);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}