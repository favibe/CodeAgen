using CodeAgen.Code.Basic;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests
{
    public class CodeBracedBlockTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeBracedBlockTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Build_Empty0Level()
        {
            var block = new CodeBracedBlock();
            block.Build(_codeOutput);
            
            Assert.Equal("{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_EmptyBlock1Level()
        {
            var block = new CodeBracedBlock();
            block.Level = 1;
            block.Build(_codeOutput);
            
            Assert.Equal("\t{\r\n\t}\r\n", _codeOutput.ToString());
        }

        [Fact]
        public void Build_WithCode0Level()
        {
            var block = new CodeBracedBlock();

            var line = new CodeLine();
            line.AddUnit(new CodeRawString("var abc = 5"));
            block.AddUnit(line);
            line = new CodeLine();
            line.AddUnit(new CodeRawString("abc.Use()"));
            block.AddUnit(line);
            
            block.Build(_codeOutput);
            block.AddUnit(line);
            
            Assert.Equal("{\r\n\tvar abc = 5;\r\n\tabc.Use();\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_WithCode1Level()
        {
            var block = new CodeBracedBlock();
            block.Level = 1;

            var line = new CodeLine();
            line.AddUnit(new CodeRawString("var abc = 5"));
            block.AddUnit(line);
            line = new CodeLine();
            line.AddUnit(new CodeRawString("abc.Use()"));
            block.AddUnit(line);
            
            block.Build(_codeOutput);
            block.AddUnit(line);
            
            Assert.Equal("\t{\r\n\t\tvar abc = 5;\r\n\t\tabc.Use();\r\n\t}\r\n", _codeOutput.ToString());
        }
    }
}