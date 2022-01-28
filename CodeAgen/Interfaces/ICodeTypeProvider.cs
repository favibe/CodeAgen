using System;
using System.Collections.Generic;
using CodeAgen.Primitives;

namespace CodeAgen.Interfaces
{
    /// <summary>
    /// Type to store and process code types
    /// </summary>
    public interface ICodeTypeProvider
    {
        /// <summary>
        /// Collection of all code types
        /// </summary>
        IReadOnlyCollection<CodeType> Types { get; }
        /// <summary>
        /// Create code type from name
        /// </summary>
        /// <param name="fullName">Full type name, including namespace</param>
        /// <returns>Code type</returns>
        CodeType CreateType(string fullName);
        /// <summary>
        /// Create code type from existing type
        /// </summary>
        /// <param name="type">TypeInfo</param>
        /// <returns>Code type</returns>
        CodeType CreateType(Type type);
        /// <summary>
        /// Try get CodeType by name, if it's exist
        /// </summary>
        /// <param name="fullName">CodeType full name</param>
        /// <param name="type">CodeType</param>
        /// <returns>Is code type exist?</returns>
        bool TryGetType(string fullName, out CodeType type);
    }
}