using System.Text;

namespace CodeAgen.Outputs.Entities
{
    public class StandardCodeOutput : ICodeOutput
    {
        private const string NewLine = "\r\n";
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private int _symbolsInLine;
        private int _tabLevel;
        
        public ICodeOutput SetTab(int level)
        {
            if (_symbolsInLine == 0)
            {
                if (_stringBuilder.Length > 0)
                {
                    _stringBuilder.Length -= _tabLevel;
                }

                _tabLevel = level;
                WriteTab();
                
                return this;
            }
            
            _tabLevel = level;

            return this;
        }

        public ICodeOutput NextLine()
        {
            _stringBuilder.Append(NewLine);
            _symbolsInLine = 0;

            WriteTab();
            
            return this;
        }

        private void WriteTab()
        {
            for (int i = 0; i < _tabLevel; i++)
            {
                _stringBuilder.Append('\t');
            }
        }

        public ICodeOutput WriteLine(string data)
        {
            _stringBuilder.AppendLine(data);
            return this;
        }

        public ICodeOutput Write(string data)
        {
            _symbolsInLine += data.Length;
            _stringBuilder.Append(data);
            return this;
        }

        public ICodeOutput Write(char data)
        {
            _symbolsInLine++;
            _stringBuilder.Append(data);
            return this;
        }

        public ICodeOutput Clear()
        {
            _symbolsInLine = 0;
            _stringBuilder.Clear();
            return this;
        }

        public override string ToString()
        {
            if (_symbolsInLine == 0 && _stringBuilder.Length > 0)
            {
                _stringBuilder.Length -= _tabLevel;
            }
            
            return _stringBuilder.ToString();
        }
    }
}