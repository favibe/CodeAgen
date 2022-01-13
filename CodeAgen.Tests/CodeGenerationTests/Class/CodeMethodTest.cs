﻿using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
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
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.Build(_codeOutput);
            
            Assert.Equal("public void ExampleMethod()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_PublicFloat()
        {
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.SetReturnType(CodeType.Get("float"));
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_GenericOneArg()
        {
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.SetReturnType(CodeType.Get("float"));
            method.AddGenericArgument("T1");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_GenericTwoArgs()
        {
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.SetReturnType(CodeType.Get("float"));
            method.AddGenericArgument("T1");
            method.AddGenericArgument("T2");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1,T2>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_Abstract()
        {
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.SetReturnType(CodeType.Get("float"));
            method.AddGenericArgument("T1");
            method.AddGenericArgument("T2");
            method.SetAbstract(true);
            method.Build(_codeOutput);
            
            Assert.Equal("public abstract float ExampleMethod<T1,T2>()\r\n{\r\n}\r\n", _codeOutput.ToString());
        }
        
        [Fact]
        private void Build_RestrictedGeneric()
        {
            var method = new CodeClassMethod("ExampleMethod");
            method.SetAccess(CodeAccessModifier.Public);
            method.SetReturnType(CodeType.Get("float"));
            method.AddGenericArgument("T1", "class");
            method.Build(_codeOutput);
            
            Assert.Equal("public float ExampleMethod<T1>() where T1:class\r\n{\r\n}\r\n", _codeOutput.ToString());
        }

        private void Build_Inherited()
        {
            
        }
    }
}