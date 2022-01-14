using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Method
{
    public class CodeConditionChainTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeConditionChainTests()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_SimpleCondition()
        {
            var condition = new CodeConditionChain("condition != null");
            condition.Build(_codeOutput);

            const string expectedCode = "condition != null";
            
            Assert.Equal(expectedCode, _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_MultipleCondition()
        {
            var condition = new CodeConditionChain("condition1 != null");
            condition.And("condition2 != null");
            condition.Build(_codeOutput);

            const string expectedCode = "condition1 != null && condition2 != null";
            
            Assert.Equal(expectedCode, _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_MultipleLevelCondition()
        {
            var condition = new CodeConditionChain("condition1 != null");
            condition.And("condition2 != null");

            condition = new CodeConditionChain(new CodeInBrackets(condition));
            condition.Or("condition3 == null");
            
            condition.Build(_codeOutput);

            const string expectedCode = "(condition1 != null && condition2 != null) || condition3 == null";
            
            Assert.Equal(expectedCode, _codeOutput.ToString());
        }
    }
}