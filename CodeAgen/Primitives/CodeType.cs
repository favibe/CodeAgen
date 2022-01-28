namespace CodeAgen.Primitives
{
    public readonly struct CodeType
    {
        public readonly string FullName;
        public readonly string ShortName;
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