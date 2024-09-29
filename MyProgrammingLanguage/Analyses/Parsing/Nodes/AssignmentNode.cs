namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class AssignmentNode : CompilerNode
{
    public AssignmentNode(CompilerNode child)
    {
        Children.Add(child);
    }
}