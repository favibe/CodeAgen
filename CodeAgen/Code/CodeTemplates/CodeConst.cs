using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    public class CodeConst : CodeTabbable
    {
        private readonly CodeType _type;
        private readonly CodeNameVar _name;
        private readonly CodeUnit _value;
        private readonly CodeAccessModifier _access;

        public CodeConst(CodeType type, CodeNameVar name, CodeUnit value, CodeAccessModifier access = null)
        {
            _type = type;
            _name = name;
            _value = value;
            _access = access ?? CodeAccessModifier.Public;
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_access);
            output.Write($"{CodeMarkups.Space}{CodeKeywords.Const}{CodeMarkups.Space}");
            _type.Build(output);
            output.Write(CodeMarkups.Space);
            _name.Build(output);
            output.Write(CodeMarkups.Assignment);
            _value.Build(output);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}