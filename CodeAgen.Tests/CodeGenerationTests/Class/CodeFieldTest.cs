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
        public void Creating_Simple()
        {
            var field = new CodeField(CodeType.Get("float"), "_field");
            
            field.Build(_codeOutput);
            
            Assert.Equal("private float _field;", _codeOutput.ToString());
        }
        
        [Fact]
        public void Creating_Public()
        {
            var field = new CodeField(CodeType.Get("float"), "Field", accessModifier: CodeAccessModifier.Public);
            
            field.Build(_codeOutput);
            
            Assert.Equal("public float Field;", _codeOutput.ToString());
        }
        
        [Fact]
        public void Creating_WithValue()
        {
            var field = new CodeField(CodeType.Get("float"), "_field", "5");
            
            field.Build(_codeOutput);
            
            Assert.Equal("private float _field = 5;", _codeOutput.ToString());
        }
        
        [Fact]
        public void Field_AddingToClass()
        {
            var @class = new CodeClass("ClassName");

            var field = new CodeField(CodeType.Get("float"), "_field", "5");

            @class.AddUnit(field);
            @class.Build(_codeOutput);

            Assert.Equal("public class ClassName\r\n{\r\n\tprivate float _field = 5;\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        public void Creating_Readonly()
        {
            var field = new CodeField(CodeType.Get("float"), "_field", "5", isReadonly:true);
            
            field.Build(_codeOutput);
            
            Assert.Equal("private readonly float _field = 5;", _codeOutput.ToString());
        }
    }
}