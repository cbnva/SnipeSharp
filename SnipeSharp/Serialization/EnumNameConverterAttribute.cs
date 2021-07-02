using System;
namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    internal sealed class EnumNameConverterAttribute : Attribute
    {
    }
}
