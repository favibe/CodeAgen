using CodeAgen.Code.Basic;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests
{
    public class CodeLineTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeLineTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Build_OneUnitNotTabbed()
        {
            const string source = "var a = 5"; 
            
            var code = new CodeLine();
            code.AddUnit(new CodeRawString(source));
            code.Build(_codeOutput);
            
            Assert.Equal((source + ";"), _codeOutput.ToString());
            _codeOutput.Clear();
        }
        
        [Fact]
        public void Build_OneUnitTabbed()
        {
            const string source = "var a = 5"; 
            
            var code = new CodeLine();
            code.AddUnit(new CodeRawString(source));
            code.Level = 2;
            code.Build(_codeOutput);
            
            Assert.Equal(("\t\t" + source + ";"), _codeOutput.ToString());
            _codeOutput.Clear();
        }
        
        [Theory]
        [InlineData("\t\tvar a = abc;", new string[] {"var ", "a", " = ", "abc"})]
        [InlineData("\t\tConsole.Write(\"HelloWorld!\");", new string[] {"Console", ".Write(", "\"HelloWorld!\"", ")"})]
        public void Build_MultipleUnitsTabbed(string expected, string[] args)
        {
            var code = new CodeLine();

            foreach (var arg in args)
            {
                code.AddUnit(new CodeRawString(arg));
            }
            
            code.Level = 2;
            code.Build(_codeOutput);
            
            Assert.Equal(expected, _codeOutput.ToString());
            _codeOutput.Clear();
        }
    }
}