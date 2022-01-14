namespace CodeAgen.Code
{
    /// <summary>
    /// Code access modifier
    /// </summary>
    public class CodeAccessModifier
    {
        public static readonly CodeAccessModifier Private = new CodeAccessModifier("private");
        public static readonly CodeAccessModifier Protected = new CodeAccessModifier("protected");
        public static readonly CodeAccessModifier Public = new CodeAccessModifier("public");
        public static readonly CodeAccessModifier Internal = new CodeAccessModifier("internal");

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