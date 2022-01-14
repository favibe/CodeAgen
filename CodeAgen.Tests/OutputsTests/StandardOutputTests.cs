using System;
using System.Collections.Generic;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.OutputsTests
{
    public class StandardOutputTests
    {
        private readonly ICodeOutput _output;
        
        public StandardOutputTests()
        {
            _output = new StandardCodeOutput();
        }
        
        [Theory]
        [InlineData("abcdefg", new[] {"ab", "cde", "fg"})]
        [InlineData("hola amigo", new[] {"hola", " ", "amigo"})]
        [InlineData("class ClassExample : object", new[] {"class", " ", "ClassExample", " ", ": object"})]
        public void Write_PlainWrites(string expected, string[] args)
        {
            foreach (var arg in args)
            {
                _output.Write(arg);
            }

            Assert.Equal(expected, _output.ToString());
            _output.Clear();
        }

        [Theory]
        [InlineData("ab\r\ncde\r\nfg", new[] {"ab", "cde", "fg"})]
        [InlineData("hola\r\namigo", new[] {"hola", "amigo"})]
        [InlineData("class\r\nClassExample\r\n: object", new[] {"class", "ClassExample",": object"})]
        public void Write_LineByLine(string expected, string[] args)
        {
            foreach (var arg in args)
            {
                _output.WriteLine(arg);
            }

            Assert.Equal(expected, _output.ToString());
            _output.Clear();
        }

        [Theory]
        [InlineData("ab\r\ncde\r\nfg", new[] {"ab", "cde", "fg"})]
        [InlineData("hola\r\namigo", new[] {"hola", "amigo"})]
        [InlineData("class\r\nClassExample\r\n: object", new[] {"class", "ClassExample",": object"})]
        public void Write_LineSkip(string expected, string[] args)
        {
            foreach (var arg in args)
            {
                _output.Write(arg);
                _output.NextLine();
            }

            Assert.Equal(expected, _output.ToString());
            _output.Clear();
        }
        
        [Theory]
        [MemberData(nameof(Write_Tabbed_Data))]
        public void Write_Tabbed(string expected, (int tabLevel, string data)[] args)
        {
            foreach (var arg in args)
            {
                _output.SetTab(arg.tabLevel);
                _output.Write(arg.data);
                _output.NextLine();
            }

            Assert.Equal(expected, _output.ToString());
            _output.Clear();
        }
        
        [Fact]
        public void Clear()
        {
            for (int i = 0; i < 5; i++)
            {
                _output.Write(new Guid().ToString());
            }
            
            Assert.NotEqual(string.Empty, _output.ToString());
            _output.Clear();
            Assert.Equal(string.Empty, _output.ToString());
        }

        public static IEnumerable<object[]> Write_Tabbed_Data()
        {
            yield return new object[] {"\ta", new[] {(1, "a")}};
            yield return new object[] {"\ta\r\n\t\tbcd\r\nefg", new[] {(1, "a"), (2, "bcd"), (0, "efg")}};
        }
    }
}