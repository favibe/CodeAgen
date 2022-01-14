using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    /// <summary>
    /// Code entity for class method
    /// </summary>
    public class CodeMethod : CodeBracedBlock, IAbstractable, IGenericable
    {
        public byte Order => 255;
        
        private readonly CodeName _name;
        private readonly CodeAccessModifier _access;
        private readonly CodeType _returnType;
        
        private List<CodeMethodParameter> _parameters;
        private CodeMethodParameter _params;
        
        public bool IsAbstract { get; set; }
        public bool HasParameters => _parameters != null && _parameters.Count > 0;
        public bool HasParams => _params != null;
        public List<CodeName> GenericArguments { get; set; }
        public List<string> GenericRestrictions { get; set; }

        public CodeMethod(CodeNameVar name, CodeType returnType = null, CodeAccessModifier access = null)
        {
            if (access == null)
            {
                access = CodeAccessModifier.Private;
            }

            if (returnType == null)
            {
                returnType = CodeType.Void;
            }

            _access = access;
            _name = name;
            _returnType = returnType;
        }

        /// <summary>
        /// Add parameter to method
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns></returns>
        public CodeMethod AddParameter(CodeMethodParameter parameter)
        {
            if (_parameters == null)
            {
                _parameters = new List<CodeMethodParameter>();
            }
            
            _parameters.Add(parameter);

            return this;
        }
        
        /// <summary>
        /// Add params to method
        /// </summary>
        /// <param name="params">Params</param>
        /// <returns></returns>
        public CodeMethod AddParams(CodeMethodParameter @params)
        {
            _params = @params;
            return this;
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
            output.Write(_access);
            output.Write(CodeMarkups.Space);
            
            if (IsAbstract)
            {
                this.WriteAbstract(output);
            }
            
            output.Write(_returnType);
            output.Write(CodeMarkups.Space);
            
            _name.Build(output);

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
                if (HasParameters)
                {
                    output.Write(CodeMarkups.Comma);
                    output.Write(CodeMarkups.Space);
                }

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
}