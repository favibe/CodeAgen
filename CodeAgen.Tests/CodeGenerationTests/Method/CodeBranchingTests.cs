using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching.Switch;
using CodeAgen.Code.CodeTemplates.MethodMembers.Loops;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Method
{
    public class CodeBranchingTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeBranchingTests()
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
        private void Build_Case()
        {
            var codeCase = new CodeSwitchCase(new CodeRawString("case1"), new CodeLine("do1"), CodeLoop.Break);
            codeCase.Build(_codeOutput);

            var actual = _codeOutput.ToString();
            const string expected = "case case1:\r\n\tdo1;\r\n\tbreak;\r\n\t";
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_Default()
        {
            var codeCase = new CodeSwitchDefault(new CodeLine("do1"), CodeLoop.Break);
            codeCase.Build(_codeOutput);

            var actual = _codeOutput.ToString();
            const string expected = "default:\r\n\tdo1;\r\n\tbreak;\r\n\t";
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_Cases()
        {
            var frag = new CodeFragment(new List<CodeUnit>()
            {
                new CodeSwitchCase(new CodeRawString("case1"), new CodeLine("do1"), CodeLoop.Break),
                new CodeSwitchCase(new CodeRawString("case2"), new CodeLine("do2"), CodeLoop.Break),
                new CodeSwitchCase(new CodeRawString("case3")),
                new CodeSwitchCase(new CodeRawString("case4")),
                new CodeSwitchCase(new CodeRawString("case5"), new CodeLine("do5"), CodeLoop.Break)
            });
            
            
            frag.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();
            const string expected =
                "case case1:\r\n\tdo1;\r\n\tbreak;\r\ncase case2:\r\n\tdo2;\r\n\tbreak;\r\ncase case3:\r\ncase case4:\r\ncase case5:\r\n\tdo5;\r\n\tbreak;\r\n";
            
            Assert.Equal(expected, actualCode);
        }
        
        [Fact]
        private void Build_CasesWithTab()
        {
            var frag = new CodeFragment(new List<CodeUnit>()
            {
                new CodeSwitchCase(new CodeRawString("case1"), new CodeLine("do1"), CodeLoop.Break),
                new CodeSwitchCase(new CodeRawString("case2"), new CodeLine("do2"), CodeLoop.Break),
                new CodeSwitchCase(new CodeRawString("case3")),
                new CodeSwitchCase(new CodeRawString("case4")),
                new CodeSwitchCase(new CodeRawString("case5"), new CodeLine("do5"), CodeLoop.Break)
            })
            {
                Level = 2
            };

            frag.Build(_codeOutput);

            var actualCode = _codeOutput.ToString();
            const string expected =
                "\t\tcase case1:\r\n\t\t\tdo1;\r\n\t\t\tbreak;\r\n\t\tcase case2:\r\n\t\t\tdo2;\r\n\t\t\tbreak;\r\n\t\tcase case3:\r\n\t\tcase case4:\r\n\t\tcase case5:\r\n\t\t\tdo5;\r\n\t\t\tbreak;\r\n\t\t";
            Assert.Equal(expected, actualCode);
        }
        
        [Fact]
        private void Build_SwitchEmpty()
        {
            var switchCode = new CodeSwitch("var1");
            
            switchCode.Build(_codeOutput);
            
            var actualCode = _codeOutput.ToString();
            const string expected =
                "switch(var1)\r\n{\r\n}\r\n";
            Assert.Equal(expected, actualCode);
        }
        
        [Fact]
        private void Build_SwitchCases()
        {
            var switchCode = new CodeSwitch("variable");

            switchCode.Default(new CodeSwitchDefault(new CodeLine("doDefault"), CodeLoop.Break))
                .Case(new CodeSwitchCase("case1", new CodeLine("do1"), CodeLoop.Break))
                .Case(new CodeSwitchCase("case2"))
                .Case(new CodeSwitchCase("case3", new CodeLine("do3"), CodeLoop.Break));
            
            switchCode.Build(_codeOutput);
            
            var actualCode = _codeOutput.ToString();

            const string expected =
                "switch(variable)\r\n{\r\n\tcase case1:\r\n\t\tdo1;\r\n\t\tbreak;\r\n\tcase case2:\r\n\tcase case3:\r\n\t\tdo3;\r\n\t\tbreak;\r\n\tdefault:\r\n\t\tdoDefault;\r\n\t\tbreak;\r\n}\r\n";
            
            Assert.Equal(expected, actualCode);
        }
        
        [Fact]
        private void Build_SwitchCasesWithTab()
        {
            var frag = new CodeFragment
            {
                Level = 2
            };

            var switchCode = new CodeSwitch("variable");

            frag.AddUnit(switchCode);

            switchCode
                .Case(new CodeSwitchCase("case1", new CodeLine("do1"), CodeLoop.Break))
                .Case(new CodeSwitchCase("case2"))
                .Case(new CodeSwitchCase("case3", new CodeLine("do3"), CodeLoop.Break))
                .Default(new CodeSwitchDefault(new CodeLine("doDefault"), CodeLoop.Break));
            
            switchCode.Build(_codeOutput);
            
            var actualCode = _codeOutput.ToString();

            const string expected =
                "\t\tswitch(variable)\r\n\t\t{\r\n\t\t\tcase case1:\r\n\t\t\t\tdo1;\r\n\t\t\t\tbreak;\r\n\t\t\tcase case2:\r\n\t\t\tcase case3:\r\n\t\t\t\tdo3;\r\n\t\t\t\tbreak;\r\n\t\t\tdefault:\r\n\t\t\t\tdoDefault;\r\n\t\t\t\tbreak;\r\n\t\t}\r\n\t\t";
            
            Assert.Equal(expected, actualCode);
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