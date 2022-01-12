using CodeAgen.Outputs;

namespace CodeAgen.Code.Abstract
{
    public abstract class CodeUnit
    {
        public abstract void Build(ICodeOutput output);
    }
}