
namespace TigerCompiler
{
    using System;
    using System.Text;

    class Lexer
    {
        private Scanner scanner;

        public Lexer(string input)
        {
            scanner = new Scanner(input);
        }

        private void GetToken(out Token token)
        {
            token = new Token(TokenType.Unknown);

            scanner.ConsumeWhiteSpace();

            if (Char.IsLetter(scanner.Ch))
            {
                token = new Token(TokenType.Id, scanner.ReadIdentifier());
            }
            else if (Char.IsDigit(scanner.Ch))
            {
                token = new Token(TokenType.Num, scanner.ReadNumber());
            }
            else
            {
                if (scanner.Ch == '\0')
                {
                    token = new Token(TokenType.Eof);
                }
                else if (scanner.Ch == '{')
                {
                    token = new Token(TokenType.LBrace);
                }
                else if (scanner.Ch == '}')
                {
                    token = new Token(TokenType.RBrace);
                }
                else if (scanner.Ch == '(')
                {
                    token = new Token(TokenType.LParen);
                }
                else if (scanner.Ch == ')')
                {
                    token = new Token(TokenType.RParen);
                }
                else if (scanner.Ch == ',')
                {
                    token = new Token(TokenType.Comma);
                }
                else if (scanner.Ch == ';')
                {
                    token = new Token(TokenType.Semicolon);
                }
                else
                {
                    Console.WriteLine("Unknown token {0} at line: {1}, position: {2}", scanner.Ch, scanner.Line, scanner.LineOffset);
                }

                scanner.Next();
            }

            Console.WriteLine(token);
        }

        public void Tokenize()
        {
            Token token;
            GetToken(out token);

            while (token.Type != TokenType.Eof)
            {
                GetToken(out token);
            }
        }
    }
}
