namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public interface IVisitor<T>
{
    T Visit(AssignmentNode node);
    T Visit(BinaryExpressionNode node);
    T Visit(LiteralExpressionNode node);
    T Visit(IdentifierExpressionNode node);
    T Visit(VariableDeclarationNode node);
    T Visit(PrintNode node);
    T Visit(StartNode node);
}