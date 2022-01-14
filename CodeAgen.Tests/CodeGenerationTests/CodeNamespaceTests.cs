using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests
{
    public class CodeNamespaceTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeNamespaceTests()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_Simple()
        {
            var @namespace = new CodeNamespace("ExampleNamespace.Subspace");
            
            @namespace.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "namespace ExampleNamespace.Subspace\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }

        [Fact]
        private void Build_ClassInside()
        {
            var @namespace = new CodeNamespace("ExampleNamespace.Subspace");

            @namespace.AddUnit(new CodeClass("ExampleClass"));
            
            @namespace.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode =
                "namespace ExampleNamespace.Subspace\r\n{\r\n\tpublic class ExampleClass\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Build_InvalidName()
        {
            new CodeNamespace("Exam1pleNamespace.Subspace");
            
            Assert.Throws(typeof(CodeNamingException), () => new CodeNamespace("Exa\\mpleNamespace.Subspace"));
            Assert.Throws(typeof(CodeNamingException), () => new CodeNamespace("1ExampleNamespace.Subspace"));
        }
        
        [Fact]
        private void Build_Usings()
        {
            var codeFragment = new CodeFragment();

            var @using = new CodeUsing("Example1.Subspace");
            codeFragment.AddUnit(@using);
            @using = new CodeUsing("Example2.Subspace");
            codeFragment.AddUnit(@using);

            codeFragment.Build(_codeOutput);

            var code = _codeOutput.ToString();

            const string targetCode = "using Example1.Subspace;\r\nusing Example2.Subspace;\r\n";
            
            Assert.Equal(targetCode, code);
        }
    }
}