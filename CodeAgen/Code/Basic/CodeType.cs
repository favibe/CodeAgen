using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeAgen.Exceptions;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code type unit
    /// </summary>
    public class CodeType : CodeRawString
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("[^A-Za-z0-9_.\\[//]<>]");
        private static readonly Dictionary<string, CodeType> Types = new Dictionary<string, CodeType>();
        
        public static CodeType Void => Types["Void"];
        
        static CodeType()
        {
            Types.Add("Void", new CodeType("void"));
        }
        
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
        
        private CodeType(string name) : base(name)
        {
            if (!IsValid(name))
            {
                throw new CodeNamingException($"Invalid type name: {name}");
            }
            
            _name = name;
        }

        private static bool IsValid(string name)
        {
            return !(string.IsNullOrWhiteSpace(name) || char.IsNumber(name[0]) || SpecialCharactersRegex.IsMatch(name));
        }
        
        //public static implicit operator string(CodeType type)
        //{
        //    return type._name;
        //}
        
        public static implicit operator CodeType(string type)
        {
            return Get(type);
        }
    }
}