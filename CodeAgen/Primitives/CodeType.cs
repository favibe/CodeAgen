using System.Text.RegularExpressions;
using CodeAgen.Exceptions;

namespace CodeAgen.Primitives
{
    /// <summary>
    /// Base struct to store code type
    /// </summary>
    public readonly struct CodeType
    {
        private static readonly Regex NameFormat = new Regex("^(?:(?:[A-z][A-z0-9 ]*\\.?)*)[^\\.]$");
        
        /// <summary>
        /// Full type name, including namespace
        /// </summary>
        public readonly string FullName;
        /// <summary>
        /// Short type name
        /// </summary>
        public readonly string ShortName;
        /// <summary>
        /// Only type namespace
        /// </summary>
        public readonly string Namespace;
        public bool HasNamespace => Namespace != null && !string.IsNullOrEmpty(Namespace);
        
        public CodeType(string fullName)
        {
            if (!NameFormat.IsMatch(fullName))
            {
                throw new CodeTypeException($"Bad name format for type: {fullName}");
            }
            
            var lastDotIndex = fullName.LastIndexOf('.');

            FullName = fullName;
            Namespace = fullName.Substring(0, lastDotIndex);
            ShortName = fullName.Substring(lastDotIndex+1, fullName.Length - lastDotIndex-1);
        }
        
        public CodeType(string fullName, string shortName)
        {
            if (!NameFormat.IsMatch(fullName) || !NameFormat.IsMatch(shortName))
            {
                throw new CodeTypeException($"Bad name format for type: {fullName}");
            }
            
            var lastDotIndex = fullName.LastIndexOf('.');

            FullName = fullName;
            
            Namespace = lastDotIndex != -1 
                ? fullName.Substring(0, lastDotIndex) 
                : null;
                
            ShortName = shortName;
        }
    }
}