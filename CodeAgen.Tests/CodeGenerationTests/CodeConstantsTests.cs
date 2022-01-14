using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests
{
    public class CodeConstantsTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeConstantsTests()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_Simple()
        {
            var constant = new CodeConst("float", "name", "5f");
            constant.Build(_codeOutput);

            const string expected = "public const float name = 5f;\r\n";
            
            Assert.Equal(expected, _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_Private()
        {
            var constant = new CodeConst("float", "name", "5f", CodeAccessModifier.Private);
            constant.Build(_codeOutput);

            const string expected = "private const float name = 5f;\r\n";
            
            Assert.Equal(expected, _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_MultipleInTab()
        {
            var codeFrag = new CodeFragment()
            {
                Level = 1
            };
            
            var constant = new CodeConst("float", "name1", "5f", CodeAccessModifier.Private);
            codeFrag.AddUnit(constant);
            constant = new CodeConst("float", "name2", "2.5f", CodeAccessModifier.Private);
            codeFrag.AddUnit(constant);
            
            codeFrag.Build(_codeOutput);

            const string expected = "\tprivate const float name1 = 5f;\r\n\tprivate const float name2 = 2.5f;\r\n";
            
            Assert.Equal(expected, _codeOutput.ToString());
        }
    }
}