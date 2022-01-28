using System;
using System.Collections.Generic;
using System.Linq;
using CodeAgen.Exceptions;
using CodeAgen.Interfaces;
using CodeAgen.Primitives;

namespace CodeAgen.Concrete
{
    public class CodeTypeProvider : ICodeTypeProvider
    {
        private static readonly (string fullName, string shortName)[] BaseTypes =
        {
            ("System.Boolean", "bool"),
            ("System.SByte", "sbyte"),
            ("System.Byte", "byte"),
            ("System.Int16", "short"),
            ("System.UInt16", "ushort"),
            ("System.Int32", "int"),
            ("System.UInt32", "uint"),
            ("System.Int64", "long"),
            ("System.UInt64", "ulong"),
            ("System.Single", "float"),
            ("System.Double", "double"),
            ("System.Decimal", "decimal"),
            ("System.Char", "char"),
            ("System.Object", "object"),
            ("dynamic", "dynamic"),
            ("System.String", "string")
        };
        
        private readonly List<CodeType> _types = new List<CodeType>();
        
        public IReadOnlyCollection<CodeType> Types => _types;

        public CodeTypeProvider()
        {
            foreach (var (fullName, shortName) in BaseTypes)
            {
                var type = new CodeType(fullName, shortName);
                _types.Add(type);
            }
        }
        
        public CodeType CreateType(string fullName)
        {
            if (_types.Any(x => x.FullName == fullName))
            {
                throw new CodeTypeException($"Type with full name {fullName} already exist");
            }

            var type = new CodeType(fullName);
            _types.Add(type);

            return type;
        }

        public CodeType CreateType(Type type)
        {
            return CreateType(type.FullName);
        }

        public bool TryGetType(string fullName, out CodeType type)
        {
            var count = _types.Count;
            
            for (var index = 0; index < count; index++)
            {
                if (_types[index].FullName != fullName)
                {
                    continue;
                }
                
                type = _types[index];
                return true;
            }

            type = default;
            return false;
        }
    }
}