using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    /// <summary>
    /// Parameter for code method
    /// </summary>
    public class CodeMethodParameter : CodeRaw
    {
        private readonly CodeName _name;
        private readonly CodeType _type;
        private readonly string _defaultValue;
        public bool HasDefaultValue => _defaultValue != null;
        
        public CodeMethodParameter(CodeType type, CodeNameVar name, string defaultValue = null)
        {
            _name = name;
            _type = type;
            _defaultValue = defaultValue;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            _type.Build(output);
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