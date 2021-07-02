using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(AssetRequestConverter))]
    [GeneratePartial, GenerateConverter]
    public sealed class AssetRequest
    {
        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        [DeserializeAs(Static.TYPE)]
        public RequestableType Type { get; }

        [DeserializeAs(Static.QUANTITY)]
        public int Quantity { get; }

        [DeserializeAs(Static.IMAGE)]
        public Uri? Image { get; }

        [DeserializeAs(Static.Types.LOCATION)]
        public string? LocationName { get; }

        [DeserializeAs(Static.EXPECTED_CHECKIN)]
        public FormattedDateTime? ExpectedCheckin { get; }

        [DeserializeAs(Static.Request.REQUEST_DATE)]
        public FormattedDateTime RequestDate { get; }

        internal AssetRequest(PartialAssetRequest partial)
        {
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
            Type = partial.Type ?? throw new ArgumentNullException(nameof(Type));
            Image = partial.Image;
            LocationName = partial.LocationName;
            ExpectedCheckin = partial.ExpectedCheckin;
            RequestDate = partial.RequestDate ?? throw new ArgumentNullException(nameof(RequestDate));
        }
    }


    [JsonConverter(typeof(EnumJsonConverter<RequestableType>))]
    public enum RequestableType
    {
        [EnumMember(Value = Static.Types.ASSET)]
        Asset
        // TODO
    }
}
