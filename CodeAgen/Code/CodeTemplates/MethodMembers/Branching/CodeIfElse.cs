using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.MethodMembers.Branching.IfElse;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Branching
{
    public class CodeIfElse : CodeTabbable
    {
        private readonly CodeBracedBlock _if;
        private CodeBracedBlock _else;
        private List<CodeBracedBlock> _elseIfs;

        public CodeIfElse(CodeConditionChain condition, CodeTabbable code)
        {
            _if = new CodeIfBlock(condition, code);
            _if.Parent = this;
        }

        public CodeIfElse ElseIf(CodeConditionChain condition, CodeTabbable code)
        {
            if (_elseIfs == null)
            {
                _elseIfs = new List<CodeBracedBlock>();
            }

            var elseIf = new CodeElseIfBlock(condition, code)
            {
                Parent = this
            };
            
            _elseIfs.Add(elseIf);

            return this;
        }
        
        public CodeIfElse Else(CodeTabbable code)
        {
            _else = new CodeElseBlock(code);
            _else.Parent = this;

            return this;
        }

        protected override void OnBuild(ICodeOutput output)
        {
            _if.Build(output);

            if (_elseIfs != null)
            {
                foreach (var elseIf in _elseIfs)
                {
                    elseIf.Build(output);
                }
            }

            if (_else != null)
            {
                _else.Build(output);
            }
        }

        protected override int GetNextTabLevel() => Level;
    }
}