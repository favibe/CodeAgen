using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Method
{
    public class CodeIfElseTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeIfElseTests()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_Simple()
        {
            var @if = new CodeIfElse("condition", new CodeLine("do"));
            
            @if.Build(_codeOutput);

            const string expected = "if (condition)\r\n{\r\n\tdo;\r\n}\r\n";
            string actual = _codeOutput.ToString();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_Elseif()
        {
            var @if = new CodeIfElse("condition", new CodeLine("do"));
            @if.ElseIf("condition2", new CodeLine("do2"));
            
            @if.Build(_codeOutput);

            const string expected = "if (condition)\r\n{\r\n\tdo;\r\n}\r\nelse if (condition2)\r\n{\r\n\tdo2;\r\n}\r\n";
            string actual = _codeOutput.ToString();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_Else()
        {
            var @if = new CodeIfElse("condition", new CodeLine("do"));
            @if.Else(new CodeLine("do2"));
            
            @if.Build(_codeOutput);

            const string expected = "if (condition)\r\n{\r\n\tdo;\r\n}\r\nelse\r\n{\r\n\tdo2;\r\n}\r\n";
            string actual = _codeOutput.ToString();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_InTab()
        {
            var codeFrag = new CodeFragment
            {
                Level = 1
            };

            var @if = new CodeIfElse("condition1", new CodeLine("do1"));
            @if.ElseIf("condition2", new CodeLine("do2"));
            @if.ElseIf("condition3", new CodeLine("do3"));
            @if.Else(new CodeLine("do2"));

            codeFrag.AddUnit(@if);
            codeFrag.Build(_codeOutput);

            const string expected = "\tif (condition1)\r\n\t{\r\n\t\tdo1;\r\n\t}\r\n\telse if (condition2)\r\n\t{\r\n\t\tdo2;\r\n\t}\r\n\telse if (condition3)\r\n\t{\r\n\t\tdo3;\r\n\t}\r\n\telse\r\n\t{\r\n\t\tdo2;\r\n\t}\r\n\t";
            var actual = _codeOutput.ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}