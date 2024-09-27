namespace MyProgrammingLanguage.Analyses.Parsing;

public abstract class CompilerNode(CompilerNode parent)
{
    public IEnumerable<CompilerNode> Children { get; set; } = [];
}