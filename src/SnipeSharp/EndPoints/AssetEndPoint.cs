using System;
using System.Threading.Tasks;
using SnipeSharp.Models;
using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp
{
    public sealed class AssetEndPoint: EndPoint<Company> {
        internal AssetEndPoint(SnipeItApi api): base(api, "hardware"){}

        public Task<Asset?> GetByTagAsync(string tag)
            => Api.Client.Get<Asset>($"{BaseUri}/bytag/{tag}");

        public Task<DataTable<Asset>?> FindBySerialNumberAsync(string serial)
            => Api.Client.Get<DataTable<Asset>>($"{BaseUri}/byserial/{serial}");

        public Task<SelectList<Asset>?> SelectAsync(string? search = null, SelectAssetStatusType? statusType = null)
        {
            var joiner = new StringJoiner("&");
            if(null != search)
                joiner.Append($"search={Uri.EscapeUriString(search)}");
            var serializedStatusType = statusType?.Serialize();
            if(null != serializedStatusType)
                joiner.Append($"assetStatusType={Uri.EscapeUriString(serializedStatusType)}");
            return Api.Client.Get<SelectList<Asset>>($"{BaseUri}/selectlist{(joiner.JoinedItemsCount > 0 ? "?" : string.Empty)}{joiner}");
        }
    }

    [SortColumn]
    public enum SelectAssetStatusType
    {
        [EnumMember(Value = "RTD")]
        ReadyToDeploy
    }
}
