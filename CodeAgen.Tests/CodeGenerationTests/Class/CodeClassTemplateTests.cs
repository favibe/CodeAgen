using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeClassTemplateTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeClassTemplateTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Build_EmptyNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");
            @class.Build(_codeOutput);
            
            Assert.Equal("private class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_EmptyWithTab()
        {
            var @class = new CodeClassTemplate("ExampleClass")
            {
                Level = 1
            };
            
            @class.Build(_codeOutput);
            
            Assert.Equal("\tprivate class ExampleClass\r\n\t{\r\n\t}\r\n\t", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_PublicNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class.SetAccess(CodeAccessModifier.Public);
            
            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_PublicCommentedNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class.SetAccess(CodeAccessModifier.Public)
                .Comment(new CodeComment("Class comment"));
            
            @class.Build(_codeOutput);

            Assert.Equal("// Class comment\r\npublic class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }

        [Fact]
        public void Build_AbstractNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .SetAbstract(true);

            @class.Build(_codeOutput);
            
            Assert.Equal("public abstract class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_GenericNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .AddGenericArgument("T")
                .AddGenericArgument("A");

            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass<T,A>\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_InvalidGenericNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");
            @class.AddGenericArgument("T1");

            Assert.Throws(typeof(CodeBuildException),() => @class.AddGenericArgument("1T"));
            Assert.Throws(typeof(CodeBuildException),() => @class.AddGenericArgument("T;"));
        }

        [Fact]
        public void Build_RestrictedGenericNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .AddGenericArgument("T", "class")
                .AddGenericArgument("A", "new()");

            @class.Build(_codeOutput);
            
                Assert.Equal("public class ExampleClass<T,A> where T:class where A:new()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_RestrictedGenericInheritedNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .InheritFrom(CodeType.Get("ParentType"));

            @class.AddGenericArgument("T", "class")
                .AddGenericArgument("A", "new()");
            
            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass<T,A> : ParentType where T:class where A:new()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_AbstractGenericNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .AddGenericArgument("T")
                .AddGenericArgument("A");

            @class.SetAbstract(true);
            @class.Build(_codeOutput);
            
            Assert.Equal("public abstract class ExampleClass<T,A>\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_InheritedNoTab()
        {
            var @class = new CodeClassTemplate("ExampleClass");

            @class
                .SetAccess(CodeAccessModifier.Public)
                .InheritFrom(CodeType.Get("ParentClass"))
                .InheritFrom(CodeType.Get("IExampleInterface"));

            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass : ParentClass, IExampleInterface\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
    }
}