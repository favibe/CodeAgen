namespace CodeAgen.Interfaces
{
    public interface ICode
    {
        void PreBuild();
        void OnBuild(ICodeBuilder builder);
        void PostBuild();
    }
}