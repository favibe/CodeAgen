using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities.CodeValueTypes
{
    public sealed class CodeValueBoolean : CodeValue<bool>
    {
        public CodeValueBoolean(bool value) : base(value)
        {
        }

        public override void OnBuild(ICodeBuilder builder)
        {
            builder.Append(Value.ToString());
        }
    }
}