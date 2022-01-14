using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    /// <summary>
    /// Code unit for class event
    /// </summary>
    public class CodeEvent : CodeTabbable
    {
        private readonly CodeName _name;
        private readonly CodeType _type;
        private readonly CodeAccessModifier _accessModifier;
        
        public byte Order => 2;

        public CodeEvent(CodeNameVar name, CodeType type, CodeAccessModifier accessModifier = null)
        {
            _name = name;
            _type = type;

            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Public;
            }
            
            _accessModifier = accessModifier;
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            output.Write(CodeKeywords.Event);
            output.Write(CodeMarkups.Space);
            _type.Build(output);
            output.Write(CodeMarkups.Space);
            _name.Build(output);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}