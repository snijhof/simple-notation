using System.Reflection;
using System.Reflection.Emit;
using MyProgrammingLanguage.Analyses.Parsing;
using MyProgrammingLanguage.Analyses.Parsing.Nodes;

namespace MyProgrammingLanguage.Compiling;

public class Compiler : IVisitor<object>
{
    private string latestVariableName;
    private readonly Dictionary<string, LocalBuilder> _locals = new Dictionary<string, LocalBuilder>();
    private ILGenerator _ilGenerator;

    public Compiler(ILGenerator ilGenerator)
    {
        _ilGenerator = ilGenerator;
    }

    public void Compile(AbstractSyntaxTree ast)
    {
        var x = ast.Root.Accept(this);
        _ilGenerator.Emit(OpCodes.Ret);
    }

    public object Visit(AssignmentNode node)
    {
        var result = node.Children.First().Accept(this);

        if (_locals.TryGetValue(latestVariableName, out var builder))
        {
            _ilGenerator.Emit(OpCodes.Stloc, builder);
        }
        else
        {
            throw new Exception($"Variable '{latestVariableName}' not declared.");
        }

        return null;
    }

    public object Visit(BinaryExpressionNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (node.Operator == OperatorType.PlusOperator)
        {
            _ilGenerator.Emit(OpCodes.Add);
        }
        else if (node.Operator == OperatorType.MinusOperator)
        {
            _ilGenerator.Emit(OpCodes.Sub);
        }
        else if (node.Operator == OperatorType.MultiplyOperator)
        {
            _ilGenerator.Emit(OpCodes.Mul);
        }
        else if (node.Operator == OperatorType.DivideOperator)
        {
            _ilGenerator.Emit(OpCodes.Div);
        }
        
        return null;
    }

    public object Visit(LiteralExpressionNode node)
    {
        if (node.Type == VariableType.Number)
        {
            _ilGenerator.Emit(OpCodes.Ldc_I4, (int)node.Value);
        }
        else if (node.Type == VariableType.String)
        {
            _ilGenerator.Emit(OpCodes.Ldstr, (string)node.Value);
        }
        
        return null;
    }

    public object Visit(IdentifierExpressionNode node)
    {
        if (_locals.TryGetValue(node.Name, out var builder) is false)
        {
            throw new Exception($"Local variable \"{node.Name}\" not found.");
        }
        
        if (builder.LocalType == typeof(int))
        {
            _ilGenerator.Emit(OpCodes.Ldc_I4, builder);   
        }
        else if (builder.LocalType == typeof(string))
        {
            _ilGenerator.Emit(OpCodes.Ldstr, builder);   
        }

        return null;
    }

    // TODO: For now variables can only be set by LiteralExpressionNodes
    public object Visit(VariableDeclarationNode node)
    {
        var variableType = node.Children
            .OfType<AssignmentNode>()
            .First()
            .Children
            .OfType<LiteralExpressionNode>()
            .First()
            .Type;
        
        var localBuilder = _ilGenerator.DeclareLocal(variableType == VariableType.Number ? typeof(int) : typeof(string));
        _locals[node.Name] = localBuilder;
        latestVariableName = node.Name;
        
        _ = node.Children
            .OfType<AssignmentNode>()
            .First()
            .Accept(this);
        
        _ilGenerator.Emit(OpCodes.Stloc, localBuilder);

        return null;
    }

    public object Visit(PrintNode node)
    {
        // Visit the expression to print
        var variableType = GetTypeOfLiteralExpressionNode(node);
        if (variableType == null)
        {
            variableType = GetTypeOfIdentifierExpressionNode(node);
        }
        
        _ = node.Children.First().Accept(this);
        
        // Emit the IL to call Console.WriteLine(int)
        MethodInfo writeLine = typeof(Console).GetMethod("WriteLine", new[] { variableType });
        _ilGenerator.Emit(OpCodes.Call, writeLine);

        return null;
    }

    private Type? GetTypeOfLiteralExpressionNode(CompilerNode node)
    {
        if (node.Children.Any(x => x is LiteralExpressionNode))
        {
            return node.Children.OfType<LiteralExpressionNode>().First().Type == VariableType.Number
                ? typeof(int)
                : typeof(string);
        }

        return node.Children
            .Select(GetTypeOfLiteralExpressionNode)
            .FirstOrDefault();
    }

    private Type? GetTypeOfIdentifierExpressionNode(CompilerNode node)
    {
        if (node.Children.Any(x => x is IdentifierExpressionNode))
        {
            return _locals[node.Children.OfType<IdentifierExpressionNode>().First().Name].LocalType;
        }

        return node.Children
            .Select(GetTypeOfIdentifierExpressionNode)
            .FirstOrDefault();
    }
    
    public object Visit(StartNode node)
    {
        // Visit each child node (statements in the program)
        foreach (var child in node.Children)
        {
            _ = child.Accept(this);
        }

        return nameof(StartNode);
    }
}