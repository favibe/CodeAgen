using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Code.Utils;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassMethod : CodeBracedBlock, IAbstractable, IGenericable, ICodeClassMember
    {
        public byte Order => 255;
        
        private string _name;
        private CodeType _returnType = CodeType.Void;
        private CodeAccessModifier _accessModifier = CodeAccessModifier.Private;
        
        // Properties
        
        public bool IsAbstract { get; set; }
        public List<string> GenericArguments { get; set; }
        public List<string> GenericRestrictions { get; set; }
        
        // Methods
        
        public CodeClassMethod(string name)
        {
            SetName(name);
        }

        public CodeClassMethod SetName(string name)
        {
            if (!CodeName.IsValidMethodName(name))
            {
                throw new CodeBuildException($"Invalid method name: {name}");
            }
            
            _name = name;
            return this;
        }
        
        public CodeClassMethod SetReturnType(CodeType type)
        {
            _returnType = type;
            return this;
        }

        public CodeClassMethod SetAccess(CodeAccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }
        
        
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);

            WriteHeader(output);

            output.NextLine();
            
            base.Build(output);
        }
        
        private void WriteHeader(ICodeOutput output)
        {
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            
            if (IsAbstract)
            {
                this.WriteAbstract(output);
            }
            
            output.Write(_returnType);
            output.Write(CodeMarkups.Space);
            output.Write(_name);

            if (this.IsGeneric())
            {
                this.WriteGeneric(output);
            }
            
            output.Write(CodeMarkups.OpenBracket);
            output.Write(CodeMarkups.CloseBracket);

            if (this.HasRestrictions())
            {
                this.WriteRestrictions(output);
            }
        }
    }
}