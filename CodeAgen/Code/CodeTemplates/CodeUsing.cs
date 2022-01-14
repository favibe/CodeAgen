using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    /// <summary>
    /// Code template for using
    /// </summary>
    public class CodeUsing : CodeTabbable
    {
        private readonly CodeNameNamespace _name;
        
        public CodeUsing(CodeNameNamespace name) : base()
        {
            _name = name;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Using}{CodeMarkups.Space}");
            _name.Build(output);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}