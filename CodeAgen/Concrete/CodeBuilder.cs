using System.Text;
using CodeAgen.Interfaces;

namespace CodeAgen.Concrete
{
    public class CodeBuilder : ICodeBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private bool _isLineStarted;
        private int _level;
        
        public ICodeBuilder Append(char data)
        {
            if (!_isLineStarted)
            {
                AppendTab();
                _isLineStarted = true;
            }
            
            _stringBuilder.Append(data);
            return this;
        }

        public ICodeBuilder Append(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return this;
            }
            
            if (!_isLineStarted)
            {
                AppendTab();
                _isLineStarted = true;
            }
            
            _stringBuilder.Append(data);
            return this;
        }

        public ICodeBuilder NextLine()
        {
            const char lineBreak = '\n';
            _stringBuilder.Append(lineBreak);
            _isLineStarted = false;

            return this;
        }

        public ICodeBuilder SetTab(int level)
        {
            _level = level;
            return this;
        }

        public ICodeBuilder Clear()
        {
            _stringBuilder.Clear();
            _level = 0;
            _isLineStarted = false;
            
            return this;
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
        
        private void AppendTab()
        {
            const char tab = '\t';
            
            for (var i = 0; i < _level; i++)
            {
                _stringBuilder.Append(tab);
            }
        }
    }
}