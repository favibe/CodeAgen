namespace CodeAgen.Outputs
{
    public interface ICodeOutput
    {
        ICodeOutput SetTab(int level);
        ICodeOutput NextLine();
        ICodeOutput WriteLine(string data);
        ICodeOutput Write(string data);
        ICodeOutput Write(char data);
        ICodeOutput Clear();
    }
}