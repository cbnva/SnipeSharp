using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    internal sealed class GeneratePartialAttribute: Attribute
    {
    }
}
