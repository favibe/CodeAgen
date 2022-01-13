using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassEvent : CodeTabbable, ICodeClassMember
    {
        private readonly string _name;
        private readonly CodeType _type;
        private readonly CodeAccessModifier _accessModifier;

        public byte Order => 2;

        public CodeClassEvent(string name, CodeType type, CodeAccessModifier accessModifier = null)
        {
            _name = name;
            _type = type;

            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Public;
            }
            
            _accessModifier = accessModifier;
        }
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            output.Write(CodeKeywords.Event);
            output.Write(CodeMarkups.Space);
            output.Write(_type);
            output.Write(CodeMarkups.Space);
            output.Write(_name);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}