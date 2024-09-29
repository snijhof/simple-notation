namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class StartNode : CompilerNode
{
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}