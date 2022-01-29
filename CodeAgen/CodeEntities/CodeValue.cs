using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities
{
    public abstract class CodeValue<T> : ICode
    {
        public readonly T Value;

        protected CodeValue(T value)
        {
            Value = value;
        }

        public void PreBuild() {}

        public abstract void OnBuild(ICodeBuilder builder);

        public void PostBuild() {}
    }
}