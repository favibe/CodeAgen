using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    /// <summary>
    /// Property unit for code class
    /// </summary>
    public sealed class CodeProperty : CodeBracedBlock
    {
        public byte Order => 1;
        
        private readonly CodeName _name;
        private readonly CodeType _type;
        private readonly CodeAccessModifier _access;

        private bool _hasGetter;
        private bool _hasSetter;

        public CodeProperty(
            CodeNameVar name, CodeType type,
            CodeAccessModifier access = null
            )
        {
            _name = name;
            _type = type;

            if (access == null)
            {
                access = CodeAccessModifier.Public;
            }
            
            _access = access;
        }

        public void AddGetter(CodeTabbable code = null, CodeAccessModifier access = null)
        {
            if (_hasGetter)
            {
                throw new CodeBuildException("Property can't have more then one getter");
            }

            if (code == null)
            {
                AddAuto("get", access);
            }
            else
            {
                AddBlock(code, "get", access);
                _hasGetter = true;
            }
        }

        public void AddSetter(CodeTabbable code = null, CodeAccessModifier access = null)
        {
            if (_hasSetter)
            {
                throw new CodeBuildException("Property can't have more then one setter");
            }
            
            if (code == null)
            {
                AddAuto("set", access);
            }
            else
            {
                AddBlock(code, "set", access);
                _hasSetter = true;
            }
        }
        
        private void AddBlock(CodeTabbable code, string label, CodeAccessModifier codeAccessModifier)
        {
            var header = new CodeLine();
            
            if (codeAccessModifier != null)
            {
                header.AddUnit(new CodeRawString(codeAccessModifier));
                header.AddUnit(new CodeRawChar(CodeMarkups.Space));
            }

            header.AddUnit(new CodeRawString(label));
            header.SetSemicolon(false);

            var block = new CodeBracedBlock();
            block.AddUnit(code);
            code.Level = Level + 2;

            AddUnit(header);
            AddUnit(block);
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            
            output.Write(_access);
            output.Write(CodeMarkups.Space);
            _type.Build(output);
            output.Write(CodeMarkups.Space);
            _name.Build(output);
            output.NextLine();
            base.OnBuild(output);
        }

        private void AddAuto(string label, CodeAccessModifier access)
        {
            var header = new CodeLine();

            header.AddUnit(access != null
                ? new CodeRawString($"{access} {label}")
                : new CodeRawString(label));

            AddUnit(header);
        }
    }
}