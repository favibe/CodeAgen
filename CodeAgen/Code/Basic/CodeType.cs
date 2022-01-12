using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeAgen.Code.Basic
{
    public class CodeType
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9]");
        private static readonly Dictionary<string, CodeType> Types = new Dictionary<string, CodeType>();

        public static CodeType Get(string name)
        {
            if (Types.ContainsKey(name))
            {
                return Types[name];
            }

            var type = new CodeType(name);
            Types.Add(name, type);
            return type;
        }

        private readonly string _name;
        
        private CodeType(string name)
        {
            _name = name;
        }

        public static bool IsValidGenericName(string name)
        {
            return !char.IsNumber(name[0]) && !SpecialCharactersRegex.IsMatch(name);
        }
        
        public static implicit operator string(CodeType type)
        {
            return type._name;
        }
    }
}