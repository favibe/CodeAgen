using System;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching.Switch;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching
{
    public class CodeSwitch : CodeBracedBlock
    {
        private readonly CodeNameVar _variable;

        private CodeFragment _cases;
        private CodeFragment _default;

        public CodeSwitch(CodeNameVar variable)
        {
            _variable = variable;
            _cases = new CodeFragment();
            AddUnit(_cases);
            _default = new CodeFragment();
            AddUnit(_default);
        }

        public CodeSwitch Case(CodeSwitchCase @case)
        {
            _cases.AddUnit(@case);
            return this;
        }

        public CodeSwitch Default(CodeSwitchDefault code)
        {
            _default.Clear();
            _default.AddUnit(code);
            
            return this;
        }

        [Obsolete("Use Case and Default instead", false)]
        public override CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            return base.AddUnit(unit);
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Switch}{CodeMarkups.OpenBracket}");
            _variable.Build(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
            base.OnBuild(output);
        }
    }
}