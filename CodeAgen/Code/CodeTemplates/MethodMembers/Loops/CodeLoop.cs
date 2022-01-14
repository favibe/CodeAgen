using CodeAgen.Code.Basic;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public abstract class CodeLoop : CodeBracedBlock
    {
        public static readonly CodeLine Break = new CodeLine("break");
        public static readonly CodeLine Continue = new CodeLine("continue");
    }
}