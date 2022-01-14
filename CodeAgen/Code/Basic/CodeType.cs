using System.Collections.Generic;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code type unit
    /// </summary>
    public class CodeType
    {
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
        
        private CodeType(string name)
        {
            _name = name;
        }

        public static implicit operator string(CodeType type)
        {
            return type._name;
        }
        
        public static implicit operator CodeType(string type)
        {
            return Get(type);
        }
    }
}