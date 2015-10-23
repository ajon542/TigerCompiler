
namespace TigerCompiler
{
    using System.Text;

    enum TokenType
    {
        Unknown,
        Id,
        Num,
        Eof,
        LBrace,
        RBrace,
        LParen,
        RParen,
        Comma,
        Semicolon
    }

    class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value = "")
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", Type);
            if (Value != string.Empty)
            {
                sb.AppendFormat("({0})", Value);
            }

            return sb.ToString();
        }
    }
}
