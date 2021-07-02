using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SnipeSharp.PowerShell.Generator
{
    internal sealed class UpdateCmdletReceiver : ISyntaxContextReceiver
    {
        public Dictionary<string, string> ModelBindingMap { get; } = new();
        public List<UpdateCmdletTarget> Targets { get; } = new();
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if(context.Node is not ClassDeclarationSyntax node)
                return;
            if(context.SemanticModel.GetDeclaredSymbol(node) is not INamedTypeSymbol symbol)
                return;
            var generateNewCmdlet = symbol.TryGetAttribute("SnipeSharp.PowerShell.GenerateNewCmdletAttribute", out var newData);
            var generateSetCmdlet = symbol.TryGetAttribute("SnipeSharp.PowerShell.GenerateSetCmdletAttribute", out var setData);
            if(!generateNewCmdlet && !generateSetCmdlet)
                return;

            TypedConstant constant;
            var postType = generateNewCmdlet && newData.TryGetOption(0, out constant) && !constant.IsNull && constant.Value is INamedTypeSymbol postTypeValue ? postTypeValue : null;
            var putType = generateSetCmdlet && setData.TryGetOption(0, out constant) && !constant.IsNull && constant.Value is INamedTypeSymbol putTypeValue ? putTypeValue : null;
            var patchType = generateSetCmdlet && setData.TryGetOption(1, out constant) && !constant.IsNull && constant.Value is INamedTypeSymbol patchTypeValue ? patchTypeValue : putType;
            if(!symbol.TryGetInterface("SnipeSharp.PowerShell.IBinding", out var bindingInterface))
                return;
            if(bindingInterface.TypeArguments[0] is not INamedTypeSymbol modelType)
                return;
            ModelBindingMap[modelType.ToDisplayString()] = symbol.ToDisplayString();

            if(!symbol.TryGetAttribute("SnipeSharp.PowerShell.AssociatedEndPointAttribute", out var data))
                return;
            if(!data.TryGetOption(0, out var propertyNameConstant) || propertyNameConstant.IsNull)
                return;

            Targets.Add(new UpdateCmdletTarget(symbol, modelType,
                endpoint: propertyNameConstant.Value!.ToString(),
                postType: postType,
                putType: putType,
                patchType: patchType
            ));
        }
    }

    internal sealed class UpdateCmdletTarget
    {
        public string EndPointPropertyName { get; }

        public INamedTypeSymbol Binding { get; }
        public INamedTypeSymbol Model { get; }

        public INamedTypeSymbol? PostType { get; }
        public INamedTypeSymbol? PutType { get; }
        public INamedTypeSymbol? PatchType { get; }

        public List<UpdateCmdletProperty> PatchProperties { get; } = new();
        public List<UpdateCmdletProperty> PutProperties { get; } = new();
        public List<UpdateCmdletProperty> PostProperties { get; } = new();

        internal UpdateCmdletTarget(INamedTypeSymbol bindingType, INamedTypeSymbol modelType, string endpoint, INamedTypeSymbol? postType, INamedTypeSymbol? putType, INamedTypeSymbol? patchType)
        {
            Binding = bindingType;
            Model = modelType;
            EndPointPropertyName = endpoint;
            PostType = postType;
            PutType = putType;
            PatchType = patchType;

            if(null != PostType)
                foreach(var property in PostType.GetMembers().OfType<IPropertySymbol>())
                    PostProperties.Add(new UpdateCmdletProperty(property));

            if(SymbolEqualityComparer.Default.Equals(PostType, PutType))
                PutProperties = PostProperties;
            else if(null != PutType)
                foreach(var property in PutType.GetMembers().OfType<IPropertySymbol>())
                    PutProperties.Add(new UpdateCmdletProperty(property));

            if(SymbolEqualityComparer.Default.Equals(PostType, PatchType))
                PatchProperties = PostProperties;
            else if(SymbolEqualityComparer.Default.Equals(PutType, PatchType))
                PatchProperties = PutProperties;
            else if(null != PatchType)
                foreach(var property in PatchType.GetMembers().OfType<IPropertySymbol>())
                    PatchProperties.Add(new UpdateCmdletProperty(property));
        }
    }

    internal sealed class UpdateCmdletProperty
    {
        public bool IsRequired { get; }
        public INamedTypeSymbol Type { get; }
        public string Name { get; }

        internal UpdateCmdletProperty(IPropertySymbol symbol)
        {
            IsRequired = symbol.TryGetAttribute("SnipeSharp.Serialization.RequiredAttribute", out var _);
            Type = (INamedTypeSymbol)symbol.Type;
            Name = symbol.Name;
        }
    }
}
