using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public abstract class CodeRaw : CodeUnit
    {
        public static readonly CodeRaw Empty = new CodeRawString(string.Empty);
    }

    public sealed class CodeRawChar : CodeRaw
    {
        private readonly char _data;

        public CodeRawChar(char data)
        {
            _data = data;
        }

        public override void OnBuild(ICodeOutput output)
        {
            output.Write(_data);
        }
    }

    public class CodeRawString : CodeRaw
    {
        private readonly string _data;

        public string Data => _data;
        
        public CodeRawString(string data)
        {
            _data = data;
        }
        
        public override void OnBuild(ICodeOutput output)
        {
            output.Write(_data);
        }
        
        public static implicit operator CodeRawString(string code)
        {
            return new CodeRawString(code);
        }
    }
}