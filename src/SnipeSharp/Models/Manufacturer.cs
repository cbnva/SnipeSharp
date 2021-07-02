using System;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(ManufacturerConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Manufacturer: IApiObject<Manufacturer>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.URL)]
        [SerializeAs(Static.URL, CanPatch = true)]
        public Uri? Url { get; }

        [DeserializeAs(Static.IMAGE)]
        [SerializeAs(Static.IMAGE, CanPatch = true)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Manufacturer.SUPPORT_URL)]
        [SerializeAs(Static.Manufacturer.SUPPORT_URL, CanPatch = true)]
        public Uri? SupportUrl { get; }

        [DeserializeAs(Static.Manufacturer.SUPPORT_PHONE)]
        [SerializeAs(Static.Manufacturer.SUPPORT_PHONE, CanPatch = true)]
        public string? SupportPhone { get; }

        [DeserializeAs(Static.Manufacturer.SUPPORT_EMAIL)]
        [SerializeAs(Static.Manufacturer.SUPPORT_EMAIL, CanPatch = true)]
        public string? SupportEmail { get; }

        [DeserializeAs(Static.Count.ACCESSORIES, IsNonNullable = true)]
        public int AccessoriesCount { get; }

        [DeserializeAs(Static.Count.ASSETS, IsNonNullable = true)]
        public int AssetsCount { get; }

        [DeserializeAs(Static.Count.CONSUMABLES, IsNonNullable = true)]
        public int ConsumablesCount { get; }

        [DeserializeAs(Static.Count.LICENSES, IsNonNullable = true)]
        public int LicensesCount { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.DELETED_AT)]
        public FormattedDateTime? DeletedAt { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialManufacturer.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Restore { get; }
            public bool Delete { get; }
        }

        internal Manufacturer(PartialManufacturer partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Url = partial.Url;
            Image = partial.Image;
            SupportUrl = partial.SupportUrl;
            SupportPhone = partial.SupportPhone;
            SupportEmail = partial.SupportEmail;
            AccessoriesCount = partial.AccessoriesCount;
            AssetsCount = partial.AssetsCount;
            ConsumablesCount = partial.ConsumablesCount;
            LicensesCount = partial.LicensesCount;
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
            DeletedAt = partial.DeletedAt;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [GenerateFilter(typeof(ManufacturerSortOn))]
    public sealed partial class ManufacturerFilter : IFilter<Manufacturer>
    {
        public bool? IsDeleted { get; set; }
    }

    [SortColumn]
    public enum ManufacturerSortOn
    {
        [EnumMember(Value = Static.CREATED_AT)]
        CreatedAt = 0,

        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.URL)]
        Url,

        [EnumMember(Value = Static.Manufacturer.SUPPORT_URL)]
        SupportUrl,

        [EnumMember(Value = Static.Manufacturer.SUPPORT_PHONE)]
        SupportPhone,

        [EnumMember(Value = Static.Manufacturer.SUPPORT_EMAIL)]
        SupportEmail,

        [EnumMember(Value = Static.UPDATED_AT)]
        UpdatedAt,

        [EnumMember(Value = Static.IMAGE)]
        Image,

        //[EnumMember(Value = Static.Count.ACCESSORIES)]
        //AccessoriesCount, // for some reason, can't actually sort as this, but can sort on components? // TODO: submit patch to SnipeIT

        [EnumMember(Value = Static.Count.ASSETS)]
        AssetsCount,

        [EnumMember(Value = Static.Count.CONSUMABLES)]
        ConsumablesCount,

        [EnumMember(Value = Static.Count.LICENSES)]
        LicensesCount,
    }
}
