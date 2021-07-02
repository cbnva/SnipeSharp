using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    internal sealed class GeneratePartialActionsAttribute: Attribute
    {
    }
}
