using System.Collections.Generic;
using CodeAgen.Code.Basic;

namespace CodeAgen.Code.CodeTemplates.Interfaces
{
    public interface IGenericable
    {
        List<CodeName> GenericArguments { get; set; } 
        List<string> GenericRestrictions { get; set; }
    }
}