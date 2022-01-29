using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities.CodeValueTypes
{
    public sealed class CodeBoolean : CodeValue<bool>
    {
        public CodeBoolean(bool value) : base(value)
        {
        }

        public override void OnBuild(ICodeBuilder builder)
        {
            builder.Append(Value.ToString());
        }
    }
}