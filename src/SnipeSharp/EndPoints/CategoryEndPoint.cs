using System;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class CompanyEndPoint: EndPoint<Company>
    {
        internal CompanyEndPoint(SnipeItApi api): base(api, "companies"){}

        public Task<SelectList<Category>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Category>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");
    }
}
