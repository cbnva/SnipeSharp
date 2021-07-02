using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Exceptions;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(CompanyConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Company: IApiObject<Company>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME), SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDate CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDate UpdatedAt { get; }

        [DeserializeAs(Static.Count.ASSETS, IsNonNullable = true)]
        public int AssetsCount { get; }

        [DeserializeAs(Static.Count.LICENSES, IsNonNullable = true)]
        public int LicensesCount { get; }

        [DeserializeAs(Static.Count.ACCESSORIES, IsNonNullable = true)]
        public int AccessoriesCount { get; }

        [DeserializeAs(Static.Count.CONSUMABLES, IsNonNullable = true)]
        public int ConsumablesCount { get; }

        [DeserializeAs(Static.Count.COMPONENTS, IsNonNullable = true)]
        public int ComponentsCount { get; }

        [DeserializeAs(Static.Count.USERS, IsNonNullable = true)]
        public int UsersCount { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialCompany.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Company(PartialCompany partial)
        {
            Id = partial.Id ?? throw new MissingRequiredPropertyException(nameof(Id), nameof(Company));
            Name = partial.Name ?? throw new MissingRequiredPropertyException(nameof(Name), nameof(Company));
            Image = partial.Image;
            CreatedAt = partial.CreatedAt ?? throw new MissingRequiredPropertyException(nameof(CreatedAt), nameof(Company));
            UpdatedAt = partial.UpdatedAt ?? throw new MissingRequiredPropertyException(nameof(UpdatedAt), nameof(Company));
            AssetsCount = partial.AssetsCount;
            LicensesCount = partial.LicensesCount;
            AccessoriesCount = partial.AccessoriesCount;
            ConsumablesCount = partial.ConsumablesCount;
            ComponentsCount = partial.ComponentsCount;
            UsersCount = partial.UsersCount;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [GenerateFilter(typeof(CompanySortOn))]
    public sealed partial class CompanyFilter: IFilter<Company>
    {
    }

    [SortColumn]
    public enum CompanySortOn
    {
        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.CREATED_AT)]
        CreatedAt,

        [EnumMember(Value = Static.UPDATED_AT)]
        UpdatedAt,

        [EnumMember(Value = Static.Count.USERS)]
        UsersCount,

        [EnumMember(Value = Static.Count.ASSETS)]
        AssetsCount,

        [EnumMember(Value = Static.Count.LICENSES)]
        Licensescount,

        [EnumMember(Value = Static.Count.ACCESSORIES)]
        AccessoriesCount,

        [EnumMember(Value = Static.Count.COMPONENTS)]
        ComponentsCount,

        [EnumMember(Value = Static.Count.CONSUMABLES)]
        ConsumablesCount
    }
}
