using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(LicenseSeatConverter))]
    [GeneratePartial, GenerateConverter]
    public sealed partial class LicenseSeat: IApiObject<LicenseSeat>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.Id.LICENSE)]
        public IApiObject<License> LicenseId { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.LicenseSeat.ASSIGNED_USER)]
        public StubLicenseSeatUser? AssignedUser { get; }

        [DeserializeAs(Static.LicenseSeat.ASSIGNED_ASSET)]
        public Stub<Asset>? AssignedAsset { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.LicenseSeat.REASSIGNABLE, IsNonNullable = true)]
        public bool IsReassignable { get; }

        [DeserializeAs(Static.Asset.USER_CAN_CHECKOUT, IsNonNullable = true)]
        public bool UserCanCheckout { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialLicenseSeat.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool CheckOut { get; }
            public bool CheckIn { get; }
            public bool Clone { get; }
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal LicenseSeat(PartialLicenseSeat partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            LicenseId = partial.LicenseId ?? throw new ArgumentNullException(nameof(LicenseId));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            AssignedUser = partial.AssignedUser;
            AssignedAsset = partial.AssignedAsset;
            Location = partial.Location;
            IsReassignable = partial.IsReassignable;
            UserCanCheckout = partial.UserCanCheckout;
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    public sealed partial class StubLicenseSeatUser: IApiObject<User>
    {
        public int Id { get; }

        // TODO
    }
}
