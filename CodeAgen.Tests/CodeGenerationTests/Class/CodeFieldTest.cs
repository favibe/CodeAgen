using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeFieldTest
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeFieldTest()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Field_Creating()
        {
            var field = new CodeClassField(CodeType.Get("float"), "field");
            
            field.Build(_codeOutput);
            
            Assert.Equal("private float _field;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Field_CreatingPublic()
        {
            var field = new CodeClassField(CodeType.Get("float"), "field", accessModifier: CodeAccessModifier.Public);
            
            field.Build(_codeOutput);
            
            Assert.Equal("public float Field;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Field_CreatingWithValue()
        {
            var field = new CodeClassField(CodeType.Get("float"), "field", "5");
            
            field.Build(_codeOutput);
            
            Assert.Equal("private float _field = 5;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Field_AddingToClass()
        {
            var @class = new CodeClassTemplate("ClassName");

            var field = new CodeClassField(CodeType.Get("float"), "field", "5");

            @class.AddUnit(field);
            @class.Build(_codeOutput);

            Assert.Equal("private class ClassName\r\n{\r\n\tprivate float _field = 5;\r\n}\r\n", _codeOutput.ToString());
        }
    }
}