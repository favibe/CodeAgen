using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public sealed class CodeClassProperty : CodeBracedBlock, ICodeClassMember
    {
        public byte Order => 1;
        
        private readonly string _name;
        private readonly CodeType _type;
        private readonly CodeAccessModifier _accessModifier;

        private bool _hasGetter;
        private bool _hasSetter;

        public CodeClassProperty(
            string name, CodeType type,
            CodeAccessModifier accessModifier = null
            )
        {
            _name = name;
            _type = type;

            if (accessModifier == null)
            {
                accessModifier = CodeAccessModifier.Public;
            }
            
            _accessModifier = accessModifier;
        }

        public void AddGetter(CodeTabbable code = null, CodeAccessModifier accessModifier = null)
        {
            if (_hasGetter)
            {
                throw new CodeBuildException("Property can't have more then one getter");
            }

            if (code == null)
            {
                AddAuto("get", accessModifier);
            }
            else
            {
                AddBlock(code, "get", accessModifier);
                _hasGetter = true;
            }
        }

        public void AddSetter(CodeTabbable code = null, CodeAccessModifier accessModifier = null)
        {
            if (_hasSetter)
            {
                throw new CodeBuildException("Property can't have more then one setter");
            }
            
            if (code == null)
            {
                AddAuto("set", accessModifier);
            }
            else
            {
                AddBlock(code, "set", accessModifier);
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
        
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);
            
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            output.Write(_type);
            output.Write(CodeMarkups.Space);
            output.Write(_name);
            output.NextLine();
            base.Build(output);
        }

        private void AddAuto(string label, CodeAccessModifier accessModifier)
        {
            var header = new CodeLine();

            header.AddUnit(accessModifier != null
                ? new CodeRawString($"{accessModifier} {label}")
                : new CodeRawString(label));

            AddUnit(header);
        }
    }
}