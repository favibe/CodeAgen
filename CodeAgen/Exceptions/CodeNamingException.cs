using System;

namespace CodeAgen.Exceptions
{
    public class CodeNamingException : Exception
    {
        public CodeNamingException()
        {
            
        }
        
        public CodeNamingException(string message) : base(message)
        {
            
        }
    }
}