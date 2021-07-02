using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SnipeSharp.PowerShell.Generator
{
    internal sealed class FindCmdletReceiver : ISyntaxContextReceiver
    {
        public Dictionary<string, string> ModelBindingMap { get; } = new();
        public List<FindCmdletTarget> Targets { get; } = new();
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if(context.Node is not ClassDeclarationSyntax node)
                return;
            if(context.SemanticModel.GetDeclaredSymbol(node) is not INamedTypeSymbol symbol)
                return;
            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.GenerateFindCmdletAttribute", out var attr))
                return;
            if(!symbol.TryGetInterface("SnipeSharp.PowerShell.IBinding", out var intfc))
                return;
            if(intfc.TypeArguments[0] is not INamedTypeSymbol modelType)
                return;
            ModelBindingMap[modelType.ToDisplayString()] = symbol.ToDisplayString();
            if(!attr.TryGetOption(0, out var filterType) || filterType.IsNull)
                return;
            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.AssociatedEndPointAttribute", out var data))
                return;
            if(!data.TryGetOption(0, out var propertyNameConstant) || propertyNameConstant.IsNull)
                return;
            Targets.Add(new FindCmdletTarget(propertyNameConstant.Value!.ToString(), symbol, modelType, filterType));
        }
    }

    internal sealed class FindCmdletTarget
    {
        private static readonly HashSet<string> IgnoredPropertyNames = new HashSet<string> { "Limit", "Offset" };
        internal INamedTypeSymbol Binding { get; }
        internal INamedTypeSymbol Model { get; }
        internal INamedTypeSymbol Filter { get; }
        internal string EndPointPropertyName { get; }
        internal Dictionary<string, FilterProperty> Properties { get; } = new();
        internal FindCmdletTarget(string endpoint, INamedTypeSymbol binding, INamedTypeSymbol model, TypedConstant argument)
        {
            if(argument.IsNull || argument.Value is not INamedTypeSymbol filterType)
                throw new ArgumentException();
            EndPointPropertyName = endpoint;
            Binding = binding;
            Model = model;
            Filter = filterType;
            foreach(var property in filterType.GetMembers().OfType<IPropertySymbol>())
            {
                if(IgnoredPropertyNames.Contains(property.Name))
                    continue;
                if(!Properties.ContainsKey(property.Name))
                    Properties[property.Name] = new FilterProperty(property);
            }
        }
    }

    internal sealed class FilterProperty
    {
        public string Name { get; }
        public INamedTypeSymbol Type { get; }

        public override string ToString() => Name;

        internal FilterProperty(IPropertySymbol property)
        {
            Name = property.Name;
            Type = (INamedTypeSymbol)property.Type;
        }
    }
}
