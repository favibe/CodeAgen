﻿using CodeAgen.Code;
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
            var method = new CodeClassMethod("ExampleMethod");
            method.Build(_codeOutput);
            
            Assert.Equal("private void ExampleMethod()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_PublicVoid()
        {
            var method = new CodeClassMethod("ExampleMethod", access: CodeAccessModifier.Public);
            method.Build(_codeOutput);
            
            Assert.Equal("public void ExampleMethod()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_PublicFloat()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_GenericOneArg()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddGenericArgument("T1");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_GenericTwoArgs()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddGenericArgument("T1");
            method.AddGenericArgument("T2");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1,T2>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_Abstract()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddGenericArgument("T1");
            method.AddGenericArgument("T2");
            method.SetAbstract(true);
            method.Build(_codeOutput);
            
            Assert.Equal("public abstract float ExampleMethod<T1,T2>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_RestrictedGeneric()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddGenericArgument("T1", "class");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>() where T1:class\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersSimple()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddParameter(new CodeMethodParameter(CodeType.Get("float"),"ex1"));
            method.AddParameter(new CodeMethodParameter(CodeType.Get("float"),"ex2"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1, float ex2)\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersDefault()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddParameter(new CodeMethodParameter(CodeType.Get("float"),"ex1", "2f"));
            method.AddParameter(new CodeMethodParameter(CodeType.Get("float"),"ex2", "5f"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1 = 2f, float ex2 = 5f)\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParametersAndParams()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddParameter(new CodeMethodParameter(CodeType.Get("float"),"ex1", "2f"));
            method.AddParams(new CodeMethodParameter(CodeType.Get("float[]"),"par"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(float ex1 = 2f, params float[] par)\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_WithParams()
        {
            var method = new CodeClassMethod("ExampleMethod", "float", CodeAccessModifier.Public);
            method.AddParams(new CodeMethodParameter(CodeType.Get("float[]"),"par"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod(params float[] par)\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
    }
}