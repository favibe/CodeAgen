using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.Extensions
{
    public static class InheritableExtension
    {
        public static bool IsInherits(this IInheritable inheritable)
        {
            return inheritable.InheritTypes != null && inheritable.InheritTypes.Count > 0;
        }
       
        public static IInheritable InheritFrom(this IInheritable inheritable, CodeType type)
        {
            if (!IsInherits(inheritable))
            {
                inheritable.InheritTypes = new List<CodeType>();
            }
            
            inheritable.InheritTypes.Add(type);

            return inheritable;
        }
        
        public static void WriteInheritance(this IInheritable inheritable, ICodeOutput output)
        {
            output.Write(CodeMarkups.Space);
            output.Write(CodeMarkups.Colon);
            output.Write(CodeMarkups.Space);
            output.Write(inheritable.InheritTypes[0]);

            for (int i = 1; i < inheritable.InheritTypes.Count; i++)
            {
                output.Write(CodeMarkups.Comma);
                output.Write(CodeMarkups.Space);
                output.Write(inheritable.InheritTypes[i]);
            }
        }
    }
}