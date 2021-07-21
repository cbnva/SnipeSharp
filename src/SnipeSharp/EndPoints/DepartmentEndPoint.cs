using System;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class DepartmentEndPoint: EndPoint<Department>
    {
        internal DepartmentEndPoint(SnipeItApi api): base(api, "departments")
        {
        }

        public Task<SelectList<Company>?> SelectAsync(string? search = null)
            => Api.Client.Get<SelectList<Company>>($"{BaseUri}/selectlist{((null == search) ? "" : $"?search={Uri.EscapeUriString(search)}")}");
    }
}
