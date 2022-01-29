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

        public virtual void PreBuild() {}

        public virtual void OnBuild(ICodeBuilder builder)
        {
            builder.Append(Value.ToString());
        }

        public virtual void PostBuild() {}
    }
}