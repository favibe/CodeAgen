using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeClassPropertyTest
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeClassPropertyTest()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_Property()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter(new CodeLine("getterCode"));
            property.AddSetter(new CodeLine("setterCode"));
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();
            
            Assert.Equal("public float Example\r\n{\r\n\tget\r\n\t{\r\n\t\tgetterCode;\r\n\t}\r\n\tset\r\n\t{\r\n\t\tsetterCode;\r\n\t}\r\n}\r\n", code);
        }
        
        [Fact]
        private void Build_AutoBoth()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter();
            property.AddSetter();
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "public float Example\r\n{\r\n\tget;\r\n\tset;\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AutoGetterOnly()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter();
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "public float Example\r\n{\r\n\tget;\r\n}\r\n";
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AutoSetterOnly()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddSetter();
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "public float Example\r\n{\r\n\tset;\r\n}\r\n";
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AutoSetterPrivate()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter();
            property.AddSetter(accessModifier: CodeAccessModifier.Private);
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "public float Example\r\n{\r\n\tget;\r\n\tprivate set;\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AutoGetterPrivate()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter(accessModifier: CodeAccessModifier.Private);
            property.AddSetter();
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "public float Example\r\n{\r\n\tprivate get;\r\n\tset;\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AccessModifierGetter()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter(new CodeLine("getterCode"), CodeAccessModifier.Private);
            property.AddSetter(new CodeLine("setterCode"));
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode =
                "public float Example\r\n{\r\n\tprivate get\r\n\t{\r\n\t\tgetterCode;\r\n\t}\r\n\tset\r\n\t{\r\n\t\tsetterCode;\r\n\t}\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_AccessModifierSetter()
        {
            var property = new CodeProperty("Example", CodeType.Get("float"));
            
            property.AddGetter(new CodeLine("getterCode"));
            property.AddSetter(new CodeLine("setterCode"), CodeAccessModifier.Private);
            
            property.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode =
                "public float Example\r\n{\r\n\tget\r\n\t{\r\n\t\tgetterCode;\r\n\t}\r\n\tprivate set\r\n\t{\r\n\t\tsetterCode;\r\n\t}\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }

        [Fact]
        private void Build_InClass()
        {
            var @class = new CodeClass("ExampleClass");

            var property1 = new CodeProperty("Example1", CodeType.Get("float"));
            var property2 = new CodeProperty("Example2", CodeType.Get("float"));

            @class.AddUnit(property1);
            @class.AddUnit(property2);
            
            property1.AddGetter(new CodeLine("getterCode"));
            property1.AddSetter(new CodeLine("setterCode"));
            
            property2.AddGetter(new CodeLine("getterCode"));
            property2.AddSetter(new CodeLine("setterCode"));

            @class.Build(_codeOutput);

            var code = _codeOutput.ToString();
            
            const string targetCode =
                "public class ExampleClass\r\n{\r\n\tpublic float Example1\r\n\t{\r\n\t\tget\r\n\t\t{\r\n\t\t\tgetterCode;\r\n\t\t}\r\n\t\tset\r\n\t\t{\r\n\t\t\tsetterCode;\r\n\t\t}\r\n\t}\r\n\tpublic float Example2\r\n\t{\r\n\t\tget\r\n\t\t{\r\n\t\t\tgetterCode;\r\n\t\t}\r\n\t\tset\r\n\t\t{\r\n\t\t\tsetterCode;\r\n\t\t}\r\n\t}\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
    }
}