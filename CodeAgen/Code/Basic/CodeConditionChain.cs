using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public class CodeConditionChain : CodeRaw
    {
        private static readonly CodeRawString AndSign = new CodeRawString("&&");
        private static readonly CodeRawString OrSign = new CodeRawString("||");
        
        private readonly List<CodeUnit> _chain;

        public CodeConditionChain(CodeUnit condition)
        {
            _chain = new List<CodeUnit>
            {
                condition
            };
        }

        public CodeConditionChain And(CodeUnit condition)
        {
            _chain.Add(AndSign);
            _chain.Add(condition);

            return this;
        }

        public CodeConditionChain Or(CodeUnit condition)
        {
            _chain.Add(OrSign);
            _chain.Add(condition);

            return this;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            _chain[0].Build(output);

            for (int i = 1; i < _chain.Count; i++)
            {
                output.Write(CodeMarkups.Space);
                _chain[i].Build(output);
            }
        }
        
        public static implicit operator CodeConditionChain(string @string)
        {
            return new CodeConditionChain(new CodeRawString(@string));
        }
    }
}