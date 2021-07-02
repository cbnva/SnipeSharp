using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(AccessoryCheckoutUserConverter))]
    [GeneratePartial, GenerateConverter]
    public sealed partial class AccessoryCheckoutUser : IApiObject<User>, IApiObject<AccessoryCheckoutUser>
    {
        [DeserializeAs(Static.ASSIGNED_PIVOT_ID)]
        public int AssignedPivotId { get; }

        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.USERNAME)]
        public string Username { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.User.FIRST_NAME)]
        public string FirstName { get; }

        [DeserializeAs(Static.User.LAST_NAME)]
        public string LastName { get; }

        [DeserializeAs(Static.User.EMPLOYEE_NUMBER)]
        public string? EmployeeNumber { get; }

        [DeserializeAs(Static.CHECKOUT_NOTES)]
        public string? CheckoutNotes { get; }

        [DeserializeAs(Static.LAST_CHECKOUT)]
        public FormattedDateTime LastCheckout { get; }

        // TODO: type: 'user'

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialAccessoryCheckoutUser.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool CheckIn { get; }
        }

        internal AccessoryCheckoutUser(PartialAccessoryCheckoutUser partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));

            AssignedPivotId = partial.AssignedPivotId ?? throw new ArgumentNullException(nameof(AssignedPivotId));
            Username = partial.Username ?? throw new ArgumentNullException(nameof(Username));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            FirstName = partial.FirstName ?? throw new ArgumentNullException(nameof(FirstName));
            LastName = partial.LastName ?? throw new ArgumentNullException(nameof(LastName));
            LastCheckout = partial.LastCheckout ?? throw new ArgumentNullException(nameof(LastCheckout));
            AvailableActions = new Actions(partial.AvailableActions);

            EmployeeNumber = partial.EmployeeNumber;
            CheckoutNotes = partial.CheckoutNotes;
        }
    }
}
