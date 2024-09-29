namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class LiteralExpressionNode(object value, VariableType type) : CompilerNode
{
    public object Value { get; } = value;
    public VariableType Type { get; } = type;
}