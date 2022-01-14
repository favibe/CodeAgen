using CodeAgen.Code.Basic;

namespace CodeAgen.Code.Abstract
{
    /// <summary>
    /// Code unit with tabulations
    /// </summary>
    public abstract class CodeTabbable : CodeUnit
    {
        public CodeTabbable Parent { get; set; }
        
        /// <summary>
        /// Tabulation level
        /// </summary>
        public int Level { get; set; }

        protected override void PreBuild()
        {
            if (Parent != null)
            {
                Level = Parent.GetNextTabLevel();
            }
        }

        protected override void PostBuild()
        {
            // Make sure tabulations clear after build
            Level = 0;
        }

        protected virtual int GetNextTabLevel()
        {
            return Level + 1;
        }
    }
}