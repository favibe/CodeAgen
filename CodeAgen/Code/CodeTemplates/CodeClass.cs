using System;
using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    /// <summary>
    /// Code template for class
    /// </summary>
    public class CodeClass : CodeBracedBlock, IAbstractable, IGenericable, IInheritable
    {
        // Fields

        private readonly CodeAccessModifier _access;
        private readonly CodeComment _comment;

        // Properties

        public CodeName Name { get; }

        public bool IsAbstract { get; set; }
        public List<CodeName> GenericArguments { get; set; }
        public List<string> GenericRestrictions { get; set; }
        public List<CodeType> InheritTypes { get; set; }
        
        // Methods

        public CodeClass(CodeNameVar name, CodeAccessModifier access = null, CodeComment comment = null)
        {
            if (access == null)
            {
                access = CodeAccessModifier.Public;
            }
            
            Name = name;
            _access = access;
            _comment = comment;
        }

        public CodeClass Method(CodeMethod method) => AddAndReturn(method);
        public CodeClass Field(CodeField field) => AddAndReturn(field);
        public CodeClass Property(CodeProperty property) => AddAndReturn(property);
        public CodeClass Constant(CodeConst constant) => AddAndReturn(constant);
        public CodeClass Event(CodeEvent @event) => AddAndReturn(@event);
        public CodeClass Constructor(CodeConstructor constructor) => AddAndReturn(constructor);
        public CodeClass Space() => AddAndReturn(CodeLine.Empty);

        [Obsolete("Use special methods instead")]
        public override CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            return base.AddUnit(unit);
        }

        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);

            WriteHeader(output);

            output.NextLine();
            
            base.OnBuild(output);
        }

        private CodeClass AddAndReturn(CodeTabbable unit)
        {
            AddUnit(unit);
            return this;
        }

        private void WriteHeader(ICodeOutput output)
        {
            if (_comment != null)
            {
                WriteComment(output);
            }

            output.Write(_access);
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