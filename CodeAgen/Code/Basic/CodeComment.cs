﻿using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    /// <summary>
    /// Code comment with // before
    /// </summary>
    public sealed class CodeComment : CodeRaw
    {
        private readonly string _content;
        
        public CodeComment(string content)
        {
            _content = content;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.Write(CodeMarkups.Comment);
            output.Write(CodeMarkups.Space);
            output.Write(_content);
        }
    }
}