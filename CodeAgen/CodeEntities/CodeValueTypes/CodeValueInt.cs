using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities.CodeValueTypes
{
    public sealed class CodeValueInt : CodeValue<int>
    {
        public CodeValueInt(int value) : base(value)
        {
        }

        public override void OnBuild(ICodeBuilder builder)
        {
            builder.Append(Value.ToString());
        }
    }
}