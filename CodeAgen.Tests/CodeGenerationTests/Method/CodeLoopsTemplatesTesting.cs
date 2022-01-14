using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers.Loops;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Method
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
            var @while = new CodeLoopWhile("a > 0");

            block.AddUnit(@while);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\twhile(a > 0)\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_BreakLoop()
        {
            var block = new CodeBracedBlock();
            var @while = new CodeLoopWhile("a > 0");

            block.AddUnit(@while);
            @while.AddUnit(CodeLoop.Break);
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\twhile(a > 0)\r\n\t{\r\n\t\tbreak;\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_YieldReturn()
        {
            var yield = CodeLoop.YieldBreak;
            
            yield.Build(_codeOutput);
            var actualCode = _codeOutput.ToString();

            const string expectedCode = "yield break;\r\n";
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_YieldBreak()
        {
            var yield = CodeLoop.YieldReturn(new CodeRawString("new Target()"));
            
            yield.Build(_codeOutput);
            var actualCode = _codeOutput.ToString();

            const string expectedCode = "yield return new Target();\r\n";
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_ContinueLoop()
        {
            var block = new CodeBracedBlock();
            var @while = new CodeLoopWhile("a > 0");

            block.AddUnit(@while);
            @while.AddUnit(CodeLoop.Continue);
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();

            const string expectedCode = "{\r\n\twhile(a > 0)\r\n\t{\r\n\t\tcontinue;\r\n\t}\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
        
        [Fact]
        private void Build_ForLoop()
        {
            var block = new CodeBracedBlock();
            var @for = new CodeLoopFor("i = 0; i < 5; i++");

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
            var @for = new CodeLoopForeach("var a in b");

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
            var @for = new CodeLoopDoWhile("true");

            block.AddUnit(@for);
            
            block.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();
            
            const string expectedCode = "{\r\n\tdo\r\n\t{\r\n\t}\r\n\twhile(true);\r\n}\r\n";
            
            Assert.Equal(expectedCode, actualCode);
        }
    }
}