using CodeAgen.Code.Basic;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests
{
    public class CodeFragmentTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeFragmentTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Build_Empty()
        {
            var block = new CodeFragment();
            block.Build(_codeOutput);
            
            Assert.Equal(string.Empty, _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_EmptyWithTab()
        {
            var block = new CodeFragment
            {
                Level = 1
            };
            
            block.Build(_codeOutput);
            
            Assert.Equal("\t", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_WithLinesWithTab()
        {
            var block = new CodeFragment
            {
                Level = 1
            };

            var codeLine1 = new CodeLine();
            codeLine1.AddUnit(new CodeRawString("abc"));
            
            var codeLine2 = new CodeLine();
            codeLine2.AddUnit(new CodeRawString("def"));

            block.AddUnit(codeLine1);
            block.AddUnit(codeLine2);
            
            block.Build(_codeOutput);
            
            Assert.Equal("\tabc;\r\n\tdef;\r\n\t", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_WithRawCodeWithTab()
        {
            var block = new CodeFragment
            {
                Level = 1
            };

            var raw1 = new CodeRawString("some code string 1");
            var raw2 = new CodeRawString("some code string 2");
            
            block.AddUnit(raw1);
            block.AddUnit(raw2);
            
            block.Build(_codeOutput);
            
            Assert.Equal("\tsome code string 1some code string 2", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_WithRawCodeWithLinesWithTab()
        {
            var block = new CodeFragment
            {
                Level = 1
            };

            var raw1 = new CodeRawString("some code string 1");
            var raw2 = new CodeRawString("some code string 2");
            var line = new CodeLine();
            line.AddUnit(new CodeRawString("var a = 5"));
            
            block.AddUnit(raw1);
            block.AddUnit(raw2);
            block.AddUnit(line);
            
            block.Build(_codeOutput);
            
            Assert.Equal("\tsome code string 1some code string 2var a = 5;\r\n\t", _codeOutput.ToString());
        }
    }
}