namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public class VariableDeclarationNode : CompilerNode
{
    public string Name { get; }

    public VariableDeclarationNode(string name, CompilerNode child)
    {
        Name = name;
        Children.Add(child);
    }
}