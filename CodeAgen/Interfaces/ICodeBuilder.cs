namespace CodeAgen.Interfaces
{
    /// <summary>
    /// Entity for code building
    /// </summary>
    public interface ICodeBuilder
    {
        /// <summary>
        /// Append char
        /// </summary>
        /// <param name="data">data</param>
        /// <returns></returns>
        ICodeBuilder Append(char data);
        /// <summary>
        /// Append string
        /// </summary>
        /// <param name="data">data</param>
        /// <returns></returns>
        ICodeBuilder Append(string data);
        /// <summary>
        /// Move caret to next line
        /// </summary>
        /// <returns></returns>
        ICodeBuilder NextLine();
        /// <summary>
        /// Set tabulation level
        /// </summary>
        /// <param name="level">Tabulation level</param>
        /// <returns></returns>
        ICodeBuilder SetTab(int level);
        /// <summary>
        /// Clear code builder
        /// </summary>
        /// <returns></returns>
        ICodeBuilder Clear();
    }
}