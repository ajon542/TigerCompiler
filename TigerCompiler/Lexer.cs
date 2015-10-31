
namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

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
        public int Column { get; set; }
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
                    token = new Token(TokenType.If, scanner.Line, scanner.Column);
                }
                else if (id == "else")
                {
                    token = new Token(TokenType.Else, scanner.Line, scanner.Column);
                }
                else
                {
                    token = new Token(TokenType.Id, scanner.Line, scanner.Column, id);
                }
            }
            else if (Char.IsDigit(scanner.Ch))
            {
                token = new Token(TokenType.Num, scanner.Line, scanner.Column, scanner.ReadNumber());
            }
            else
            {
                switch (scanner.Ch)
                {
                    case '\0':
                        token = new Token(TokenType.Eof, scanner.Line, scanner.Column);
                        break;
                    case '{':
                        token = new Token(TokenType.LBrace, scanner.Line, scanner.Column);
                        break;
                    case '}':
                        token = new Token(TokenType.RBrace, scanner.Line, scanner.Column);
                        break;
                    case '(':
                        token = new Token(TokenType.LParen, scanner.Line, scanner.Column);
                        break;
                    case ')':
                        token = new Token(TokenType.RParen, scanner.Line, scanner.Column);
                        break;
                    case ',':
                        token = new Token(TokenType.Comma, scanner.Line, scanner.Column);
                        break;
                    case ';':
                        token = new Token(TokenType.Semicolon, scanner.Line, scanner.Column);
                        break;
                    default:
                        token = new Token(TokenType.Unknown, scanner.Line, scanner.Column);
                        Error(token);
                        break;
                }

                scanner.Next();
            }

            return token;
        }

        /// <summary>
        /// Call the error handler.
        /// </summary>
        /// <param name="token">The token causing the error.</param>
        private void Error(Token token)
        {
            EventHandler<ErrorEventArgs> handler = ErrorEventHandler;

            if (handler != null)
            {
                handler(this, new ErrorEventArgs
                {
                    Token = token,
                    Line = token.Line,
                    Column = token.Column
                });
            }
        }

        /// <summary>
        /// Generate all the tokens for the input string.
        /// </summary>
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token token = GetToken();
            tokens.Add(token);

            while (token.Type != TokenType.Eof)
            {
                token = GetToken();
                tokens.Add(token);
            }

            return tokens;
        }
    }
}
