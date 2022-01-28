using CodeAgen.Exceptions;
using CodeAgen.Interfaces;
using Xunit;
using Provider = CodeAgen.Concrete.CodeTypeProvider;

namespace CodeAgen.Tests.Typing
{
    public class CodeTypeProvider
    {
        private readonly ICodeTypeProvider _provider = new Provider();

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
            var baseTypesCount = _provider.Types.Count;
            
            Create_SingleType("Namespace.Type1", "Namespace", "Type1");
            Create_SingleType("Namespace.Type2", "Namespace", "Type2");
            
            Assert.Equal(baseTypesCount + 2, _provider.Types.Count);
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
        [InlineData("System.Boolean", "bool")]
        [InlineData("System.Boolean", "bool")]
        [InlineData("System.SByte", "sbyte")]
        [InlineData("System.Byte", "byte")]
        [InlineData("System.Int16", "short")]
        [InlineData("System.UInt16", "ushort")]
        [InlineData("System.Int32", "int")]
        [InlineData("System.UInt32", "uint")]
        [InlineData("System.Int64", "long")]
        [InlineData("System.UInt64", "ulong")]
        [InlineData("System.Single", "float")]
        [InlineData("System.Double", "double")]
        [InlineData("System.Decimal", "decimal")]
        [InlineData("System.Char", "char")]
        [InlineData("System.Object", "object")]
        [InlineData("dynamic", "dynamic")]
        [InlineData("System.String", "string")]
        public void Get_BaseTypes(string input, string expected)
        {
            if (!_provider.TryGetType(input, out var type))
            {
                throw new CodeTypeException("Base type not found");
            }
            
            Assert.Equal(expected, type.ShortName);
        }
    }
}