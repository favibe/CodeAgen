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

        private void PostBuild()
        {
            
        }

        private void PreBuild()
        {
            
        }

        public abstract void OnBuild(ICodeOutput output);
    }
}