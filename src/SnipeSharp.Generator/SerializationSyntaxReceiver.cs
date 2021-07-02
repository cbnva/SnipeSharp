using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SnipeSharp.Generator
{
    internal sealed class SerializationSyntaxReceiver : ISyntaxContextReceiver
    {
        public Dictionary<string, UpdatableClass> Classes { get; } = new Dictionary<string, UpdatableClass>();

        private Dictionary<string, List<UpdatableProperty>> ExtendedProperties { get; } = new Dictionary<string, List<UpdatableProperty>>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if(context.Node is not ClassDeclarationSyntax node)
                return;
            if(context.SemanticModel.GetDeclaredSymbol(node) is not INamedTypeSymbol symbol)
                return;
            if(symbol.TryGetAttribute(Static.GeneratePostableAttributeFullName, out var _))
            {
                var extended = ExtendedProperties.TryGetValue(symbol.ToDisplayString(), out var extendedProperties) ? extendedProperties : null;
                Classes.Add(symbol.ToDisplayString(), new UpdatableClass(symbol, extended));
                if(null != extended)
                    ExtendedProperties.Remove(symbol.ToDisplayString());
            } else if(symbol.TryGetAttribute(Static.ExtendsPostableAttributeFullName, out var attr)
                && attr.TryGetOption(0, out var typeConstant)
                && typeConstant.Value is ITypeSymbol typeSymbol)
            {
                // get the UpdatableClass, or a list if there isn't one
                // keep the list around so we can add it to a class later, or just add it directly to the class.
                var updatableClass = Classes.TryGetValue(typeSymbol.ToDisplayString(), out var updatable) ? updatable : null;
                var list = null != updatableClass ? null : new List<UpdatableProperty>();
                foreach(var property in symbol.GetMembers().OfType<IPropertySymbol>())
                {
                    if(property.TryGetAttribute(Static.SerializeAsAttributeFullName, out var attribute))
                    {
                        if(!property.TryGetAttribute(Static.JsonPropertyNameAttributeFullName, out var _))
                            throw new ArgumentException($"Property is missing JsonPropertyName: {property.ToDisplayString()}");
                        var prop = new UpdatableProperty(property, attribute){ IsExtra = true };
                        if(null != updatableClass)
                            updatableClass.AddProperty(prop);
                        else
                            list!.Add(prop);
                    }
                }
                if(list != null)
                    ExtendedProperties[typeSymbol.ToDisplayString()] = list;
            }
        }
    }

    internal sealed class UpdatableClass
    {
        public INamedTypeSymbol Symbol { get; }
        public string Modifiers { get; }
        public List<UpdatableProperty> Properties { get; } = new List<UpdatableProperty>();

        public string Name => Symbol.Name;
        public string Namespace => Symbol.ContainingNamespace.ToDisplayString();
        public string FullName => Symbol.ToDisplayString();

        public bool AnyPatchable { get; private set; }
        public bool AllPatchable { get; private set; } = true;
        public bool HasExtraProperties { get; private set; }

        internal UpdatableClass(INamedTypeSymbol symbol, List<UpdatableProperty>? extendedProperties)
        {
            Symbol = symbol;
            Modifiers = symbol.DeclaredAccessibility.AsString();
            foreach(var property in symbol.GetMembers().OfType<IPropertySymbol>())
                if(property.TryGetAttribute(Static.SerializeAsAttributeFullName, out var attribute))
                    AddProperty(new UpdatableProperty(property, attribute));
            if(null != extendedProperties)
                foreach(var property in extendedProperties)
                    AddProperty(property);
        }

        internal void AddProperty(UpdatableProperty property)
        {
            AnyPatchable = AnyPatchable || property.CanPatch;
            AllPatchable = AllPatchable && property.CanPatch;
            HasExtraProperties = HasExtraProperties || property.IsExtra;
            Properties.Add(property);
        }
    }

    internal sealed class UpdatableProperty
    {
        public IPropertySymbol Property { get; }
        public string Key { get; }
        public bool IsRequired { get; }
        public bool CanPatch { get; }

        public string Name { get; }
        public string Type { get; }
        public bool IsApiObject { get; }
        public bool IsNullable { get; }

        public bool IsExtra { get; set; }

        public UpdatableProperty(IPropertySymbol property, AttributeData attribute)
        {
            Property = property;

            if(!attribute.TryGetOption(0, out var keyConstant) || keyConstant.IsNull)
                throw new ArgumentNullException(nameof(Key));
            Key = keyConstant.Value!.ToString();
            IsRequired = attribute.TryGetOption(nameof(IsRequired), out var required) && required.Value is bool r && r;
            CanPatch = attribute.TryGetOption(nameof(CanPatch), out var patch) && patch.Value is bool b && b;

            Name = property.Name;
            var type = attribute.TryGetOption(nameof(Type), out var typeConstant) && typeConstant.Value is INamedTypeSymbol typeSymbol
                ? typeSymbol
                : (INamedTypeSymbol)property.Type;
            IsApiObject = type.TryGetAnyGenericInterface("SnipeSharp.IApiObject", out var genericType);
            Type = IsApiObject
                    ? $"SnipeSharp.IApiObject<{genericType.ToDisplayString()}>{(type.NullableAnnotation == NullableAnnotation.Annotated ? "?" : "")}"
                    : type.ToDisplayString();
            IsNullable = type.Name == "Nullable" || type.NullableAnnotation == NullableAnnotation.Annotated;
        }
    }
}
