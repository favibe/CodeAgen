using CodeAgen.Code.Abstract;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public abstract class CodeClassMember : CodeTabbable
    {
        public abstract byte Order { get; }
    }
}