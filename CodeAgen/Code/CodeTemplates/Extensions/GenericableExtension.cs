using System.Collections.Generic;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Code.Utils;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.Extensions
{
    public static class GenericableExtension
    {
        public static bool IsGeneric(this IGenericable genericable)
        {
            return genericable.GenericArguments != null && genericable.GenericArguments.Count > 0;
        }
        
        public static bool HasRestrictions(this IGenericable genericable)
        {
            return genericable.GenericRestrictions != null && genericable.GenericRestrictions.Count > 0;
        }
        
        public static IGenericable AddGenericArgument(this IGenericable genericable, string name, string restriction = null)
        {
            if (!CodeName.IsValidTypeName(name))
            {
                throw new CodeBuildException($"Invalid generic argument name: {name}");
            }
            
            if (!IsGeneric(genericable))
            {
                genericable.GenericArguments = new List<string>();
            }

            if (restriction != null)
            {
                if (!HasRestrictions(genericable))
                {
                    genericable.GenericRestrictions = new List<string>();
                }
                
                genericable.GenericRestrictions.Add($"{name}:{restriction}");
            }

            genericable.GenericArguments.Add(name);

            return genericable;
        }
        
        public static void WriteGeneric(this IGenericable genericable, ICodeOutput output)
        {
            output.Write(CodeMarkups.OpenAngleBracket);
            output.Write(genericable.GenericArguments[0]);

            for (var i = 1; i < genericable.GenericArguments.Count; i++)
            {
                output.Write(CodeMarkups.Comma);
                output.Write(genericable.GenericArguments[i]);
            }

            output.Write(CodeMarkups.CloseAngleBracket);
        }
        
        public static void WriteRestrictions(this IGenericable genericable, ICodeOutput output)
        {
            output.Write(CodeMarkups.Space);
            output.Write(CodeKeywords.Where);
            output.Write(CodeMarkups.Space);
            output.Write(genericable.GenericRestrictions[0]);

            for (var i = 1; i < genericable.GenericRestrictions.Count; i++)
            {
                output.Write(CodeMarkups.Space);
                output.Write(CodeKeywords.Where);
                output.Write(CodeMarkups.Space);
                output.Write(genericable.GenericRestrictions[i]);
            }
        }
    }
}