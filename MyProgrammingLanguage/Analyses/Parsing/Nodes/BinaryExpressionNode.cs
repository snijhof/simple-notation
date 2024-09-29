namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class BinaryExpressionNode : CompilerNode
{
    public BinaryExpressionNode(CompilerNode left, CompilerNode right, OperatorType operatorType)
    {
        Operator = operatorType;
        Left = left;
        Right = right;
        
        Children.Add(left);
        Children.Add(right);
    }

    public CompilerNode Left { get; set; }
    public CompilerNode Right { get; set; }
    public OperatorType Operator { get; set; }
    
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}