namespace MyProgrammingLanguage.Analyses.Parsing.Nodes;

public abstract class CompilerNode
{
    public ICollection<CompilerNode> Children { get; private set; } = [];
    
    public void AddChild(CompilerNode child)
    {
        Children.Add(child);
    }
}