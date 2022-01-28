using CodeAgen.Exceptions;
using Xunit;
using Provider = CodeAgen.Concrete.CodeTypeProvider;

namespace CodeAgen.Tests.Typing
{
    public class CodeTypeProvider
    {
        private readonly Provider _provider = new Provider();

        [Theory]
        [InlineData("ExampleSpace.TypeA", "ExampleSpace", "TypeA")]
        [InlineData("ExampleSpace.Subspace.TypeB", "ExampleSpace.Subspace", "TypeB")]
        public void Create_SingleType(string source, string expectedNamespace, string expectedShortName)
        {
            var type = _provider.CreateType(source);
            
            Assert.Equal(expectedNamespace, type.Namespace);
            Assert.Equal(expectedShortName, type.ShortName);
            Assert.Equal(source, type.FullName);
        }
        
        [Fact]
        public void Create_MultipleTypes()
        {
            Create_SingleType("Namespace.Type1", "Namespace", "Type1");
            Create_SingleType("Namespace.Type2", "Namespace", "Type2");
            
            Assert.Equal(2, _provider.Types.Count);
        }
        
        [Fact]
        public void Create_DuplicateTypes()
        {
            Assert.Throws(typeof(CodeTypeException), () =>
            {
                Create_SingleType("Namespace.Type1", "Namespace", "Type1");
                Create_SingleType("Namespace.Type1", "Namespace", "Type1");
            });
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
                _provider.CreateType(input);
            });
        }
    }
}