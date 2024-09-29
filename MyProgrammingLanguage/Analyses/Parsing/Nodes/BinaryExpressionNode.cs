namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class BinaryExpressionNode : CompilerNode
{
    public BinaryExpressionNode(CompilerNode left, CompilerNode right, OperatorType operatorType)
    {
        Children.Add(left);
        Children.Add(right);
        Operator = operatorType;
    }

    public OperatorType Operator { get; set; }
}