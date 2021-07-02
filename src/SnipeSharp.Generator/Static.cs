using System.Runtime.Serialization;
using Microsoft.CodeAnalysis;

namespace SnipeSharp.Generator
{
    internal static class Static
    {
        internal const string Namespace = "SnipeSharp.Serialization";

        internal const string GeneratePartialAttribute = nameof(GeneratePartialAttribute);
        internal const string GeneratePartialAttributeFullName = Namespace + "." + GeneratePartialAttribute;

        internal const string GeneratePartialActionsAttribute = nameof(GeneratePartialActionsAttribute);
        internal const string GeneratePartialActionsAttributeFullName = Namespace + "." + GeneratePartialActionsAttribute;

        internal const string DeserializeAsAttribute = nameof(DeserializeAsAttribute);
        internal const string DeserializeAsAttributeFullName = Namespace + "." + DeserializeAsAttribute;

        internal const string SortColumnAttribute = nameof(SortColumnAttribute);
        internal const string SortColumnAttributeFullName = Namespace + "." + SortColumnAttribute;

        internal const string GenerateFilterAttribute = nameof(GenerateFilterAttribute);
        internal const string GenerateFilterAttributeFullName = Namespace + "." + GenerateFilterAttribute;

        internal const string GenerateConverterAttribute = nameof(GenerateConverterAttribute);
        internal const string GenerateConverterAttributeFullName = Namespace + "." + GenerateConverterAttribute;

        internal const string SerializeAsStringAttribute = nameof(SerializeAsStringAttribute);
        internal const string SerializeAsStringAttributeFullName = Namespace + "." + SerializeAsStringAttribute;

        internal const string EnumMemberAttributeFullName = "System.Runtime.Serialization." + nameof(EnumMemberAttribute);
        internal const string JsonPropertyNameAttributeFullName = "System.Text.Json.Serialization.JsonPropertyNameAttribute";
        internal const string IApiObjectFullName = "SnipeSharp.IApiObject";
        internal const string NullableBoolFullName = "System.Nullable";

        internal const string SerializeAsAttributeFullName = "SnipeSharp.Serialization.SerializeAsAttribute";
        internal const string GeneratePostableAttributeFullName = "SnipeSharp.Serialization.GeneratePostableAttribute";
        internal const string ExtendsPostableAttributeFullName = "SnipeSharp.Serialization.ExtendsPostableAttribute";
    }
}
