using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
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
            code.Build(output);
            
            var result = output.ToString();
        }
    }
}