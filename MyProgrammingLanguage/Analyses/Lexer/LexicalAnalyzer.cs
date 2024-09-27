using System.Text.RegularExpressions;

namespace MyProgrammingLanguage.Analyses.Lexer;

public class LexicalAnalyzer
{
    private static readonly Dictionary<string, TokenType> _grammar = new Dictionary<string, TokenType>
    {
        { "^var", TokenType.VarKeyword },
        { "^print", TokenType.PrintKeyword },
        { "^return", TokenType.ReturnKeyword },
        { @"^\+", TokenType.OperatorPlus },
        { "^-", TokenType.OperatorMinus },
        { @"^\*", TokenType.OperatorMultiply },
        { "^/", TokenType.OperatorDivide },
        { "^=", TokenType.OperatorInit },
        { @"^\(", TokenType.LeftParen },
        { @"^\)", TokenType.RightParen },
        { "^,", TokenType.Comma },
        { "^;", TokenType.Semicolon },
        { @"^\{", TokenType.LeftBrace },
        { @"^\}", TokenType.RightBrace },
        { @"^\d+", TokenType.Number },
        { "^\"(.*?)\"", TokenType.String },
        { "^[a-zA-Z_][a-zA-Z0-9_]*", TokenType.Identifier }
    };

    public IEnumerable<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        
        var index = 0;
        while (index < input.Length)
        {
            var currentChar = input[index];
            if (char.IsWhiteSpace(currentChar))
            {
                index++;
                continue;
            }

            var token = GetToken(input.Substring(index));

            if (token == null)
            {
                throw new UnexpectedCharacterException(index, currentChar);   
            }
            
            tokens.Add(token);
            index += token.Value.Length;
        }

        return tokens;
    }

    private Token? GetToken(string input)
    {
        foreach (var (regexPattern, tokenType) in _grammar)
        {
            var regex = new Regex("^" + regexPattern);
            var match = regex.Match(input);

            if (match.Success)
            {
                return new Token(tokenType, match.Value);
            }
        }

        return null;
    }
}