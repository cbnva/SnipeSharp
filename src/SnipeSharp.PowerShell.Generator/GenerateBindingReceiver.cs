using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SnipeSharp.PowerShell.Generator
{
    internal sealed class GenerateBindingReceiver : ISyntaxContextReceiver
    {
        public List<(INamedTypeSymbol Symbol, string model)> Targets { get; } = new();
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if(context.Node is not ClassDeclarationSyntax node)
                return;
            if(context.SemanticModel.GetDeclaredSymbol(node) is not INamedTypeSymbol symbol)
                return;
            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.GenerateBindingAttribute", out var _))
                return;
            if(!symbol.TryGetInterface("SnipeSharp.PowerShell.IBinding", out var intfc))
                return;
            if(intfc.TypeArguments[0] is not INamedTypeSymbol arg)
                return;
            Targets.Add((symbol, arg.ToDisplayString()));
        }
    }
}
