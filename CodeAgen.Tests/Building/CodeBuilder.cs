using CodeAgen.Interfaces;
using Xunit;
using Builder = CodeAgen.Concrete.CodeBuilder;

namespace CodeAgen.Tests.Building
{
    public class CodeBuilder
    {
        private readonly ICodeBuilder _builder = new Builder();

        [Fact]
        public void Creating()
        {
            Assert.NotNull(_builder);
        }

        [Theory]
        [InlineData('a')]
        [InlineData('c')]
        [InlineData('\t')]
        [InlineData('\r')]
        public void Append_Char(char @char)
        {
            _builder.Append(@char);
            Assert.Equal(@char.ToString(), _builder.ToString());
        }
        
        [Theory]
        [InlineData("hello")]
        [InlineData("world")]
        [InlineData("some\nstring")]
        [InlineData("other\t\tstring")]
        public void Append_String(string @string)
        {
            _builder.Append(@string);
            Assert.Equal(@string, _builder.ToString());
        }

        [Fact]
        public void Clear()
        {
            _builder.Append("stuff");
            _builder.Clear();
            Assert.True(string.IsNullOrEmpty(_builder.ToString()));
        }

        [Fact]
        public void Append_NextLine_NoTab()
        {
            _builder.Append("Line1");
            _builder.NextLine();
            _builder.Append("Line2");
            
            Assert.Equal("Line1\nLine2", _builder.ToString());
        }
        
        [Fact]
        public void Append_NextLine_SameTab()
        {
            _builder.SetTab(2);
            _builder.Append("Line1");
            _builder.NextLine();
            _builder.Append("Line2");
            
            Assert.Equal("\t\tLine1\n\t\tLine2", _builder.ToString());
        }
        
        [Fact]
        public void Append_NextLine_DifferentTab()
        {
            _builder.SetTab(2);
            _builder.Append("Line1");
            _builder.NextLine();
            _builder.SetTab(3);
            _builder.Append("Line2");
            _builder.NextLine();
            _builder.SetTab(0);
            _builder.Append("Line3");
            
            Assert.Equal("\t\tLine1\n\t\t\tLine2\nLine3", _builder.ToString());
        }

        [Fact]
        public void Chain_Call()
        {
            _builder.Append('a')
                .Append("line")
                .NextLine()
                .SetTab(2)
                .Clear()
                .ToString();
        }
    }
}