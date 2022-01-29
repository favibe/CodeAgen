using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities
{
    public sealed class CodeFragment : ICode
    {
        public readonly string Code;
        
        public CodeFragment(string code)
        {
            Code = code;
        }
        
        public void PreBuild() {}

        public void OnBuild(ICodeBuilder builder)
        {
            builder.Append(Code);
        }

        public void PostBuild() {}
    }
}