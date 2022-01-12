using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    public class CodeClassTemplate : CodeBracedBlock
    {
        private string _name = string.Empty;
        private CodeComment _comment;
        private bool _isAbstract;
        private List<string> _genericArguments;
        private List<string> _genericRestrictions;
        private List<CodeType> _inheritTypes;

        private CodeAccessModifier _accessModifier = CodeAccessModifier.Private;

        private bool IsGeneric => _genericArguments != null && _genericArguments.Count > 0;
        private bool IsInherited => _inheritTypes != null && _inheritTypes.Count > 0;
        private bool IsRestricted => _genericRestrictions != null && _genericRestrictions.Count > 0;
        
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

        public CodeClassTemplate AddGenericArgument(string name, string restriction = null)
        {
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
            output.SetTab(Level);

            if (_comment != null)
            {
                _comment.Build(output);
                output.NextLine();
            }
            
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);

            if (_isAbstract)
            {
                output.Write(CodeKeywords.Abstract);
                output.Write(CodeMarkups.Space);
            }
            
            output.Write(CodeKeywords.Class);
            output.Write(CodeMarkups.Space);
            output.Write(_name);

            if (IsGeneric)
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

            if (IsInherited)
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

            if (IsRestricted)
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
            
            output.NextLine();
            base.Build(output);
        }
    }
}