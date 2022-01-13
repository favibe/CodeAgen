using System.Collections.Generic;

namespace CodeAgen.Code.CodeTemplates.Interfaces
{
    public interface IGenericable
    {
        List<string> GenericArguments { get; set; } 
        List<string> GenericRestrictions { get; set; }
    }
}