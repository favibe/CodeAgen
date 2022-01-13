using System.Text.RegularExpressions;
using CodeAgen.Code.Basic;
using CodeAgen.Exceptions;

namespace CodeAgen.Code.CodeTemplates
{
    public class CodeName : CodeRawString
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9_@]");

        public static bool IsValidMethodName(string name) => IsValidName(name);
        public static bool IsValidTypeName(string name) => IsValidName(name);
        public static bool IsValidClassName(string name) => IsValidName(name);
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !char.IsNumber(name[0]) && !SpecialCharactersRegex.IsMatch(name);
        }

        public static CodeName GetFieldName(string name, CodeAccessModifier accessModifier)
        {
            if (accessModifier == CodeAccessModifier.Private)
            {
                return $"{CodeMarkups.Underscore}{char.ToLower(name[0])}{name.Substring(1)}";
            }
            
            return $"{char.ToUpper(name[0])}{name.Substring(1)}";
        }

        public static implicit operator CodeName(string name)
        {
            return new CodeName(name);
        }
        
        public CodeName(string data) : base(data)
        {
            if (!IsValidName(data))
            {
                throw new CodeBuildException();
            }
        }
    }
}