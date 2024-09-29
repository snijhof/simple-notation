using MyProgrammingLanguage.Analyses.Lexer;
using MyProgrammingLanguage.Analyses.Parsing.Nodes;

namespace MyProgrammingLanguage.Analyses.Parsing;

public class Parser(List<Token> tokens, int index = 0)
{
    public AbstractSyntaxTree? Parse()
    {
        if (tokens.Any() is false)
        {
            return null;
        }

        var rootCompilerNode = new StartNode();
        
        while (index < tokens.Count)
        {
            var node = ParseStatement();
            rootCompilerNode.AddChild(node);
        }
        
        return new AbstractSyntaxTree
        {
            Root = rootCompilerNode
        };
    }

    private CompilerNode ParseStatement()
    {
        var token = tokens[index];
        
        switch (token.Type)
        {
            case TokenType.VarKeyword:
                return ParseVariableDeclaration();
            case TokenType.OperatorInit:
                return ParseAssignment();
            case TokenType.PrintKeyword:
                return ParsePrint();
            default:
                throw new UnexpectedTokenException(token);
        }
    }
    
    private CompilerNode ParseVariableDeclaration()
    {
        Consume(TokenType.VarKeyword);
        
        var identifierToken = tokens[index++];
        var assignmentCompilerNode = ParseAssignment();
        var variableDeclarationNode = new VariableDeclarationNode(identifierToken.Value, assignmentCompilerNode);

        return variableDeclarationNode;
    }

    private CompilerNode ParseAssignment()
    {
        Consume(TokenType.OperatorInit);
        
        var expressionCompilerNode = ParseExpression();
        var assignmentNode = new AssignmentNode(expressionCompilerNode);

        Consume(TokenType.EndOfLine);

        return assignmentNode;
    }

    private CompilerNode ParseExpression()
    {
        if (IsBinaryExpressionOperator(tokens[index + 1]))
        {
            return ParseBinaryExpression();
        }

        return ParseLiteralExpression();
    }

    private CompilerNode ParseLiteralExpression()
    {
        var token = tokens[index++];
        switch (token.Type)
        {
            case TokenType.String:
                return new LiteralExpressionNode(token.Value.Trim('"'), VariableType.String);
            case TokenType.Number:
                return new LiteralExpressionNode(int.Parse(token.Value), VariableType.Number);
            case TokenType.Identifier:
                return new LiteralExpressionNode(token.Value, VariableType.Identifier);
            default:
                throw new UnexpectedTokenException(token);
        }
    }
    
    private CompilerNode ParsePrint()
    {
        Consume(TokenType.PrintKeyword);
        Consume(TokenType.LeftParen);
        
        var expressionCompilerNode = ParseExpression();
        var printNode = new PrintNode(expressionCompilerNode);

        Consume(TokenType.RightParen);
        Consume(TokenType.EndOfLine);

        return printNode;
    }
    
    private CompilerNode ParseBinaryExpression()
    {
        var leftExpression = ParseLiteralExpression();
        var operatorType = GetOperatorType(tokens[index++]);
        var rightExpression = ParseLiteralExpression();
        
        return new BinaryExpressionNode(leftExpression, rightExpression, operatorType);
    }
    
    private void Consume(TokenType type)
    {
        if (tokens[index++].Type != type)
        {
            throw new UnexpectedTokenException(tokens[index]);
        }
    }
    
    private static OperatorType GetOperatorType(Token token)
    {
        return token.Type switch
        {
            TokenType.OperatorPlus => OperatorType.PlusOperator,
            TokenType.OperatorMinus => OperatorType.MinusOperator,
            TokenType.OperatorMultiply => OperatorType.MultiplyOperator,
            TokenType.OperatorDivide => OperatorType.DivideOperator,
            _ => throw new UnexpectedTokenException(token)
        };
    }
    
    private static bool IsBinaryExpressionOperator(Token token)
    {
        return token.Type is TokenType.OperatorPlus 
            or TokenType.OperatorMinus 
            or TokenType.OperatorMultiply 
            or TokenType.OperatorDivide;
    }
}