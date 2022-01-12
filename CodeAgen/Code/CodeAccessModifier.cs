using CodeAgen.Code.Basic;

namespace CodeAgen.Code
{
    public class CodeAccessModifier
    {
        public static readonly CodeAccessModifier Private = new CodeAccessModifier("private");
        public static readonly CodeAccessModifier Protected = new CodeAccessModifier("protected");
        public static readonly CodeAccessModifier Public = new CodeAccessModifier("public");

        private readonly string _content;
        
        private CodeAccessModifier(string content)
        {
            _content = content;
        }

        public override string ToString()
        {
            return _content;
        }
        
        public static implicit operator string(CodeAccessModifier modifier)
        {
            return modifier._content;
        }
    }
}