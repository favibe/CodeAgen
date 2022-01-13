using System.Text.RegularExpressions;

namespace CodeAgen.Code.Utils
{
    public static class CodeName
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9]");

        public static bool IsValidMethodName(string name) => IsValidName(name);
        public static bool IsValidTypeName(string name) => IsValidName(name);
        public static bool IsValidClassName(string name) => IsValidName(name);
        
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !char.IsNumber(name[0]) && !SpecialCharactersRegex.IsMatch(name);
        }
    }
}