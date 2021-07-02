using System;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class ComponentEndPoint: EndPoint<Component>
    {
        internal ComponentEndPoint(SnipeItApi api): base(api, "components"){}

        public Task<SelectList<Component>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Component>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");
    }
}
