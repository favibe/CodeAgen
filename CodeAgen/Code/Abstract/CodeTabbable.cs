namespace CodeAgen.Code.Abstract
{
    /// <summary>
    /// Code unit with tabulations
    /// </summary>
    public abstract class CodeTabbable : CodeUnit
    {
        /// <summary>
        /// Tabulation level
        /// </summary>
        public int Level { get; set; }

        protected override void PostBuild()
        {
            // Make sure tabulations clear after build
            Level = 0;
        }
    }
}