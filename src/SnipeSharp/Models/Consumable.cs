using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(ConsumableConverter))]
    [GenerateConverter, GeneratePartial]
    public sealed partial class Consumable: IApiObject<Consumable>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        public Stub<Category>? Category { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        public Stub<Company>? Company { get; }

        [DeserializeAs(Static.Consumable.ITEM_NO)]
        public string ItemNumber { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.Types.MANUFACTURER)]
        public Stub<Manufacturer>? Manufacturer { get; }

        [DeserializeAs(Static.Accessory.MINIMUM_AMOUNT, IsNonNullable = true)]
        public int MinimumQuantity { get; }

        [DeserializeAs(Static.MODEL_NUMBER)]
        public string? ModelNumber { get; }

        [DeserializeAs(Static.Consumable.REMAINING, IsNonNullable = true)]
        public int RemainingQuantity { get; }

        [DeserializeAs(Static.Asset.ORDER_NUMBER)]
        public string? OrderNumber { get; }

        [DeserializeAs(Static.Asset.PURCHASE_COST)]
        public decimal? PurchaseCost { get; }

        [DeserializeAs(Static.Asset.PURCHASE_DATE)]
        public FormattedDate? PurchaseDate { get; }

        [DeserializeAs(Static.QUANTITY, IsNonNullable = true)]
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
            ItemNumber = partial.ItemNumber ?? throw new ArgumentNullException(nameof(ItemNumber));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

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
}
