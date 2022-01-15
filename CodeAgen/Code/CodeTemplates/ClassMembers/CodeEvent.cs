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
        private readonly CodeAccessModifier _access;
        
        public byte Order => 2;

        public CodeEvent(CodeNameVar name, CodeType type, CodeAccessModifier access = null)
        {
            _name = name;
            _type = type;

            if (access == null)
            {
                access = CodeAccessModifier.Public;
            }
            
            _access = access;
        }
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_access);
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