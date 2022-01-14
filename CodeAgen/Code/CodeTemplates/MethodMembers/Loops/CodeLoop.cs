using CodeAgen.Code.Basic;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public abstract class CodeLoop : CodeBracedBlock
    {
        public static CodeLine Break => new CodeLine("break");
        public static CodeLine Continue => new CodeLine("continue");
    }
}