using System;
using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.ClassMembers;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    /// <summary>
    /// Code template for class
    /// </summary>
    public class CodeClassTemplate : CodeBracedBlock
    {
        // Fields
        
        private string _name = string.Empty;
        private CodeComment _comment;
        private bool _isAbstract;
        
        private List<string> _genericArguments;
        private List<string> _genericRestrictions;
        private List<CodeType> _inheritTypes;
        private List<CodeClassMember> _members;
        
        private CodeAccessModifier _accessModifier = CodeAccessModifier.Private;

        // Properties
        
        private bool IsGeneric => _genericArguments != null && _genericArguments.Count > 0;
        private bool IsInherited => _inheritTypes != null && _inheritTypes.Count > 0;
        private bool IsRestricted => _genericRestrictions != null && _genericRestrictions.Count > 0;
        
        // Methods
        
        public CodeClassTemplate SetName(string name)
        {
            _name = name;
            return this;
        }

        public CodeClassTemplate SetAccess(CodeAccessModifier modifier)
        {
            _accessModifier = modifier;
            return this;
        }

        public CodeClassTemplate SetAbstract(bool isAbstract)
        {
            _isAbstract = isAbstract;
            return this;
        }

        public override CodeBracedBlock AddUnit(CodeTabbable unit)
        {
            if (!(unit is CodeClassMember))
            {
                throw new CodeBuildException("Only class members can be added as units to class");
            }
            
            return base.AddUnit(unit);
        }

        public CodeClassTemplate AddGenericArgument(string name, string restriction = null)
        {
            if (!CodeType.IsValidGenericName(name))
            {
                throw new CodeBuildException("Invalid generic argument name");
            }
            
            if (!IsGeneric)
            {
                _genericArguments = new List<string>();
            }

            if (restriction != null)
            {
                if (_genericRestrictions == null)
                {
                    _genericRestrictions = new List<string>();
                }
                
                _genericRestrictions.Add($"{name}:{restriction}");
            }

            _genericArguments.Add(name);

            return this;
        }

        public CodeClassTemplate InheritFrom(CodeType type)
        {
            if (_inheritTypes == null)
            {
                _inheritTypes = new List<CodeType>();
            }
            
            _inheritTypes.Add(type);

            return this;
        }

        public CodeClassTemplate Comment(CodeComment comment)
        {
            _comment = comment;
            return this;
        }

        public override void Build(ICodeOutput output)
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new CodeBuildException("Class name can't be empty or null string");
            }
            
            output.SetTab(Level);

            WriteHeader(output);

            output.NextLine();
            
            base.Build(output);
        }

        private void WriteHeader(ICodeOutput output)
        {
            if (_comment != null)
            {
                WriteComment(output);
            }

            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);

            if (_isAbstract)
            {
                WriteAbstract(output);
            }

            output.Write(CodeKeywords.Class);
            output.Write(CodeMarkups.Space);
            output.Write(_name);

            if (IsGeneric)
            {
                WriteGeneric(output);
            }

            if (IsInherited)
            {
                WriteInheritance(output);
            }

            if (IsRestricted)
            {
                WriteRestrictions(output);
            }
        }

        private void WriteInheritance(ICodeOutput output)
        {
            output.Write(CodeMarkups.Space);
            output.Write(CodeMarkups.Colon);
            output.Write(CodeMarkups.Space);
            output.Write(_inheritTypes[0]);

            for (int i = 1; i < _inheritTypes.Count; i++)
            {
                output.Write(CodeMarkups.Comma);
                output.Write(CodeMarkups.Space);
                output.Write(_inheritTypes[i]);
            }
        }

        private void WriteRestrictions(ICodeOutput output)
        {
            output.Write(CodeMarkups.Space);
            output.Write(CodeKeywords.Where);
            output.Write(CodeMarkups.Space);
            output.Write(_genericRestrictions[0]);

            for (int i = 1; i < _genericRestrictions.Count; i++)
            {
                output.Write(CodeMarkups.Space);
                output.Write(CodeKeywords.Where);
                output.Write(CodeMarkups.Space);
                output.Write(_genericRestrictions[i]);
            }
        }

        private void WriteComment(ICodeOutput output)
        {
            _comment.Build(output);
            output.NextLine();
        }

        private void WriteGeneric(ICodeOutput output)
        {
            output.Write(CodeMarkups.OpenAngleBracket);
            output.Write(_genericArguments[0]);

            for (int i = 1; i < _genericArguments.Count; i++)
            {
                output.Write(CodeMarkups.Comma);
                output.Write(_genericArguments[i]);
            }

            output.Write(CodeMarkups.CloseAngleBracket);
        }

        private static void WriteAbstract(ICodeOutput output)
        {
            output.Write(CodeKeywords.Abstract);
            output.Write(CodeMarkups.Space);
        }
    }
}