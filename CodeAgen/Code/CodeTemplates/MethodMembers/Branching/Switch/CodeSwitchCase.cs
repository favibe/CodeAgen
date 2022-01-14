using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers.Loops;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching.Switch
{
    public class CodeSwitchCase : CodeTabbable
    {
        private readonly CodeRawString _case;
        private readonly CodeFragment _inner;

        public CodeSwitchCase(CodeRawString @case, CodeTabbable code = null, CodeTabbable end = null)
        {
            _case = @case;

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
            output.Write($"{CodeKeywords.Case}{CodeMarkups.Space}");
            _case.Build(output);
            output.Write(CodeMarkups.Colon);
            output.NextLine();

            _inner?.Build(output);
        }
    }
}