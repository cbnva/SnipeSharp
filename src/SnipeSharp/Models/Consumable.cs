using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(ConsumableConverter))]
    [GenerateConverter, GeneratePartial, GeneratePostable]
    public sealed partial class Consumable: IApiObject<Consumable>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        [SerializeAs(Static.NAME, CanPatch = true)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        [SerializeAs(Static.Id.CATEGORY, IsRequired = true, CanPatch = true)]
        public Stub<Category>? Category { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        [SerializeAs(Static.Id.COMPANY, CanPatch = true)]
        public Stub<Company>? Company { get; }

        [DeserializeAs(Static.Consumable.ITEM_NO)]
        [SerializeAs(Static.Consumable.ITEM_NO, CanPatch = true)]
        public string? ItemNumber { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        [SerializeAs(Static.Id.LOCATION, CanPatch = true)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.Types.MANUFACTURER)]
        [SerializeAs(Static.Id.MANUFACTURER, CanPatch = true)]
        public Stub<Manufacturer>? Manufacturer { get; }

        [DeserializeAs(Static.Accessory.MINIMUM_AMOUNT, IsNonNullable = true)]
        [SerializeAs(Static.Accessory.MINIMUM_AMOUNT, CanPatch = true)]
        public int MinimumQuantity { get; }

        [DeserializeAs(Static.MODEL_NUMBER)]
        [SerializeAs(Static.MODEL_NUMBER, CanPatch = true)]
        public string? ModelNumber { get; }

        [DeserializeAs(Static.Consumable.REMAINING, IsNonNullable = true)]
        public int RemainingQuantity { get; }

        [DeserializeAs(Static.Asset.ORDER_NUMBER)]
        [SerializeAs(Static.Asset.ORDER_NUMBER, CanPatch = true)]
        public string? OrderNumber { get; }

        [DeserializeAs(Static.Asset.PURCHASE_COST)]
        [SerializeAs(Static.Asset.PURCHASE_COST, CanPatch = true)]
        public decimal? PurchaseCost { get; }

        [DeserializeAs(Static.Asset.PURCHASE_DATE)]
        [SerializeAs(Static.Asset.PURCHASE_DATE, CanPatch = true)]
        public FormattedDate? PurchaseDate { get; }

        [DeserializeAs(Static.QUANTITY, IsNonNullable = true)]
        [SerializeAs(Static.QUANTITY, IsRequired = true, CanPatch = true)]
        public int Quantity { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.Asset.USER_CAN_CHECKOUT, IsNonNullable = true)]
        public bool UserCanCheckout { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialConsumable.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Checkout { get; }
            public bool Checkin { get; }
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Consumable(PartialConsumable partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            ItemNumber = partial.ItemNumber;
            Image = partial.Image;
            Category = partial.Category;
            Company = partial.Company;
            Location = partial.Location;
            Manufacturer = partial.Manufacturer;
            ModelNumber = partial.ModelNumber;
            OrderNumber = partial.OrderNumber;
            PurchaseCost = partial.PurchaseCost;
            PurchaseDate = partial.PurchaseDate;

            MinimumQuantity = partial.MinimumQuantity;
            RemainingQuantity = partial.RemainingQuantity;
            Quantity = partial.Quantity;
            UserCanCheckout = partial.UserCanCheckout;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [ExtendsPostable(typeof(Consumable))]
    public sealed partial class ConsumableProperty
    {
        [SerializeAs(Static.REQUESTABLE, CanPatch = true)]
        [JsonPropertyName(Static.REQUESTABLE)]
        public bool? IsRequestable { get; set; }
    }

    [GenerateFilter(typeof(ConsumableSortOn))]
    public sealed partial class ConsumableFilter: IFilter<Consumable>
    {
        [SerializeAsString(Static.Id.COMPANY)]
        public IApiObject<Company>? Company { get; set; }

        [SerializeAsString(Static.Id.CATEGORY)]
        public IApiObject<Category>? Category { get; set; }

        [SerializeAsString(Static.Id.MANUFACTURER)]
        public IApiObject<Manufacturer>? Manufacturer { get; set; }
    }

    [SortColumn]
    public enum ConsumableSortOn
    {
        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.Asset.ORDER_NUMBER)]
        OrderNumber,

        [EnumMember(Value = Static.Accessory.MINIMUM_AMOUNT)]
        MinimumQuantity,

        [EnumMember(Value = Static.Asset.PURCHASE_DATE)]
        PurchaseDate,

        [EnumMember(Value = Static.Asset.PURCHASE_COST)]
        PurchaseCost,

        [EnumMember(Value = Static.Types.COMPANY)]
        Company,

        [EnumMember(Value = Static.Types.CATEGORY)]
        Category,

        [EnumMember(Value = Static.MODEL_NUMBER)]
        ModelNumber,

        [EnumMember(Value = Static.Consumable.ITEM_NO)]
        ItemNumber,

        [EnumMember(Value = Static.Types.MANUFACTURER)]
        Manufacturer,

        [EnumMember(Value = Static.Types.LOCATION)]
        Location,

        [EnumMember(Value = Static.QUANTITY)]
        Quantity,

        [EnumMember(Value = Static.IMAGE)]
        Image
    }
}
