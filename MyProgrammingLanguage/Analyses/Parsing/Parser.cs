using MyProgrammingLanguage.Analyses.Lexer;

namespace MyProgrammingLanguage.Analyses.Parsing;

public class Parser
{
    public AbstractSyntaxTree Parse(IEnumerable<Token> tokens)
    {
        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.VarKeyword:
                    break;
                case TokenType.PrintKeyword:
                    break;
                case TokenType.ReturnKeyword:
                    break;
                case TokenType.Identifier:
                    break;
                case TokenType.Number:
                    break;
                case TokenType.String:
                    break;
                case TokenType.LeftParen:
                    break;
                case TokenType.OperatorPlus:
                case TokenType.OperatorMinus:
                case TokenType.OperatorMultiply:
                case TokenType.OperatorDivide:
                    break;
                case TokenType.LeftBrace:
                    break;
                case TokenType.RightBrace:
                    break;
                default:
                    throw new IndexOutOfRangeException(token.Type.ToString());
            }
        }

        return null;
    }
}

public class VariableDeclarationNode(CompilerNode parent, string name, CompilerNode value)
    : CompilerNode(parent);

public class AssignmentNode(CompilerNode parent, string name, CompilerNode value)
    : CompilerNode(parent);

public class LiteralExpressionNode(CompilerNode parent, object value, VariableType type)
    : CompilerNode(parent);

public class PrintNode(CompilerNode parent, CompilerNode expression)
    : CompilerNode(parent);