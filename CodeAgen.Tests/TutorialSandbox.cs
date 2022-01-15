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
            //
            //code.Field(new CodeField("float", "_index", accessModifier: CodeAccessModifier.Private,
            //        isReadonly: true))
            //    .Field(new CodeField("string", "_name", accessModifier: CodeAccessModifier.Private,
            //        isReadonly: true));
            //

            code.AddUnit(new CodeLine("private readonly float _index"));
            code.AddUnit(new CodeLine("private readonly float _name"));
            
            code.Build(output);
            
            var result = output.ToString();
        }
    }
}