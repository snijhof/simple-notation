namespace MyProgrammingLanguage.Analyses.Lexer;

public class UnexpectedCharacterException(int position, char character)
    : Exception($"Unexpected character at position {position}: '{character}'");