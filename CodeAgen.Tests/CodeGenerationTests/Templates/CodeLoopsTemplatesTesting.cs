using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Loops;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Templates
{
    public class CodeLoopsTemplatesTesting
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeLoopsTemplatesTesting()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        private void Build_WhileLoop()
        {
            var block = new CodeBracedBlock();
            var @while = new CodeWhileLoop("a > 0");

            block.AddUnit(@while);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\twhile(a > 0)\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_ForLoop()
        {
            var block = new CodeBracedBlock();
            var @for = new CodeForLoop("i = 0; i < 5; i++");

            block.AddUnit(@for);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\tfor(i = 0; i < 5; i++)\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_ForeachLoop()
        {
            var block = new CodeBracedBlock();
            var @for = new CodeForeachLoop("var a in b");

            block.AddUnit(@for);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\tforeach(var a in b)\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_DoWhileLoop()
        {
            var block = new CodeBracedBlock();
            var @for = new CodeDoWhileLoop("true");

            block.AddUnit(@for);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();
            
            const string expectedCode = "{\r\n\tdo\r\n\t{\r\n\t}\r\n\twhile(true);\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }

        [Fact]
        private void GenerateCode()
        {
            var @namespace = new CodeNamespace("ExampleNamespace.Subspace");
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);
            var @field = new CodeField("string", "name", accessModifier: CodeAccessModifier.Private, isReadonly:true);

            var constructor = CodeConstructor.CreateFor(@class, CodeAccessModifier.Public);
            
            @constructor.AddParameter(new CodeMethodParameter( "string", "name"));
            @constructor.AddUnit(new CodeLine("_name = name"));
            @constructor.AddUnit(new CodeLine());
            
            var @for = new CodeForLoop("var i = 0; i < name.Length; i++");
            @for.AddUnit(new CodeLine("Console.WriteLine(name[i])"));
            
            @constructor.AddUnit(@for);
            
            @class.AddUnit(field);
            @class.AddUnit(new CodeLine());
            @class.AddUnit(constructor);
            @namespace.AddUnit(@class);

            @namespace.Build(_codeOutput);
            
            var code = _codeOutput.ToString();
        }
    }
}