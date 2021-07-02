using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(CustomFieldConverter))]
    [GenerateConverter, GeneratePartial]
    public sealed partial class CustomField: IApiObject<CustomField>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.CustomField.DB_COLUMN_NAME)]
        public string DatabaseColumnName { get; }

        [DeserializeAs(Static.CustomField.FORMAT)]
        public string Format { get; }

        [DeserializeAs(Static.CustomField.FIELD_VALUES)]
        public string? FieldValues { get; }

        [DeserializeAs(Static.CustomField.FIELD_VALUES_ARRAY, Type = typeof(string[]))]
        public IReadOnlyList<string>? FieldValuesList { get; }

        [DeserializeAs(Static.TYPE)]
        public string Type { get; } // TODO: enum type

        [DeserializeAs(Static.CustomField.REQUIRED, IsNonNullable = true)]
        public bool IsRequired { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        internal CustomField(PartialCustomField partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            DatabaseColumnName = partial.DatabaseColumnName ?? throw new ArgumentNullException(nameof(DatabaseColumnName));
            Format = partial.Format ?? throw new ArgumentNullException(nameof(Format));
            Type = partial.Type ?? throw new ArgumentNullException(nameof(Type));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            FieldValues = partial.FieldValues;
            IsRequired = partial.IsRequired;

            FieldValuesList = null == partial.FieldValuesList ? null : Array.AsReadOnly(partial.FieldValuesList);
        }
    }
}
