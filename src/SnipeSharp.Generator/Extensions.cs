using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace SnipeSharp.Generator
{
    internal static class Extensions
    {
        internal static bool TryGetAnyGenericInterface(this INamedTypeSymbol symbol, string ifaceName, out ITypeSymbol genericTypeArgument)
        {

            var a = symbol.ToDisplayString().Split('<')[0] == ifaceName
                    ? symbol
                    : symbol.AllInterfaces.FirstOrDefault(a => a?.ToDisplayString().Split('<')[0] == ifaceName);
            if(null == a || a.TypeArguments.Length < 1)
            {
                genericTypeArgument = default!;
                return false;
            }
            genericTypeArgument = a.TypeArguments[0];
            return true;
        }

        internal static bool TryGetAttribute(this ISymbol symbol, string attribute, out AttributeData data)
        {
            var a = symbol.GetAttributes().SingleOrDefault(a => a?.AttributeClass?.ToDisplayString() == attribute);
            if(null == a)
            {
                data = default!;
                return false;
            }
            data = a;
            return true;
        }


        internal static bool TryGetOption(this AttributeData data, string name, out TypedConstant value)
        {
            var arg = data.NamedArguments.Where(a => a.Key == name).ToArray();
            if(0 == arg.Length)
            {
                value = default!;
                return false;
            }
            value = arg[0].Value;
            return true;
        }

        internal static bool TryGetOption(this AttributeData data, int position, out TypedConstant value)
        {
            if(position >= data.ConstructorArguments.Length)
            {
                value = default!;
                return false;
            }
            value = data.ConstructorArguments[position];
            return true;
        }

        internal static string Nullable(this ITypeSymbol type)
        {
            return type.ToDisplayString() + "?";
        }

        internal static string Nullable(this string type)
        {
            if(type.EndsWith("?"))
                return type;
            return type + "?";
        }

        internal static string FirstToLower(this string str)
            => str.Substring(0, 1).ToLowerInvariant() + str.Substring(1);

        internal static string AsString(this Accessibility accessibility)
            => accessibility switch
            {
                Accessibility.NotApplicable => "",
                Accessibility.Private => "private",
                Accessibility.Protected => "protected",
                Accessibility.Internal => "internal",
                Accessibility.ProtectedOrInternal => "protected internal",
                Accessibility.Public => "public",
                _ => throw new ArgumentException($"Unsupported accessibility type {accessibility}")
            };

        internal static string GetGenericTypeConstraints(this INamedTypeSymbol symbol)
        {
            var builder = new StringBuilder();
            foreach(var typeParameter in symbol.TypeParameters)
            {
                if(typeParameter.ConstraintTypes.Any()
                    || typeParameter.HasConstructorConstraint
                    || typeParameter.HasNotNullConstraint
                    || typeParameter.HasReferenceTypeConstraint
                    || typeParameter.HasUnmanagedTypeConstraint
                    || typeParameter.HasValueTypeConstraint)
                {
                    builder.Append(" where ").Append(typeParameter.Name).Append(" : ");
                    var separator = "";
                    if(typeParameter.HasReferenceTypeConstraint)
                    {
                        builder.Append(separator).Append("class");
                        separator = ", ";
                    }
                    if(typeParameter.HasNotNullConstraint)
                    {
                        builder.Append(separator).Append("notnull");
                        separator = ", ";
                    }
                    if(typeParameter.HasUnmanagedTypeConstraint)
                    {
                        builder.Append(separator).Append("unmanaged");
                        separator = ", ";
                    }
                    if(typeParameter.HasValueTypeConstraint)
                    {
                        builder.Append(separator).Append("value");
                        separator = ", ";
                    }
                    foreach(var constraintType in typeParameter.ConstraintTypes)
                    {
                        builder.Append(separator).Append(constraintType.ToDisplayString());
                        separator = ", ";
                    }

                    if(typeParameter.HasConstructorConstraint)
                        builder.Append("separator").Append("new()");
                }
            }
            return builder.ToString();
        }
    }
}
