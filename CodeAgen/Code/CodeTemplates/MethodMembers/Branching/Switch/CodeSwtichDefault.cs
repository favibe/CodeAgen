using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching.Switch
{
    public class CodeSwitchDefault : CodeTabbable 
    {
        private readonly CodeFragment _inner;

        public CodeSwitchDefault(CodeTabbable code = null, CodeTabbable end = null)
        {

            if (code == null && end == null)
            {
                return;
            }
            
            _inner = new CodeFragment
            {
                Parent = this
            };

            if (code != null)
            {
                _inner.AddUnit(code);
            }

            if (end != null)
            {
                _inner.AddUnit(end);
            }
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(CodeKeywords.Default);
            output.Write(CodeMarkups.Colon);
            output.NextLine();

            _inner?.Build(output);
        }
    }
}