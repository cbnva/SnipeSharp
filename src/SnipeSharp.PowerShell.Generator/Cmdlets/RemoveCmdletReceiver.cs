using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SnipeSharp.PowerShell.Generator
{
    internal sealed class RemoveCmdletReceiver : ISyntaxContextReceiver
    {
        public List<CmdletTarget> Targets { get; } = new();
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if(context.Node is not ClassDeclarationSyntax node)
                return;
            if(context.SemanticModel.GetDeclaredSymbol(node) is not INamedTypeSymbol symbol)
                return;
            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.GenerateRemoveCmdletAttribute", out var _))
                return;
            if(!symbol.TryGetInterface("SnipeSharp.PowerShell.IBinding", out var intfc))
                return;
            if(intfc.TypeArguments[0] is not INamedTypeSymbol arg)
                return;
            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.AssociatedEndPointAttribute", out var data))
                return;
            if(!data.TryGetOption(0, out var propertyNameConstant) || propertyNameConstant.IsNull)
                return;
            Targets.Add(new CmdletTarget(arg.Name, symbol.ToDisplayString(), arg.ToDisplayString(), propertyNameConstant.Value!.ToString()));
        }
    }
}
