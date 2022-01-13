using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassConstructor : CodeBracedBlock, ICodeClassMember
    {
        private readonly CodeName _className;
        private readonly CodeAccessModifier _access;
        
        private List<CodeClassMethodParameter> _parameters;
        private CodeClassMethodParameter _params;
        private CodeFragment _inheritance;
        
        public bool HasParams => _params != null;
        public bool HasParameters => _parameters != null && _parameters.Count > 0;

        private CodeClassConstructor(CodeName className, CodeAccessModifier access = null)
        {
            if (access == null)
            {
                access = CodeAccessModifier.Private;
            }
            
            _className = className;
            _access = access;
        }
        
        public CodeClassConstructor AddParameter(CodeClassMethodParameter parameter)
        {
            if (_parameters == null)
            {
                _parameters = new List<CodeClassMethodParameter>();
            }
            
            _parameters.Add(parameter);

            return this;
        }

        public CodeClassConstructor AddParams(CodeClassMethodParameter @params)
        {
            _params = @params;
            return this;
        }

        public CodeClassConstructor InheritFromBase(
            CodeClassMethodParameter[] parameters = null,
            CodeClassMethodParameter @params = null)
        {
            var @base = new CodeFragment();
            _inheritance = @base;

            @base.AddUnit(new CodeRawChar(CodeMarkups.Colon));
            @base.AddUnit(new CodeRawString(CodeKeywords.Base));
            @base.AddUnit(new CodeRawChar(CodeMarkups.OpenBracket));

            var hasParameters = parameters != null && parameters.Length > 0;
            
            if (hasParameters)
            {
                @base.AddUnit(parameters[0]);

                for (var i = 1; i < parameters.Length; i++)
                {
                    @base.AddUnit(new CodeRawChar(CodeMarkups.Comma));
                    @base.AddUnit(parameters[i]);
                }
            }

            if (@params != null)
            {
                if (hasParameters)
                {
                    @base.AddUnit(new CodeRawChar(CodeMarkups.Comma));
                }

                @base.AddUnit(new CodeRawString($"{CodeKeywords.Params} "));
                @base.AddUnit(@params);
            }

            @base.AddUnit(new CodeRawChar(CodeMarkups.CloseBracket));

            return this;
        }

        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_access);
            output.Write(CodeMarkups.Space);
            
            _className.Build(output);
            
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

            _inheritance?.Build(output);

            output.NextLine();
            
            base.Build(output);
        }

        public static CodeClassConstructor CreateFor(CodeClass @class, CodeAccessModifier access = null)
        {
            return new CodeClassConstructor(@class.Name, access);
        }

        public byte Order => 5;
    }
}