using System;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Loops
{
    public abstract class CodeLoop : CodeBracedBlock
    {
        public static CodeLine Break => new CodeLine("break");
        public static CodeLine Continue => new CodeLine("continue");
        public static CodeLine YieldBreak => new CodeLine("yield break");
        public static CodeLine Yield(CodeUnit code)
        {
            var yield = new CodeLine();
            yield.AddUnit($"{CodeKeywords.Yield} {CodeKeywords.Return} ");
            yield.AddUnit(code);

            return yield;
        }
    }
}