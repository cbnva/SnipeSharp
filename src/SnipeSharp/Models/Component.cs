using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(ComponentConverter))]
    [GenerateConverter, GeneratePartial, GeneratePostable]
    public sealed partial class Component: IApiObject<Component>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.SERIAL)]
        [SerializeAs(Static.SERIAL, CanPatch = true)]
        public string? SerialNumber { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        [SerializeAs(Static.Id.LOCATION, CanPatch = true)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.QUANTITY)]
        [SerializeAs(Static.QUANTITY, CanPatch = true)]
        public int? Quantity { get; }

        [DeserializeAs(Static.Accessory.MINIMUM_AMOUNT)]
        [SerializeAs(Static.Accessory.MINIMUM_AMOUNT, CanPatch = true)]
        public int? MinimumQuantity { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        [SerializeAs(Static.Id.CATEGORY, IsRequired = true, CanPatch = true)]
        public Stub<Category>? Category { get; }

        [DeserializeAs(Static.Asset.ORDER_NUMBER)]
        [SerializeAs(Static.Asset.ORDER_NUMBER, CanPatch = true)]
        public string? OrderNumber { get; }

        [DeserializeAs(Static.Asset.PURCHASE_DATE)]
        [SerializeAs(Static.Asset.PURCHASE_DATE, CanPatch = true)]
        public FormattedDate? PurchaseDate { get; }

        [DeserializeAs(Static.Asset.PURCHASE_COST)]
        [SerializeAs(Static.Asset.PURCHASE_COST, CanPatch = true)]
        public decimal? PurchaseCost { get; }

        [DeserializeAs(Static.Consumable.REMAINING, IsNonNullable = true)]
        public int RemainingQuantity { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        [SerializeAs(Static.Id.COMPANY, CanPatch = true)]
        public Stub<Company>? Company { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.Asset.USER_CAN_CHECKOUT, IsNonNullable = true)]
        [JsonConverter(typeof(IntegerBooleanConverter))]
        public bool UserCanCheckout { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialComponent.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Checkout { get; }
            public bool Checkin { get; }
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Component(PartialComponent partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            Category = partial.Category;
            Company = partial.Company;
            Image = partial.Image;
            Location = partial.Location;
            MinimumQuantity = partial.MinimumQuantity;
            OrderNumber = partial.OrderNumber;
            PurchaseCost = partial.PurchaseCost;
            PurchaseDate = partial.PurchaseDate;
            Quantity = partial.Quantity;
            RemainingQuantity = partial.RemainingQuantity;
            SerialNumber = partial.SerialNumber;
            UserCanCheckout = partial.UserCanCheckout;

            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [GenerateFilter(typeof(ComponentSortOn))]
    public sealed partial class ComponentFilter: IFilter<Component>
    {
        [SerializeAsString(Static.Id.COMPANY)]
        public IApiObject<Company>? Company { get; set; }

        [SerializeAsString(Static.Id.CATEGORY)]
        public IApiObject<Category>? Category { get; set; }

        [SerializeAsString(Static.Id.LOCATION)]
        public IApiObject<Location>? Location { get; set; }
    }

    [SortColumn]
    public enum ComponentSortOn
    {
        [EnumMember(Value = Static.CREATED_AT)]
        CreatedAt = 0,

        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.Accessory.MINIMUM_AMOUNT)]
        MinimumQuantity,

        [EnumMember(Value = Static.Asset.ORDER_NUMBER)]
        OrderNumber,

        [EnumMember(Value = Static.SERIAL)]
        SerialNumber,

        [EnumMember(Value = Static.Asset.PURCHASE_DATE)]
        PurchaseDate,

        [EnumMember(Value = Static.Asset.PURCHASE_COST)]
        PurchaseCost,

        [EnumMember(Value = Static.Types.COMPANY)]
        Company,

        [EnumMember(Value = Static.Types.CATEGORY)]
        Category,

        [EnumMember(Value = Static.QUANTITY)]
        Quantity,

        [EnumMember(Value = Static.Types.LOCATION)]
        Location,

        [EnumMember(Value = Static.IMAGE)]
        Image
    }
}
