using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassParameter : CodeRaw
    {
        private readonly CodeName _name;
        private readonly CodeType _type;
        private readonly string _defaultValue;
        public bool HasDefaultValue => _defaultValue != null;
        
        public CodeClassParameter(CodeNameVar name, CodeType type, string defaultValue = null)
        {
            _name = name;
            _type = type;
            _defaultValue = defaultValue;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.Write(_type);
            output.Write(CodeMarkups.Space);
            _name.Build(output);

            if (!HasDefaultValue)
            {
                return;
            }
            
            output.Write(CodeMarkups.Assignment);
            output.Write(_defaultValue);
        }
    }
}