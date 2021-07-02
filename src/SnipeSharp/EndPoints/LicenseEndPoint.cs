using System;
using System.Threading.Tasks;
using SnipeSharp.Exceptions;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class LicenseEndPoint: EndPoint<License>
    {
        internal LicenseEndPoint(SnipeItApi api): base(api, "licenses")
        {
        }

        public Task<SelectList<License>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<License>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");

        public Task<DataTable<LicenseSeat>?> FindSeatsAsync(IApiObject<License> license)
            => FindSeatsAsync(license, new BasicFilter<LicenseSeat>());
        public Task<DataTable<LicenseSeat>?> FindSeatsAsync(IApiObject<License> license, IFilter<LicenseSeat> filter)
            => Api.FindAsync($"{BaseUri}/{license.Id}/seats", filter);
    }
}
