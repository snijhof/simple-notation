namespace MyProgrammingLanguage.Analyses.Lexer;

public enum TokenType
{
    VarKeyword,
    PrintKeyword,
    ReturnKeyword,
    Identifier,
    OperatorPlus,
    OperatorMinus,
    OperatorDivide,
    OperatorMultiply,
    OperatorInit,
    LeftParen,
    RightParen,
    Number,
    Comma,
    EndOfLine,
    LeftBrace,
    RightBrace,
    String
}