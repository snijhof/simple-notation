namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class LiteralExpressionNode(object value, VariableType type) : CompilerNode
{
    public object Value { get; } = value;
    public VariableType Type { get; } = type;
    
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}