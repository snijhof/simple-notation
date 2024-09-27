namespace MyProgrammingLanguage.Analyses.Lexer;

public record Token(TokenType Type, string Value)
{
    public override string ToString()
    {
        return $"Token(Type: {Type}, Value: '{Value}')";
    }
}