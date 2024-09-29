using MyProgrammingLanguage.Analyses.Lexer;

namespace MyProgrammingLanguage.Analyses.Parsing;

public class UnexpectedTokenException(Token token) : Exception
{
    
}