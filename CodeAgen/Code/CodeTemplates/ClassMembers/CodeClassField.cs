using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassField : CodeTabbable, ICodeClassMember
    {
        public byte Order => 0;
        
        private readonly CodeType _type;
        private readonly CodeName _name;
        private readonly string _value;
        private readonly CodeAccessModifier _accessModifier;
        private readonly bool _isReadonly;

        public CodeClassField(CodeType type, CodeNameVar name, string value = null, CodeAccessModifier accessModifier = null, bool isReadonly = false)
        {
            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Private;
            }

            _type = type;
            _name = name;
            _value = value;
            _accessModifier = accessModifier;
            _isReadonly = isReadonly;
        }
        
        public override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            
            if (_isReadonly)
            {
                output.Write(CodeKeywords.Readonly);
                output.Write(CodeMarkups.Space);
            }
            
            output.Write(_type);
            output.Write(CodeMarkups.Space);

            var name = CodeNameVar.CreateFieldName(_name.Data, _accessModifier);
            
            name.OnBuild(output);

            if (!string.IsNullOrWhiteSpace(_value))
            {
                output.Write(CodeMarkups.Assignment);
                output.Write(_value);
            }

            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}