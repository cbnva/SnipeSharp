// TODO: figure out what accessory actually takes/has
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(AccessoryConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Accessory: IApiObject<Accessory>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        [SerializeAs(Static.IMAGE, CanPatch = true)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        [SerializeAs(Static.Id.COMPANY, CanPatch = true)]
        public Stub<Company>? Company { get; }

        [DeserializeAs(Static.Types.MANUFACTURER)]
        [SerializeAs(Static.Id.MANUFACTURER, CanPatch = true)]
        public Stub<Manufacturer>? Manufacturer { get; }

        [DeserializeAs(Static.Types.SUPPLIER)]
        [SerializeAs(Static.Id.SUPPLIER, CanPatch = true)]
        public Stub<Supplier>? Supplier { get; }

        [DeserializeAs(Static.MODEL_NUMBER)]
        [SerializeAs(Static.MODEL_NUMBER, CanPatch = true)]
        public string? ModelNumber { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        [SerializeAs(Static.Id.CATEGORY, IsRequired = true, CanPatch = true)]
        public Stub<Category>? Category { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        [SerializeAs(Static.Id.LOCATION, CanPatch = true)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.NOTES)]
        public string? Notes { get; }

        [DeserializeAs(Static.QUANTITY)]
        [SerializeAs(Static.QUANTITY, IsRequired = true, CanPatch = true)]
        public int? Quantity { get; }

        [DeserializeAs(Static.Asset.PURCHASE_DATE)]
        [SerializeAs(Static.Asset.PURCHASE_DATE, CanPatch = true)]
        public FormattedDate? PurchaseDate { get; }

        [DeserializeAs(Static.Asset.PURCHASE_COST, IsNonNullable = true)]
        [SerializeAs(Static.Asset.PURCHASE_COST, CanPatch = true)]
        public decimal PurchaseCost { get; }

        [DeserializeAs(Static.Asset.ORDER_NUMBER)]
        [SerializeAs(Static.Asset.ORDER_NUMBER, CanPatch = true)]
        public string? OrderNumber { get; }

        [DeserializeAs(Static.Accessory.MINIMUM_QUANTITY)]
        public int? MinimumQuantity { get; }

        [DeserializeAs(Static.Accessory.REMAINING_QUANTITY, IsNonNullable = true)]
        public int RemainingQuantity { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.Asset.USER_CAN_CHECKOUT, IsNonNullable = true)]
        public bool UserCanCheckout { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialAccessory.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Checkout { get; }

            public bool Checkin { get; } // will always be false
            public bool Delete { get; }
            public bool Update { get; }
        }

        internal Accessory(PartialAccessory partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Image = partial.Image;
            Company = partial.Company;
            Manufacturer = partial.Manufacturer;
            Supplier = partial.Supplier;
            ModelNumber = partial.ModelNumber;
            Category = partial.Category;
            Location = partial.Location;
            Notes = partial.Notes;
            Quantity = partial.Quantity;
            PurchaseDate = partial.PurchaseDate;
            PurchaseCost = partial.PurchaseCost;
            OrderNumber = partial.OrderNumber;
            MinimumQuantity = partial.MinimumQuantity;
            RemainingQuantity = partial.RemainingQuantity;
            UserCanCheckout = partial.UserCanCheckout;
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    public sealed partial class AccessoryProperty
    {
        [SerializeAs(Static.REQUESTABLE)]
        public bool? IsRequestable { get; set; }
    }

    [GenerateFilter(typeof(AccessorySortOn))]
    public sealed partial class AccessoryFilter: IFilter<Accessory>
    {
        [SerializeAsString(Static.Id.COMPANY)]
        public IApiObject<Company>? Company { get; set; }

        [SerializeAsString(Static.Id.CATEGORY)]
        public IApiObject<Category>? Category { get; set; }

        [SerializeAsString(Static.Id.MANUFACTURER)]
        public IApiObject<Manufacturer>? Manufacturer { get; set; }

        [SerializeAsString(Static.Id.SUPPLIER)]
        public IApiObject<Supplier>? Supplier { get; set; }
    }

    [SortColumn]
    public enum AccessorySortOn
    {
        [EnumMember(Value = Static.CREATED_AT)]
        CreatedAt = 0,

        [EnumMember(Value = Static.Types.CATEGORY)]
        Category,

        [EnumMember(Value = Static.Types.COMPANY)]
        Company,

        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.MODEL_NUMBER)]
        ModelNumber,

        [EnumMember(Value = Static.EOL)]
        EndOfLife,

        [EnumMember(Value = Static.NOTES)]
        Notes,

        [EnumMember(Value = Static.Accessory.MINIMUM_AMOUNT)]
        MinimumQuantity,

        [EnumMember(Value = Static.Id.COMPANY)]
        CompanyId
    }
}
