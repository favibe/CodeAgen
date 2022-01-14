﻿using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
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
        
        private List<CodeClassParameter> _parameters;
        private CodeClassParameter _params;
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
        
        public CodeClassConstructor AddParameter(CodeClassParameter parameter)
        {
            if (_parameters == null)
            {
                _parameters = new List<CodeClassParameter>();
            }
            
            _parameters.Add(parameter);

            return this;
        }

        public CodeClassConstructor AddParams(CodeClassParameter @params)
        {
            _params = @params;
            return this;
        }

        public CodeClassConstructor InheritFromBase(
            params CodeNameVar[] parameters)
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

            @base.AddUnit(new CodeRawChar(CodeMarkups.CloseBracket));

            return this;
        }

        protected override void OnBuild(ICodeOutput output)
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
            
            base.OnBuild(output);
        }

        public static CodeClassConstructor CreateFor(CodeClass @class, CodeAccessModifier access = null)
        {
            return new CodeClassConstructor(@class.Name, access);
        }

        public byte Order => 5;
    }
}