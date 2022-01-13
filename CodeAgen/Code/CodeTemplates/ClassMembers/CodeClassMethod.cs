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
        private List<CodeClassMethodParameter> _parameters;
        private CodeClassMethodParameter _params;
        
        // Properties
        
        public bool IsAbstract { get; set; }
        public bool HasParameters => _parameters != null && _parameters.Count > 0;
        public bool HasParams => _params != null;
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

        public CodeClassMethod AddParameter(CodeClassMethodParameter parameter)
        {
            if (_parameters == null)
            {
                _parameters = new List<CodeClassMethodParameter>();
            }
            
            _parameters.Add(parameter);

            return this;
        }

        public CodeClassMethod AddParams(CodeClassMethodParameter parameter)
        {
            _params = parameter;
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

            if (HasParameters)
            {
                _parameters[0].Build(output);
                
                for (int index = 1; index < _parameters.Count; index++)
                {
                    output.Write(CodeMarkups.Comma);
                    output.Write(CodeMarkups.Space);
                    _parameters[index].Build(output);
                }
            }

            if (HasParams)
            {
                output.Write(CodeMarkups.Comma);
                output.Write(CodeMarkups.Space);
                output.Write(CodeKeywords.Params);
                output.Write(CodeMarkups.Space);
                _params.Build(output);
            }
            
            output.Write(CodeMarkups.CloseBracket);

            if (this.HasRestrictions())
            {
                this.WriteRestrictions(output);
            }
        }
    }

    public class CodeClassMethodParameter : CodeRaw
    {
        private readonly string _name;
        private readonly CodeType _type;
        private readonly string _defaultValue;
        public bool HasDefaultValue => _defaultValue != null;
        
        public CodeClassMethodParameter(string name, CodeType type, string defaultValue = null)
        {
            _name = name;
            _type = type;
            _defaultValue = defaultValue;
        }
        
        public override void Build(ICodeOutput output)
        {
            output.Write(_type);
            output.Write(CodeMarkups.Space);
            output.Write(_name);

            if (!HasDefaultValue)
            {
                return;
            }
            
            output.Write(CodeMarkups.Assignment);
            output.Write(_defaultValue);
        }
    }
}