using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests
{
    public class TutorialSandbox
    {
        [Fact]
        private void GenerateCode()
        {
            ICodeOutput output = new StandardCodeOutput();
            
            var code = new CodeClass("ExampleClass", CodeAccessModifier.Internal, new CodeComment("Мой первый класс!"));
            
            code.Field(new CodeField("float", "_index", "5f", CodeAccessModifier.Private, true))
                .Field(new CodeField("string", "_name", null, CodeAccessModifier.Private, true));

            code.Constructor(CodeConstructor.CreateFor(code, CodeAccessModifier.Public)
                .AddParameter(new CodeMethodParameter("float", "_index"))
                .AddParameter(new CodeMethodParameter("string", "_name"))
            );
            
            code.Build(output);
            
            var result = output.ToString();
        }
    }
}