using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(ModelConverter))]
    [GenerateConverter, GeneratePartial]
    public sealed partial class Model: IApiObject<Model>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.Types.MANUFACTURER)]
        public Stub<Manufacturer>? Manufacturer { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.MODEL_NUMBER)]
        public string ModelNumber { get; }

        [DeserializeAs(Static.Types.DEPRECIATION)]
        public Stub<Depreciation>? Depreciation { get; }

        [DeserializeAs(Static.Count.ASSETS, IsNonNullable = true)]
        public int AssetsCount { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        public Stub<Category>? Category { get; }

        [DeserializeAs(Static.Types.FIELDSET)]
        public Stub<Fieldset>? Fieldset { get; }

        [DeserializeAs(Static.EOL)]
        public string EndOfLife { get; }

        [DeserializeAs(Static.REQUESTABLE, IsNonNullable = true)]
        public bool IsRequestable { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.DELETED_AT)]
        public FormattedDateTime? DeletedAt { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialModel.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
            public bool Clone { get; }
            public bool Restore { get; }
        }

        internal Model(PartialModel partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            ModelNumber = partial.ModelNumber ?? throw new ArgumentNullException(nameof(ModelNumber));
            EndOfLife = partial.EndOfLife ?? throw new ArgumentNullException(nameof(EndOfLife));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            Manufacturer = partial.Manufacturer;
            Image = partial.Image;
            Depreciation = partial.Depreciation;
            Category = partial.Category;
            Fieldset = partial.Fieldset;
            DeletedAt = partial.DeletedAt;

            AssetsCount = partial.AssetsCount;
            IsRequestable = partial.IsRequestable;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }
}
