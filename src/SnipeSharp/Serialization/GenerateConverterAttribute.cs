using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    internal sealed class GenerateConverterAttribute: Attribute
    {
        public Type? PartialType { get; }

        public GenerateConverterAttribute()
        {
            // infer partial type from attached type.
        }

        public GenerateConverterAttribute(Type partialType)
        {
            PartialType = partialType;
        }
    }
}
