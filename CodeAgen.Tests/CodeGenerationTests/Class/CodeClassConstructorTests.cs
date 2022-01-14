using CodeAgen.Code;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Outputs;
using CodeAgen.Outputs.Entities;
using Xunit;

namespace CodeAgen.Tests.CodeGenerationTests.Class
{
    public class CodeClassConstructorTests
    {
        private readonly ICodeOutput _codeOutput;
        
        public CodeClassConstructorTests()
        {
            _codeOutput = new StandardCodeOutput();
        }
        
        [Fact]
        private void Created_PrivateSimple()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);
            constructor.Build(_codeOutput);

            const string targetCode = "private ExampleClass()\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, _codeOutput.ToString());
        }
        
        [Fact]
        private void Created_PublicSimple()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class, CodeAccessModifier.Public);
            constructor.Build(_codeOutput);

            const string targetCode = "public ExampleClass()\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, _codeOutput.ToString());
        }
        
        [Fact]
        private void Created_WithParameters()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);

            constructor.AddParameter(new CodeClassParameter("par1", CodeType.Get("float")));
            constructor.AddParameter(new CodeClassParameter("par2", CodeType.Get("float"), "2"));
            
            constructor.Build(_codeOutput);
            var code = _codeOutput.ToString();

            const string targetCode = "private ExampleClass(float par1, float par2 = 2)\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Created_WithParams()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);

            constructor.AddParams(new CodeClassParameter("par", CodeType.Get("float[]")));
        
            constructor.Build(_codeOutput);
            var code = _codeOutput.ToString();

            const string targetCode = "private ExampleClass(params float[] par)\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Created_WithParametersAndParams()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);

            constructor.AddParameter(new CodeClassParameter("par1", CodeType.Get("float")));
            constructor.AddParameter(new CodeClassParameter("par2", CodeType.Get("float"), "2"));
            constructor.AddParams(new CodeClassParameter("par", CodeType.Get("float[]")));
        
            constructor.Build(_codeOutput);
            var code = _codeOutput.ToString();

            const string targetCode = "private ExampleClass(float par1, float par2 = 2, params float[] par)\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Created_InheritsFromBaseSimple()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);
            constructor.InheritFromBase();
            
            constructor.Build(_codeOutput);
            var code = _codeOutput.ToString();

            const string targetCode = "private ExampleClass():base()\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact()]
        private void Created_InheritsFromBaseWithParameters()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);

            constructor.AddParameter(new CodeClassParameter("par1", CodeType.Get("float")));
            constructor.AddParameter(new CodeClassParameter("par2", CodeType.Get("float")));
            
            constructor.InheritFromBase("par1", "par2");
            
            constructor.Build(_codeOutput);
            var code = _codeOutput.ToString();

            const string targetCode = "private ExampleClass(float par1, float par2):base(par1,par2)\r\n{\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
        [Fact]
        private void Inserted_InClass()
        {
            var @class = new CodeClass("ExampleClass");
            var constructor = CodeClassConstructor.CreateFor(@class);

            @class.AddUnit(constructor);
            @class.Build(_codeOutput);

            var code = _codeOutput.ToString();
            const string targetCode = "public class ExampleClass\r\n{\r\n\tprivate ExampleClass()\r\n\t{\r\n\t}\r\n}\r\n";
            
            Assert.Equal(targetCode, code);
        }
        
    }
}