using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class GeneratePostableAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class ExtendsPostableAttribute: Attribute
    {
        public Type Type { get; }

        public ExtendsPostableAttribute(Type type)
            => Type = type;
    }
}
