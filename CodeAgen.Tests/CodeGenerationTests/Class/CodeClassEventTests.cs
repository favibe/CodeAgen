using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeClassEventTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeClassEventTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        private void Build_Public()
        {
            var @event = new CodeClassEvent("ExampleEvent", CodeType.Get("Action"));
            @event.Build(_codeOutput);
            
            Assert.Equal("public event Action ExampleEvent;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_Private()
        {
            var @event = new CodeClassEvent("ExampleEvent", CodeType.Get("Action"), CodeAccessModifier.Private);
            @event.Build(_codeOutput);
            
            Assert.Equal("private event Action ExampleEvent;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Add_ToClass()
        {
            var @class = new CodeClass("ExampleClass");
            var @event = new CodeClassEvent("ExampleEvent", CodeType.Get("Action"), CodeAccessModifier.Private);
            
            @class.AddUnit(@event);
            @class.Build(_codeOutput);
            
            Assert.Equal("private class ExampleClass\r\n{\r\n\tprivate event Action ExampleEvent;\r\n}\r\n", _codeOutput.ToString());
        }
    }
}