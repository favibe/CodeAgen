namespace CodeAgen.Interfaces
{
    public interface ICodeEnvironment
    {
        ICodeTypeProvider TypeProvider { get; }

        void Compile(ICode code);
    }
}