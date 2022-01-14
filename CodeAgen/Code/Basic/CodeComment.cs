using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public sealed class CodeComment : CodeRaw
    {
        private readonly string _content;
        
        public CodeComment(string content)
        {
            _content = content;
        }
        
        public override void OnBuild(ICodeOutput output)
        {
            output.Write(CodeMarkups.Comment);
            output.Write(CodeMarkups.Space);
            output.Write(_content);
        }
    }
}