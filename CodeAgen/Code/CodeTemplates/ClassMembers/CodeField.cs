﻿using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    /// <summary>
    /// Code unit for class field
    /// </summary>
    public class CodeField : CodeTabbable
    {
        public byte Order => 0;
        
        private readonly CodeType _type;
        private readonly CodeName _name;
        private readonly string _value;
        private readonly CodeAccessModifier _access;
        private readonly bool _isReadonly;

        public CodeField(CodeType type, CodeNameVar name, string value = null, CodeAccessModifier access = null, bool isReadonly = false)
        {
            if (access == null)
            {
                access = CodeAccessModifier.Private;
            }

            _type = type;
            _name = name;
            _value = value;
            _access = access;
            _isReadonly = isReadonly;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(_access);
            output.Write(CodeMarkups.Space);
            
            if (_isReadonly)
            {
                output.Write(CodeKeywords.Readonly);
                output.Write(CodeMarkups.Space);
            }
            
            _type.Build(output);
            output.Write(CodeMarkups.Space);
            
            _name.Build(output);

            if (!string.IsNullOrWhiteSpace(_value))
            {
                output.Write(CodeMarkups.Assignment);
                output.Write(_value);
            }

            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}