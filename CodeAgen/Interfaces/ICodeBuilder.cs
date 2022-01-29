namespace CodeAgen.Interfaces
{
    public interface ICodeBuilder
    {
        ICodeBuilder Append(char data);
        ICodeBuilder Append(string data);
    }
}