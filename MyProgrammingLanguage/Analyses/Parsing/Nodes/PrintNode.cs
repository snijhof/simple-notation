namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class PrintNode : CompilerNode
{
    public PrintNode(CompilerNode expression)
    {
        Children.Add(expression);
    }
}