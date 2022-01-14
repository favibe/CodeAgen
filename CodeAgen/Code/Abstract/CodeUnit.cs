using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Abstract
{
    /// <summary>
    /// Basic code unit
    /// </summary>
    public abstract class CodeUnit
    {
        /// <summary>
        /// Recursively build code to output 
        /// </summary>
        /// <param name="output">Output</param>
        public void Build(ICodeOutput output)
        {
            PreBuild();
            OnBuild(output);
            PostBuild();
        }

        /// <summary>
        /// Pre-build object handling
        /// </summary>
        protected virtual void PreBuild()
        {
            
        }
        
        /// <summary>
        /// Post-build object handling
        /// </summary>
        protected virtual void PostBuild()
        {
            
        }

        /// <summary>
        /// Object building to output
        /// </summary>
        /// <param name="output">Output</param>
        protected abstract void OnBuild(ICodeOutput output);
        
        public static implicit operator CodeUnit(string @string)
        {
            return new CodeRawString(@string);
        }
        
        public static implicit operator CodeUnit(char @char)
        {
            return new CodeRawChar(@char);
        }
    }
}