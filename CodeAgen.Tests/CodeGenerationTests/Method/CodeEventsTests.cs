using CodeAgen.Code.CodeTemplates.MethodMembers.Events;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Method
{
    public class CodeEventsTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeEventsTests()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_EventSubscribe()
        {
            var code = new CodeEventSubscribe("event", "instance.Foo");
            code.Build(_codeOutput);
            
            Assert.Equal("event += instance.Foo;\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_EventUnsubscribe()
        {
            var code = new CodeEventUnsubscribe("event", "instance.Foo");
            code.Build(_codeOutput);
            
            Assert.Equal("event -= instance.Foo;\r\n", _codeOutput.ToString());
        }
    }
}