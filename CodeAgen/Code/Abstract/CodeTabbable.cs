namespace CodeAgen.Code.Abstract
{
    public abstract class CodeTabbable : CodeUnit
    {
        public int Level { get; set; }

        protected override void PostBuild()
        {
            Level = 0;
        }
    }
}