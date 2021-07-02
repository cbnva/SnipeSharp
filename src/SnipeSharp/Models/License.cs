using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(LicenseConverter))]
    [GeneratePartial, GenerateConverter]
    public sealed partial class License: IApiObject<License>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.Types.COMPANY)]
        public Stub<Company>? Company { get; }

        [DeserializeAs(Static.Types.MANUFACTURER)]
        public Stub<Manufacturer>? Manufacturer { get; } // required on form

        [DeserializeAs(Static.License.PRODUCT_KEY)]
        public string? ProductKey { get; }

        [DeserializeAs(Static.Asset.ORDER_NUMBER)]
        public string? OrderNumber { get; }

        [DeserializeAs(Static.License.PURCHASE_ORDER)]
        public string? PurchaseOrder { get; }

        [DeserializeAs(Static.Asset.PURCHASE_DATE)]
        public FormattedDate? PurchaseDate { get; }

        [DeserializeAs(Static.License.TERMINATION_DATE)]
        public FormattedDate? TerminationDate { get; }

        [DeserializeAs(Static.Types.DEPRECIATION)]
        public Stub<Depreciation>? Depreciation { get; }

        [DeserializeAs(Static.Asset.PURCHASE_COST)]
        public string? PurchaseCost { get; } // TODO: is this a number?

        [DeserializeAs(Static.NOTES)]
        public string? Notes { get; }

        [DeserializeAs(Static.License.EXPIRATION_DATE)]
        public FormattedDate? ExpirationDate { get; }

        [DeserializeAs(Static.License.SEATS, IsNonNullable = true)]
        public int Seats { get; } // required on form

        [DeserializeAs(Static.License.FREE_SEATS_COUNT, IsNonNullable = true)]
        public int FreeSeats { get; }

        [DeserializeAs(Static.License.LICENSE_NAME)]
        public string? LicenseName { get; }

        [DeserializeAs(Static.License.LICENSE_EMAIL)]
        public string? LicenseEmail { get; }

        [DeserializeAs(Static.LicenseSeat.REASSIGNABLE, IsNonNullable = true)]
        public bool IsReassignable { get; }

        [DeserializeAs(Static.License.MAINTAINED, IsNonNullable = true)]
        public bool IsMaintained { get; }

        [DeserializeAs(Static.Types.SUPPLIER)]
        public Stub<Supplier>? Supplier { get; }

        [DeserializeAs(Static.Types.CATEGORY)]
        public Stub<Category>? Category { get; } // required on form

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.Asset.USER_CAN_CHECKOUT, IsNonNullable = true)]
        public bool UserCanCheckout { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialLicense.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Checkout { get; }
            public bool Checkin { get; }
            public bool Clone { get; }
            public bool Update { get; }
            public bool Deleted { get; }
        }

        internal License(PartialLicense partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));

            Company = partial.Company;
            Manufacturer = partial.Manufacturer;
            ProductKey = partial.ProductKey;
            PurchaseOrder = partial.PurchaseOrder;
            PurchaseDate = partial.PurchaseDate;
            TerminationDate = partial.TerminationDate;
            Depreciation = partial.Depreciation;
            PurchaseCost = partial.PurchaseCost;
            Notes = partial.Notes;
            ExpirationDate = partial.ExpirationDate;
            LicenseName = partial.LicenseName;
            LicenseEmail = partial.LicenseEmail;
            Supplier = partial.Supplier;
            Category = partial.Category;

            Seats = partial.Seats;
            FreeSeats = partial.FreeSeats;
            IsReassignable = partial.IsReassignable;
            IsMaintained = partial.IsMaintained;
            UserCanCheckout = partial.UserCanCheckout;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }
}
