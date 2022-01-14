using CodeAgen.Outputs;

namespace CodeAgen.Code.Abstract
{
    public abstract class CodeUnit
    {
        public void Build(ICodeOutput output)
        {
            PreBuild();
            OnBuild(output);
            PostBuild();
        }

        protected virtual void PostBuild()
        {
            
        }

        protected virtual void PreBuild()
        {
            
        }

        protected abstract void OnBuild(ICodeOutput output);
    }
}