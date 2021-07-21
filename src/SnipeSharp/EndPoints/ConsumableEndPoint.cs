using System;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class ConsumableEndPoint: EndPoint<Consumable>
    {
        internal ConsumableEndPoint(SnipeItApi api): base(api, "consumables")
        {
        }

        public Task<SelectList<Company>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Company>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");

        // TODO: /view/{id}/users
        // TODO: POST /{id}/checkout
    }
}
