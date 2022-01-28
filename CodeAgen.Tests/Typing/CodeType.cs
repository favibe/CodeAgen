using CodeAgen.Exceptions;
using Xunit;

namespace CodeAgen.Tests.Typing
{
    public class CodeType
    {
        [Fact]
        public void NotEquals_DifferentShortNames()
        {
            var nameA = new Primitives.CodeType("Namespace.Name");
            var nameB = new Primitives.CodeType("Namespace.Name2");
            
            Assert.NotEqual(nameA, nameB);
        }
        
        [Fact]
        public void NotEquals_DifferentNamespaces()
        {
            var nameA = new Primitives.CodeType("Namespace.Name");
            var nameB = new Primitives.CodeType("Namespace2.Name");
            
            Assert.NotEqual(nameA, nameB);
        }

        [Fact]
        public void Parse_Namespace()
        {
            var type = new Primitives.CodeType("Namespace.Name");
            
            Assert.Equal("Namespace", type.Namespace);
        }
        
        [Fact]
        public void Parse_FullName()
        {
            var type = new Primitives.CodeType("Namespace.Name");
            
            Assert.Equal("Namespace.Name", type.FullName);
        }
        
        [Fact]
        public void Parse_ShortName()
        {
            var type = new Primitives.CodeType("Namespace.Name");
            
            Assert.Equal("Name", type.ShortName);
        }
        
        [Theory]
        [InlineData("1Namespace.Format")]
        [InlineData("Namespace.1Format")]
        [InlineData("N$amespace.Format")]
        [InlineData("Namesp-ace.Format")]
        [InlineData(".Namespace.Format")]
        [InlineData("Namespace.Format.")]
        public void Create_BadFormat(string input)
        {
            Assert.Throws(typeof(CodeTypeException), () =>
            {
                new Primitives.CodeType(input);
            });
        }
    }
}