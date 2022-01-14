using System.Text.RegularExpressions;
using CodeAgen.Exceptions;

namespace CodeAgen.Code.Basic.CodeNames
{
    /// <summary>
    /// Code name for variable
    /// </summary>
    public class CodeNameVar : CodeName
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9_@]");

        public CodeNameVar(string data) : base(data)
        {
            if (!IsValid(data))
            {
                throw new CodeNamingException($"Invalid variable name: {data}");
            }
        }
        
        public static implicit operator string(CodeNameVar name)
        {
            return name.Data;
        }
        
        public static implicit operator CodeNameVar(string name)
        {
            return new CodeNameVar(name);
        }

        private static bool IsValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !char.IsNumber(name[0]) && !SpecialCharactersRegex.IsMatch(name);
        }
    }
}