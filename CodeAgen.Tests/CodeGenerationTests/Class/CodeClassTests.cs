using System.CodeDom;
using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;
using CodeComment = CodeAgen.Code.Basic.CodeComment;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeClassTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeClassTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        public void Build_EmptyNoTab()
        {
            var @class = new CodeClass("ExampleClass");
            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_EmptyWithTab()
        {
            var @class = new CodeClass("ExampleClass")
            {
                Level = 1
            };
            
            @class.Build(_codeOutput);
            
            Assert.Equal("\tpublic class ExampleClass\r\n\t{\r\n\t}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_PublicNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }

        [Fact]
        public void Build_PublicCommentedNoTab()
        {
            var @class = new CodeClass(
                "ExampleClass", 
                CodeAccessModifier.Public,
                new CodeComment("Class comment"));

            @class.Build(_codeOutput);

            Assert.Equal("// Class comment\r\npublic class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }

        [Fact]
        public void Build_AbstractNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Abstract(true);

            @class.Build(_codeOutput);
            
            Assert.Equal("public abstract class ExampleClass\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_GenericNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Generic("T")
                .Generic("A");

            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass<T,A>\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_InvalidGenericNoTab()
        {
            var @class = new CodeClass("ExampleClass");
            @class.Generic("T1");

            Assert.Throws(typeof(CodeNamingException),() => @class.Generic("1T"));
            Assert.Throws(typeof(CodeNamingException),() => @class.Generic("T;"));
        }

        [Fact]
        public void Build_InvalidName()
        {
            Assert.Throws(typeof(CodeNamingException),() => new CodeClass("1ExampleClass"));
            Assert.Throws(typeof(CodeNamingException),() => new CodeClass("Exa;mpleClass"));
        }

        [Fact]
        public void Build_RestrictedGenericNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Generic("T", "class")
                .Generic("A", "new()");

            @class.Build(_codeOutput);
            
                Assert.Equal("public class ExampleClass<T,A> where T:class where A:new()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_RestrictedGenericInheritedNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Inherit(CodeType.Get("ParentType"));

            @class.Generic("T", "class")
                .Generic("A", "new()");
            
            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass<T,A> : ParentType where T:class where A:new()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_AbstractGenericNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Generic("T")
                .Generic("A");

            @class.Abstract(true);
            @class.Build(_codeOutput);
            
            Assert.Equal("public abstract class ExampleClass<T,A>\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        public void Build_InheritedNoTab()
        {
            var @class = new CodeClass("ExampleClass", CodeAccessModifier.Public);

            @class
                .Inherit(CodeType.Get("ParentClass"))
                .Inherit(CodeType.Get("IExampleInterface"));

            @class.Build(_codeOutput);
            
            Assert.Equal("public class ExampleClass : ParentClass, IExampleInterface\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
    }
}