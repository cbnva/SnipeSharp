using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class SerializeAsAttribute: Attribute
    {
        public string Key { get; }

        public bool IsRequired { get; set; }

        public bool CanPatch { get; set; }

        public Type? Type { get; set; }

        public SerializeAsAttribute(string key)
        {
            Key = key;
        }
    }
}
