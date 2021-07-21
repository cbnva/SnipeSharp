using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(DepartmentConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Department : IApiObject<Department>
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

        [DeserializeAs(Static.MANAGER)]
        [SerializeAs(Static.Id.MANAGER, CanPatch = true)]
        public StubUser? Manager { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        [SerializeAs(Static.Id.LOCATION, CanPatch = true)]
        public Stub<Location>? Location { get; }

        [DeserializeAs(Static.Count.USERS)]
        public string UsersCount { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialDepartment.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
        }

        internal Department(PartialDepartment partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Image = partial.Image;
            Company = partial.Company;
            Manager = partial.Manager;
            Location = partial.Location;
            UsersCount = partial.UsersCount ?? throw new ArgumentNullException(nameof(UsersCount));
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [GenerateFilter(typeof(DepartmentSortOn))]
    public sealed partial class DepartmentFilter: IFilter<Department>
    {
    }

    [SortColumn]
    public enum DepartmentSortOn
    {
        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.IMAGE)]
        Image,

        [EnumMember(Value = Static.Count.USERS)]
        UserCount,

        [EnumMember(Value = Static.Types.LOCATION)]
        Location,

        [EnumMember(Value = Static.MANAGER)]
        Manager
    }
}
