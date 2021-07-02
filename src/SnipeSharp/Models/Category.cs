using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(CategoryConverter))]
    [GenerateConverter, GeneratePartial, GeneratePostable]
    public sealed partial class Category: IApiObject<Category>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Category.CATEGORY_TYPE)]
        [SerializeAs(Static.Category.CATEGORY_TYPE, IsRequired = true, CanPatch = true)]
        public CategoryType CategoryType { get; }

        [DeserializeAs(Static.Category.HAS_EULA, IsNonNullable = true)]
        [SerializeAs(Static.Category.HAS_EULA, CanPatch = true)]
        public bool HasEula { get; }

        [DeserializeAs(Static.Category.EULA)]
        [SerializeAs(Static.Category.EULA_TEXT, CanPatch = true)]
        public string? Eula { get; }

        [DeserializeAs(Static.Category.CHECKIN_EMAIL, IsNonNullable = true)]
        [SerializeAs(Static.Category.CHECKIN_EMAIL, CanPatch = true)]
        public bool CheckInEmail { get; }

        [DeserializeAs(Static.Category.REQUIRE_ACCEPTANCE, IsNonNullable = true)]
        [SerializeAs(Static.Category.REQUIRE_ACCEPTANCE, CanPatch = true)]
        public bool RequireAcceptance { get; }

        [DeserializeAs(Static.Count.ITEM, IsNonNullable = true)]
        public int ItemCount { get; }

        [DeserializeAs(Static.Count.ASSETS, IsNonNullable = true)]
        public int AssetsCount { get; }

        [DeserializeAs(Static.Count.ACCESSORIES, IsNonNullable = true)]
        public int AccessoriesCount { get; }

        [DeserializeAs(Static.Count.CONSUMABLES, IsNonNullable = true)]
        public int ConsumablesCount { get; }

        [DeserializeAs(Static.Count.LICENSES, IsNonNullable = true)]
        public int LicensesCount { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialCategory.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Category(PartialCategory partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            CategoryType = partial.CategoryType ?? throw new ArgumentNullException(nameof(CategoryType));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            Image = partial.Image;
            Eula = partial.Eula;

            HasEula = partial.HasEula;
            CheckInEmail = partial.CheckInEmail;
            RequireAcceptance = partial.RequireAcceptance;
            ItemCount = partial.ItemCount;
            AssetsCount = partial.AssetsCount;
            AccessoriesCount = partial.AccessoriesCount;
            ConsumablesCount = partial.ConsumablesCount;
            LicensesCount = partial.LicensesCount;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [JsonConverter(typeof(EnumJsonConverter<CategoryType>))]
    public enum CategoryType
    {
        [EnumMember(Value = "Asset")]
        Asset,

        [EnumMember(Value = "Accessory")]
        Accessory,

        [EnumMember(Value = "Consumable")]
        Consumable,

        [EnumMember(Value = "Component")]
        Component,

        [EnumMember(Value = "License")]
        License
    }

    [GenerateFilter(typeof(CategorySortOn))]
    public sealed partial class CategoryFilter: IFilter<Category>
    {
    }

    [SortColumn]
    public enum CategorySortOn
    {
        [EnumMember(Value = Static.Count.ASSETS)]
        AssetsCount = 0,

        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.Category.CATEGORY_TYPE)]
        CategoryType,

        [EnumMember(Value = Static.Category.USE_DEFAULT_EULA)]
        UseDefaultEula,

        [EnumMember(Value = Static.Category.EULA_TEXT)]
        EulaText,

        [EnumMember(Value = Static.Category.REQUIRE_ACCEPTANCE)]
        RequireAcceptance,

        [EnumMember(Value = Static.Category.CHECKIN_EMAIL)]
        CheckInEmail,

        [EnumMember(Value = Static.Count.ACCESSORIES)]
        AccessoriesCount,

        [EnumMember(Value = Static.Count.CONSUMABLES)]
        ConsumablesCount,

        [EnumMember(Value = Static.Count.COMPONENTS)]
        ComponentsCount,

        [EnumMember(Value = Static.Count.LICENSES)]
        LicensesCount,

        [EnumMember(Value = Static.IMAGE)]
        Image
    }

    [ExtendsPostable(typeof(Category))]
    public sealed partial class CategoryProperty
    {
        [SerializeAs(Static.Category.USE_DEFAULT_EULA, CanPatch = true)]
        [JsonPropertyName(Static.Category.USE_DEFAULT_EULA)]
        public bool UseDefaultEula { get; set; }
    }
}
