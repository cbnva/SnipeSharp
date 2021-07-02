using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(LocationConverter))]
    [GeneratePartial, GenerateConverter, GeneratePostable]
    public sealed partial class Location: IApiObject<Location>, IEnumerable<Stub<Location>>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        [SerializeAs(Static.NAME, IsRequired = true, CanPatch = true)]
        public string Name { get; }

        [DeserializeAs(Static.IMAGE)]
        [SerializeAs(Static.IMAGE, CanPatch = true)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Location.ADDRESS)]
        [SerializeAs(Static.Location.ADDRESS, CanPatch = true)]
        public string? Address { get; }

        [DeserializeAs(Static.Location.ADDRESS2)]
        [SerializeAs(Static.Location.ADDRESS2, CanPatch = true)]
        public string? Address2 { get; }

        [DeserializeAs(Static.Location.CITY)]
        [SerializeAs(Static.Location.CITY, CanPatch = true)]
        public string? City { get; }

        [DeserializeAs(Static.Location.STATE)]
        [SerializeAs(Static.Location.STATE, CanPatch = true)]
        public string? State { get; }

        [DeserializeAs(Static.Location.COUNTRY)]
        [SerializeAs(Static.Location.COUNTRY, CanPatch = true)]
        public string? Country { get; }

        [DeserializeAs(Static.Location.ZIP)]
        [SerializeAs(Static.Location.ZIP, CanPatch = true)]
        public string? ZipCode { get; }

        [DeserializeAs(Static.Count.ASSIGNED_ASSETS, IsNonNullable = true)]
        public int AssignedAssetsCount { get; }

        [DeserializeAs(Static.Count.ASSETS, IsNonNullable = true)]
        public int AssetsCount { get; }

        [DeserializeAs(Static.Count.USERS, IsNonNullable = true)]
        public int UsersCount { get; }

        [DeserializeAs(Static.CURRENCY)]
        [SerializeAs(Static.CURRENCY, CanPatch = true)]
        public string? Currency { get; }

        [DeserializeAs(Static.CREATED_AT)]
        public FormattedDateTime CreatedAt { get; }

        [DeserializeAs(Static.UPDATED_AT)]
        public FormattedDateTime UpdatedAt { get; }

        [DeserializeAs(Static.Location.PARENT)]
        [SerializeAs(Static.Location.PARENT, CanPatch = true)]
        public Stub<Location>? ParentLocation { get; }

        [DeserializeAs(Static.MANAGER)]
        [SerializeAs(Static.MANAGER, CanPatch = true)]
        public StubUser? Manager { get; }

        [DeserializeAs(Static.Location.LDAP_OU)]
        [SerializeAs(Static.Location.LDAP_OU, CanPatch = true)]
        public string? LdapOu { get; }

        [DeserializeAs(Static.Location.CHILDREN)]
        public Stub<Location>[] ChildLocations { get; }

        [DeserializeAs(Static.AVAILABLE_ACTIONS, Type = typeof(PartialLocation.Actions), IsNonNullable = true)]
        public readonly Actions AvailableActions;

        [GeneratePartialActions]
        public partial struct Actions
        {
            public bool Update { get; }
            public bool Delete { get; }
        }

        IEnumerator<Stub<Location>> IEnumerable<Stub<Location>>.GetEnumerator()
            => ((IEnumerable<Stub<Location>>)ChildLocations).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ChildLocations.GetEnumerator();

        internal Location(PartialLocation partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Image = partial.Image;
            Address = partial.Address;
            Address2 = partial.Address2;
            City = partial.City;
            State = partial.State;
            Country = partial.Country;
            ZipCode = partial.ZipCode;
            AssignedAssetsCount = partial.AssignedAssetsCount;
            AssetsCount = partial.AssetsCount;
            UsersCount = partial.UsersCount;
            Currency = partial.Currency;
            CreatedAt = partial.CreatedAt ?? throw new ArgumentNullException(nameof(CreatedAt));
            UpdatedAt = partial.UpdatedAt ?? throw new ArgumentNullException(nameof(UpdatedAt));
            ParentLocation = partial.ParentLocation;
            Manager = partial.Manager;
            ChildLocations = partial.ChildLocations ?? Array.Empty<Stub<Location>>();
            AvailableActions = new Actions(partial.AvailableActions);
        }
    }

    [GenerateFilter(typeof(LocationSortOn))]
    public sealed partial class LocationFilter: IFilter<Location>
    {
    }

    [SortColumn]
    public enum LocationSortOn
    {
        [EnumMember(Value = Static.CREATED_AT)]
        CreatedAt = 0,

        [EnumMember(Value = Static.ID)]
        Id,

        [EnumMember(Value = Static.NAME)]
        Name,

        [EnumMember(Value = Static.Location.ADDRESS)]
        Address,

        [EnumMember(Value = Static.Location.ADDRESS2)]
        Address2,

        [EnumMember(Value = Static.Location.CITY)]
        City,

        [EnumMember(Value = Static.Location.STATE)]
        State,

        [EnumMember(Value = Static.Location.COUNTRY)]
        Country,

        [EnumMember(Value = Static.Location.ZIP)]
        Zip,

        [EnumMember(Value = Static.UPDATED_AT)]
        UpdatedAt,

        [EnumMember(Value = "manager_id")]
        ManagerId,

        [EnumMember(Value = Static.IMAGE)]
        Image,

        [EnumMember(Value = Static.Count.ASSIGNED_ASSETS)]
        AssignedAssetsCount,

        [EnumMember(Value = Static.Count.USERS)]
        UsersCount,

        [EnumMember(Value = Static.Count.ASSETS)]
        AssetsCount,

        [EnumMember(Value = Static.CURRENCY)]
        Currency,

        [EnumMember(Value = Static.Location.PARENT)]
        ParentLocation,

        [EnumMember(Value = Static.MANAGER)]
        Manager
    }
}
