using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Basic code unit
    /// </summary>
    public abstract class CodeRaw : CodeUnit
    {
        public static readonly CodeRaw Empty = new CodeRawString(string.Empty);
    }

    /// <summary>
    /// Char code unit
    /// </summary>
    public sealed class CodeRawChar : CodeRaw
    {
        private readonly char _data;

        public CodeRawChar(char data)
        {
            _data = data;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.Write(_data);
        }
    }

    /// <summary>
    /// String code unit
    /// </summary>
    public class CodeRawString : CodeRaw
    {
        private readonly string _data;

        public string Data => _data;
        
        public CodeRawString(string data)
        {
            _data = data;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.Write(_data);
        }
        
        public static implicit operator CodeRawString(string code)
        {
            return new CodeRawString(code);
        }
    }
}