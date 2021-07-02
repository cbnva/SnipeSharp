using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class GenerateFilterAttribute: Attribute
    {
        public Type? ColumnType { get; }

        public bool HasSearchString { get; set; } = true;

        public GenerateFilterAttribute()
        {
        }

        public GenerateFilterAttribute(Type columnType)
        {
            ColumnType = columnType;
        }
    }
}
