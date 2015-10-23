
namespace TigerCompiler
{
    using System;

    /// <summary>
    /// Error EventArgs class.
    /// </summary>
    class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the token that generated the error.
        /// </summary>
        public Token Token { get; set; }

        /// <summary>
        /// Gets or sets the line number the error occurred on.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Gets or sets the offset into the line the error occurred on.
        /// </summary>
        public int LineOffset { get; set; }
    }

    /// <summary>
    /// A lexer will take an input string and convert it into tokens.
    /// </summary>
    class Lexer
    {
        public EventHandler<ErrorEventArgs> ErrorEventHandler { get; set; }

        /// <summary>
        /// Scanner for the input string.
        /// </summary>
        private Scanner scanner;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="input"></param>
        public Lexer(string input)
        {
            scanner = new Scanner(input);
        }

        /// <summary>
        /// Get the next token from the input string.
        /// </summary>
        private Token GetToken()
        {
            Token token;

            scanner.ConsumeWhiteSpace();

            if (Char.IsLetter(scanner.Ch))
            {
                string id = scanner.ReadIdentifier();

                // TODO: Handle keywords a little better.
                if (id == "if")
                {
                    token = new Token(TokenType.If);
                }
                else if (id == "else")
                {
                    token = new Token(TokenType.Else);
                }
                else
                {
                    token = new Token(TokenType.Id, id);
                }
            }
            else if (Char.IsDigit(scanner.Ch))
            {
                token = new Token(TokenType.Num, scanner.ReadNumber());
            }
            else
            {
                switch (scanner.Ch)
                {
                    case '\0':
                        token = new Token(TokenType.Eof);
                        break;
                    case '{':
                        token = new Token(TokenType.LBrace);
                        break;
                    case '}':
                        token = new Token(TokenType.RBrace);
                        break;
                    case '(':
                        token = new Token(TokenType.LParen);
                        break;
                    case ')':
                        token = new Token(TokenType.RParen);
                        break;
                    case ',':
                        token = new Token(TokenType.Comma);
                        break;
                    case ';':
                        token = new Token(TokenType.Semicolon);
                        break;
                    default:

                        token = new Token(TokenType.Unknown);

                        EventHandler<ErrorEventArgs> handler = ErrorEventHandler;

                        if (handler != null)
                        {
                            handler(this, new ErrorEventArgs
                            {
                                Token = token,
                                Line = scanner.Line,
                                LineOffset = scanner.LineOffset
                            });
                        }

                        break;
                }

                scanner.Next();
            }

            Console.WriteLine(token);

            return token;
        }

        /// <summary>
        /// Generate all the tokens for the input string.
        /// </summary>
        public void Tokenize()
        {
            Token token = GetToken();

            while (token != null && token.Type != TokenType.Eof)
            {
                token = GetToken();
            }
        }
    }
}
