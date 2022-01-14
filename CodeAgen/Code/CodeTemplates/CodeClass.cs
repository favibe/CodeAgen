using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    /// <summary>
    /// Code template for class
    /// </summary>
    public class CodeClass : CodeBracedBlock, IAbstractable, IGenericable, IInheritable
    {
        // Fields

        private readonly CodeAccessModifier _accessModifier = CodeAccessModifier.Private;
        private readonly CodeComment _comment;
        private readonly List<ICodeClassMember> _members = new List<ICodeClassMember>();

        // Properties

        public CodeName Name { get; }

        public bool IsAbstract { get; set; }
        public List<CodeName> GenericArguments { get; set; }
        public List<string> GenericRestrictions { get; set; }
        public List<CodeType> InheritTypes { get; set; }
        
        // Methods

        public CodeClass(CodeNameVar name, CodeAccessModifier accessModifier = null, CodeComment comment = null)
        {
            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Public;
            }
            
            Name = name;
            _accessModifier = accessModifier;
            _comment = comment;
        }

        public override CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            if (!(unit is ICodeClassMember))
            {
                throw new CodeBuildException("Only class members can be added as units to class");
            }
            
            return base.AddUnit(unit);
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);

            WriteHeader(output);

            output.NextLine();
            
            base.OnBuild(output);
        }

        private void WriteHeader(ICodeOutput output)
        {
            if (_comment != null)
            {
                WriteComment(output);
            }

            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);

            if (IsAbstract)
            {
                this.WriteAbstract(output);
            }

            output.Write(CodeKeywords.Class);
            output.Write(CodeMarkups.Space);
            Name.Build(output);

            if (this.IsGeneric())
            {
                this.WriteGeneric(output);
            }

            if (this.IsInherits())
            {
                this.WriteInheritance(output);
            }

            if (this.HasRestrictions())
            {
                this.WriteRestrictions(output);
            }
        }

        private void WriteComment(ICodeOutput output)
        {
            _comment.Build(output);
            output.NextLine();
        }
    }
}