using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(FieldsetConverter))]
    [GeneratePartial, GenerateConverter]
    public sealed partial class Fieldset: IApiObject<Fieldset>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.Fieldset.FIELDS, Type = typeof(DataTable<CustomField>))]
        public IReadOnlyList<CustomField> Fields { get; }

        [DeserializeAs(Static.Fieldset.MODELS, Type = typeof(DataTable<Stub<Model>>))]
        public IReadOnlyList<Stub<Model>> Models { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        internal Fieldset(PartialFieldset partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Fields = partial.Fields ?? throw new ArgumentNullException(nameof(Fields));
            Models = partial.Models ?? throw new ArgumentNullException(nameof(Models));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
        }
    }
}
