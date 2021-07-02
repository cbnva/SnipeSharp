using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(MaintenanceConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Maintenance: IApiObject<Maintenance>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.Types.ASSET)]
        [SerializeAs(Static.Id.ASSET, Type = typeof(IApiObject<Asset>), IsRequired = true, CanPatch = true)]
        public StubAsset Asset { get; }

        [DeserializeAs(Static.Types.SUPPLIER)]
        [SerializeAs(Static.Id.SUPPLIER, Type = typeof(IApiObject<Supplier>), IsRequired = true, CanPatch = true)]
        public Stub<Supplier> Supplier { get; }

        [DeserializeAs(Static.Maintenance.ASSET_MAINTENANCE_TYPE)]
        [SerializeAs(Static.Maintenance.ASSET_MAINTENANCE_TYPE, IsRequired = true, CanPatch = true)]
        public AssetMaintenanceType MaintenanceType { get; }

        [DeserializeAs(Static.TITLE)]
        [SerializeAs(Static.TITLE, IsRequired = true, CanPatch = true)]
        public string Title { get; }

        [DeserializeAs(Static.Maintenance.START_DATE)]
        [SerializeAs(Static.Maintenance.START_DATE, IsRequired = true, CanPatch = true)]
        public FormattedDate StartDate { get; }

        [DeserializeAs(Static.Types.MODEL)]
        public Stub<Model>? Model { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        public Stub<Company>? Company { get; }


        [DeserializeAs(Static.Types.LOCATION)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.NOTES), SerializeAs(Static.NOTES, CanPatch = true)]
        public string? Notes { get; }

        [DeserializeAs(Static.COST), SerializeAs(Static.COST, CanPatch = true)]
        public decimal? Cost { get; }


        [DeserializeAs(Static.Maintenance.ASSET_MAINTENANCE_TIME, Type = typeof(int))]
        public TimeSpan? MaintenanceDuration { get; }

        [DeserializeAs(Static.Maintenance.COMPLETION_DATE)]
        [SerializeAs(Static.Maintenance.COMPLETION_DATE, CanPatch = true)]
        public FormattedDate? CompletionDate { get; }

        [DeserializeAs(Static.Types.USER)]
        public Stub<User>? User { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialMaintenance.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Maintenance(PartialMaintenance partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Asset = partial.Asset ?? throw new ArgumentNullException(nameof(Asset));
            Model = partial.Model;
            Company = partial.Company;
            Title = partial.Title ?? throw new ArgumentNullException(nameof(Title));
            Location = partial.Location;
            Notes = partial.Notes;
            Supplier = partial.Supplier ?? throw new ArgumentNullException(nameof(Supplier));
            Cost = partial.Cost;
            MaintenanceType = partial.MaintenanceType ?? throw new ArgumentNullException(nameof(MaintenanceType));
            StartDate = partial.StartDate ?? throw new ArgumentNullException(nameof(StartDate));
            CompletionDate = partial.CompletionDate;
            User = partial.User;
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
            AvailableActions = new Actions(partial.AvailableActions);

            if(partial.MaintenanceDuration is int days)
                MaintenanceDuration = StartDate.Date <= CompletionDate!.Date
                    ? new TimeSpan(days, 0, 0, 0, 0)
                    : new TimeSpan(-days, 0, 0, 0, 0);
        }
    }

    [GenerateFilter(typeof(MaintenanceSortOn))]
    public sealed partial class MaintenanceFilter: IFilter<Maintenance>
    {
    }

    [SortColumn]
    public enum MaintenanceSortOn
    {
        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.TITLE)]
        Title,

        [EnumMember(Value = Static.Maintenance.ASSET_MAINTENANCE_TYPE)]
        AssetMaintenanceType,

        [EnumMember(Value = Static.Maintenance.ASSET_MAINTENANCE_TIME)]
        AssetMaintenanceTime,

        [EnumMember(Value = Static.COST)]
        Cost,

        [EnumMember(Value = Static.Maintenance.START_DATE)]
        StartDate,

        [EnumMember(Value = Static.Maintenance.COMPLETION_DATE)]
        CompletionDate,

        [EnumMember(Value = Static.NOTES)]
        Notes,

        [EnumMember(Value = Static.ASSET_TAG)]
        AssetTag,

        [EnumMember(Value = Static.ASSET_NAME)]
        AssetName,

        [EnumMember(Value = Static.Id.USER)]
        UserId
    }

    [JsonConverter(typeof(EnumJsonConverter<AssetMaintenanceType>))]
    public enum AssetMaintenanceType
    {
        [EnumMember(Value = "Maintenance")]
        Maintenance,

        [EnumMember(Value = "Repair")]
        Repair,

        [EnumMember(Value = "Upgrade")]
        Upgrade,

        [EnumMember(Value = "PAT test")]
        PATTest,

        [EnumMember(Value = "Calibration")]
        Calibration,

        [EnumMember(Value = "Software Support")]
        SoftwareSupport,

        [EnumMember(Value = "Hardware Support")]
        HardwareSupport,
    }

    [ExtendsPostable(typeof(Maintenance))]
    public sealed partial class MaintenanceProperty: IPutable<Maintenance>, IPostable<Maintenance>, IPatchable<Maintenance>
    {
        [JsonPropertyName(Static.Maintenance.IS_WARRANTY)]
        [SerializeAs(Static.Maintenance.IS_WARRANTY, CanPatch = true)]
        public bool? IsWarranty { get; set; }
    }
}
