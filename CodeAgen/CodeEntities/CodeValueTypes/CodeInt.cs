using CodeAgen.Interfaces;

namespace CodeAgen.CodeEntities.CodeValueTypes
{
    public sealed class CodeInt : CodeValue<int>
    {
        public CodeInt(int value) : base(value)
        {
        }
    }
}