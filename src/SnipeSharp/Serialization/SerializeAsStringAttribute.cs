using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class SerializeAsStringAttribute: Attribute
    {
        public string Key { get; set; }

        public string? With { get; set; }

        public SerializeAsStringAttribute(string key)
        {
            Key = key;
        }
    }
}
