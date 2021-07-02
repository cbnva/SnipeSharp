using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal sealed class DeserializeAsAttribute: Attribute
    {
        public string Key { get; }

        public Type? Type { get; set; }

        public bool IsNonNullable { get; set; }

        public DeserializeAsAttribute(string key)
        {
            Key = key;
        }
    }
}
