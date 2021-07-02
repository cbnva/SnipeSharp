using System;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class CategoryEndPoint: EndPoint<Category>
    {
        internal CategoryEndPoint(SnipeItApi api): base(api, "categories"){}

        public Task<SelectList<Company>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Company>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");
    }
}
