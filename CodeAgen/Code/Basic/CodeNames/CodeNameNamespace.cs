using System.Text.RegularExpressions;
using CodeAgen.Exceptions;

namespace CodeAgen.Code.Basic.CodeNames
{
    /// <summary>
    /// Code name for namespace
    /// </summary>
    public class CodeNameNamespace : CodeName
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9_.]");
        
        public CodeNameNamespace(string data) : base(data)
        {
            if (!IsValid(data))
            {
                throw new CodeNamingException($"Invalid namespace name: {data}");
            }
        }
        
        public static implicit operator string(CodeNameNamespace name)
        {
            return name.Data;
        }
        
        public static implicit operator CodeNameNamespace(string name)
        {
            return new CodeNameNamespace(name);
        }

        private static bool IsValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !char.IsNumber(name[0]) && !SpecialCharactersRegex.IsMatch(name);
        }
    }
}