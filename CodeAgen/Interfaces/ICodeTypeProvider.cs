using System;
using System.Collections.Generic;
using CodeAgen.Primitives;

namespace CodeAgen.Interfaces
{
    public interface ICodeTypeProvider
    {
        IReadOnlyCollection<CodeType> Types { get; }

        CodeType CreateType(string fullName);
        CodeType CreateType(Type type);

        bool TryGetType(string fullName, out CodeType type);
    }
}