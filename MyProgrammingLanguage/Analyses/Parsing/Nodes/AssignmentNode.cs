namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class AssignmentNode : CompilerNode
{
    public AssignmentNode(CompilerNode child)
    {
        Children.Add(child);
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}