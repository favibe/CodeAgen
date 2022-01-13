using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassField : CodeTabbable, ICodeClassMember
    {
        public byte Order => 0;
        
        private readonly CodeType _type;
        private readonly string _name;
        private readonly string _value;
        private readonly CodeAccessModifier _accessModifier;

        public CodeClassField(CodeType type, string name, string value = null, CodeAccessModifier accessModifier = null)
        {
            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Private;
            }
            
            _type = type;
            _name = name;
            _value = value;
            _accessModifier = accessModifier;
        }
        
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            output.Write(_type);
            output.Write(CodeMarkups.Space);

            var name = _accessModifier == CodeAccessModifier.Private ? GetPrivateName(_name) : GetPublicName(_name);

            output.Write(name);

            if (!string.IsNullOrWhiteSpace(_value))
            {
                output.Write(CodeMarkups.Assignment);
                output.Write(_value);
            }

            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }

        private string GetPrivateName(string name)
        {
            return $"{CodeMarkups.Underscore}{char.ToLower(name[0])}{name.Substring(1)}";
        }
        
        private string GetPublicName(string name)
        {
            return $"{char.ToUpper(name[0])}{name.Substring(1)}";
        }
    }
}