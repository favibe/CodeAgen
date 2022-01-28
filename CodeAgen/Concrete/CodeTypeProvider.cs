using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeAgen.Exceptions;
using CodeAgen.Interfaces;
using CodeAgen.Primitives;

namespace CodeAgen.Concrete
{
    public class CodeTypeProvider : ICodeTypeProvider
    {
        private static readonly Regex SpecialCharactersRegex = new Regex("^(?:(?:[A-z][A-z0-9 ]*\\.?)*)[^\\.]$");
        
        private readonly List<CodeType> _types = new List<CodeType>();
        
        public IReadOnlyCollection<CodeType> Types => _types;
        
        public CodeType CreateType(string fullName)
        {
            if (_types.Any(x => x.FullName == fullName))
            {
                throw new CodeTypeException($"Type with full name {fullName} already exist");
            }
            
            if (!SpecialCharactersRegex.IsMatch(fullName))
            {
                throw new CodeTypeException($"Bad name format for type: {fullName}");
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