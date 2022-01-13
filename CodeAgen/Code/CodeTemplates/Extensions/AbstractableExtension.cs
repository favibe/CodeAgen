using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.Extensions
{
    public static class AbstractableExtension
    {
        public static IAbstractable SetAbstract(this IAbstractable abstractable, bool isAbstract)
        {
            abstractable.IsAbstract = isAbstract;
            return abstractable;
        }
        
        public static IAbstractable WriteAbstract(this IAbstractable abstractable, ICodeOutput output)
        {
            output.Write(CodeKeywords.Abstract);
            output.Write(CodeMarkups.Space);
            return abstractable;
        }
    }
}