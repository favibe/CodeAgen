using System;

namespace CodeAgen.Exceptions
{
    public class CodeBuildException : Exception
    {
        public CodeBuildException() : base()
        {
            
        }
        
        public CodeBuildException(string message) : base(message)
        {
            
        }
    }
}