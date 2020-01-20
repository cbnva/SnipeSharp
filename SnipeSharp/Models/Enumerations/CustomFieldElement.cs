using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp.Models.Enumerations
{
    /// <summary>
    /// What the field type of the custom field is.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [EnumNameConverter]
    public enum CustomFieldElement
    {
        /// <summary>The field type is a list selection.</summary>
        [EnumMember(Value = "list")]
        List,
        /// <summary>The field type is a text field.</summary>
        [EnumMember(Value = "text")]
        Text,
        /// <summary>The field type is a text area, with newline support.</summary>
        [EnumMember(Value = "textarea")]
        TextArea
    }
}
