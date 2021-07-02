using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class AccessoryEndPoint: EndPoint<Accessory> {
        internal AccessoryEndPoint(SnipeItApi api): base(api, "accessories"){}

        public Task<SelectList<Accessory>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Accessory>>($"{BaseUri}/selectlist{(null == search ? "?" : "")}{search}");

        public Task<DataTable<AccessoryCheckoutUser>?> FindCheckedOutUsers(IApiObject<Accessory> accessory)
            => FindCheckedOutUsers(accessory, new BasicFilter<AccessoryCheckoutUser>());
        public Task<DataTable<AccessoryCheckoutUser>?> FindCheckedOutUsers(IApiObject<Accessory> accessory, IFilter<AccessoryCheckoutUser> filter)
            => Api.FindAsync($"{BaseUri}/{accessory.Id}/checkedout", filter);
    }
}

