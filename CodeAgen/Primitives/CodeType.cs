namespace CodeAgen.Primitives
{
    /// <summary>
    /// Base struct to store code type
    /// </summary>
    public readonly struct CodeType
    {
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
        
        public CodeType(string fullName)
        {
            var lastDotIndex = fullName.LastIndexOf('.');

            FullName = fullName;
            Namespace = fullName.Substring(0, lastDotIndex);
            ShortName = fullName.Substring(lastDotIndex+1, fullName.Length - lastDotIndex-1);
        }
    }
}