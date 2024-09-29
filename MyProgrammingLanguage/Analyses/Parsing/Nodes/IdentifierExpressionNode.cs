namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class IdentifierExpressionNode(string name) : CompilerNode
{
    public string Name { get; set; } = name;
    
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}