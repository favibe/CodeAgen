using System.Globalization;
using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities.CodeValueTypes
{
    public sealed class CodeValueFloat : CodeValue<float>
    {
        public CodeValueFloat(float value) : base(value)
        {
        }

        public override void OnBuild(ICodeBuilder builder)
        {
            builder.Append($"{Value.ToString(CultureInfo.InvariantCulture)}f");
        }
    }
}