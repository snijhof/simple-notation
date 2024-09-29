namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class PrintNode : CompilerNode
{
    public PrintNode(CompilerNode expression)
    {
        Children.Add(expression);
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}