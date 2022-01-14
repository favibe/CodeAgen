using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeMethodTest
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeMethodTest()
        {
            _codeOutput = new StandardCodeOutput();
        }

        [Fact]
        private void Build_PrivateVoid()
        {
            var method = new CodeMethod("ExampleMethod");
            method.Build(_codeOutput);
            
            Assert.Equal("private void ExampleMethod()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_PublicVoid()
        {
            var method = new CodeMethod("ExampleMethod", access: CodeAccessModifier.Public);
            method.Build(_codeOutput);
            
            Assert.Equal("public void ExampleMethod()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_PublicFloat()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_GenericOneArg()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Generic("T1");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_ReturnVoid()
        {
            var method = new CodeMethod("ExampleMethod");

            method.AddUnit(CodeMethod.Return());
            method.Build(_codeOutput);

            string actual = _codeOutput.ToString();
            const string expected = "private void ExampleMethod()\r\n{\r\n\treturn;\r\n}";
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_ReturnSomething()
        {
            var method = new CodeMethod("ExampleMethod");

            method.AddUnit(CodeMethod.Return("new ExampleClass()"));
            method.Build(_codeOutput);

            string actual = _codeOutput.ToString();
            const string expected = "private void ExampleMethod()\r\n{\r\n\treturn new ExampleClass();\r\n}";
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private void Build_GenericTwoArgs()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Generic("T1");
            method.Generic("T2");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1,T2>()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_Abstract()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Generic("T1");
            method.Generic("T2");
            method.Abstract(true);
            method.Build(_codeOutput);
            
            Assert.Equal("public abstract float ExampleMethod<T1,T2>()\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_RestrictedGeneric()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Generic("T1", "class");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>() where T1:class\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersSimple()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Parameter(new CodeMethodParameter(CodeType.Get("float"),"ex1"));
            method.Parameter(new CodeMethodParameter(CodeType.Get("float"),"ex2"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1, float ex2)\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersDefault()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Parameter(new CodeMethodParameter(CodeType.Get("float"),"ex1", "2f"));
            method.Parameter(new CodeMethodParameter(CodeType.Get("float"),"ex2", "5f"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1 = 2f, float ex2 = 5f)\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersAndParams()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Parameter(new CodeMethodParameter(CodeType.Get("float"),"ex1", "2f"));
            method.Params(new CodeMethodParameter(CodeType.Get("float[]"),"par"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1 = 2f, params float[] par)\r\n{\r\n}", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParams()
        {
            var method = new CodeMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Params(new CodeMethodParameter(CodeType.Get("float[]"),"par"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(params float[] par)\r\n{\r\n}", _codeOutput.ToString());
        }
    }
}