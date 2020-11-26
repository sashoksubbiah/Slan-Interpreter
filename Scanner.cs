using System;
using System.Collections.Generic;

namespace Slan
{
    internal class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();

        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        internal List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();

            }
            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!': addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;

                case '/':
                    if (match('/'))
                    {
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    line++;
                    break;

                case '"': itsString(); break;

                case 'o': if (peek() == 'r') addToken(TokenType.OR); break;


                default:
                    if (isDigit(c))
                    {
                        number();
                    }
                    else if (isAlpha(c))
                    {
                        identifier();
                    }
                    else
                    {
                        Slan.error(line, "Unexpected character.");
                    }
                    break;

            }
        }


        private static Dictionary<string, TokenType> KeyWords()
        {
            return new Dictionary<string, TokenType>
            {
                { "and", TokenType.AND },
                {"class" , TokenType.CLASS },
                {"else" , TokenType.ELSE },
                {"false"  ,TokenType.FALSE},
                {"for" , TokenType.FOR },
                {"fun" , TokenType.FUN },
                {"if" , TokenType.IF },
                {"null" , TokenType.NULL },
                {"or" , TokenType.OR },
                {"print" , TokenType.PRINT },
                {"return" , TokenType.RETURN },
                {"super" , TokenType.SUPER },
                {"this" , TokenType.THIS },
                {"true" , TokenType.TRUE },
                {"var" , TokenType.VAR },
                {"while" , TokenType.WHILE }
            };
        }




        private void identifier()
        {
            while (isAlphaNumeric(peek())) advance();

            string text = source.Substring(start, current - start);
            TokenType type = KeyWords()[text];
          

            addToken(TokenType.IDENTIFIER);
        }

        private bool isAlphaNumeric(char c) => isAlpha(c) || isDigit(c);


        private bool isAlpha(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == '_');


        private void number()
        {
            while (isDigit(peek())) advance();

            if (peek() == '.' && isDigit(peekNext()))
            {
                advance();
                while (isDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current - start)));

        }

        private char peekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void itsString()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n') line++;
                advance();
            }

            if (isAtEnd())
            {
                Slan.error(line, "Unterminated string.");
                return;
            }

            advance();

            String value = source.Substring(start + 1, (current - start) - 1);
            addToken(TokenType.STRING, value);
        }

        private char peek()
        {
            if (isAtEnd()) return '\0';

            return source[current];
        }

        private bool match(char expected)
        {
            if (isAtEnd()) return false;

            if (source[current] != expected) return false;

            current++;
            return true;

        }

        private char advance()
        {
            current++;
            return source[current - 1];
        }

        private void addToken(TokenType tokenType) => addToken(tokenType, null);


        private void addToken(TokenType tokenType, object literal)
        {
            string text = source.Substring(start, current - start);
            tokens.Add(new Token(tokenType, text, literal, line));
        }

        private bool isAtEnd() => current >= source.Length;

    }
}