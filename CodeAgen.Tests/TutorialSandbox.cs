using System;
using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching;
using CodeAgen.Code.CodeTemplates.MethodMembers.Loops;
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

            code.Space();

            var constructor = CodeConstructor.CreateFor(code, CodeAccessModifier.Internal)
                .AddParameter(new CodeMethodParameter("float", "index"))
                .AddParameter(new CodeMethodParameter("string", "name"));

            constructor
                .AddUnit(new CodeLine("_index = index;"))
                .AddUnit(new CodeLine("_name = name;"));
            
            code.Constructor(constructor);

            var methodA = new CodeMethod("MethodA", "float", CodeAccessModifier.Internal);
            var methodB = new CodeMethod("MethodB", CodeType.Void, CodeAccessModifier.Internal);

            code.Space().Method(methodA)
                .Space().Method(methodB);

            var commonCode = new CodeFragment();

            commonCode
                .AddUnit(new CodeLine("var i = 0;"))
                .AddUnit(CodeLine.Empty)
                .AddUnit(new CodeLoopWhile("i < 10")
                    .AddUnit(new CodeLine("Console.WriteLine(i);"))
                    .AddUnit(new CodeLine("i++;"))
                );

            methodA
                .AddUnit(new CodeIfElse("_index == 0", 
                    new CodeFragment()
                        .AddUnit(commonCode)
                        .AddUnit(CodeLine.Empty)
                        .AddUnit(CodeMethod.Return("3.5f * 2"))
                    ))
                .AddUnit(CodeLine.Empty)
                .AddUnit(CodeMethod.Return("12f"));
            
            methodB.AddUnit(commonCode);

            code.Build(output);
            
            var result = output.ToString();
        }
    }
}