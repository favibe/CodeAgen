using System.Collections.Generic;
using CodeAgen.Code.Basic;

namespace CodeAgen.Code.CodeTemplates.Interfaces
{
    public interface IInheritable
    {
        List<CodeType> InheritTypes { get; set; }
    }
}