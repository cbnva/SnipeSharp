using System.Collections.Generic;
using System.Threading.Tasks;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public abstract class EndPoint<T>
        where T: class, IApiObject<T>
    {
        protected readonly SnipeItApi Api;
        protected readonly string BaseUri;

        internal EndPoint(SnipeItApi api, string baseUri)
        {
            Api = api;
            BaseUri = baseUri;
        }

        public Task<T?> GetAsync(IApiObject<T> origin) => GetAsync(origin.Id);
        public Task<T?> GetAsync(int id) => Api.Client.Get<T>($"{BaseUri}/{id}");

        public async IAsyncEnumerable<T?> GetAllAsync(IEnumerable<IApiObject<T>> ids)
        {
            foreach(var id in ids)
                yield return await GetAsync(id);
        }
        public async IAsyncEnumerable<T?> GetAllAsync(IEnumerable<int> ids)
        {
            foreach(var id in ids)
                yield return await GetAsync(id);
        }

        public Task<DataTable<T>?> FindAsync() => FindAsync(new BasicFilter<T>());
        public Task<DataTable<T>?> FindAsync(IFilter<T> filter) => Api.FindAsync(BaseUri, filter);

        public Task<ApiResult<T?>?> CreateAsync(IPostable<T> properties)
            => Api.Client.Post<T>(BaseUri, properties);

        public Task<ApiResult<T?>?> SetAsync(IApiObject<T> item, IPutable<T> properties)
            => Api.Client.Put<T>($"{BaseUri}/{item.Id}", properties);

        public Task<ApiResult<T?>?> UpdateAsync(T item, IPatchable<T> properties)
            => Api.Client.Patch<T>($"{BaseUri}/{item.Id}", properties, item);

        public Task<SimpleApiResult?> DeleteAsync(IApiObject<T> item)
            => Api.Client.Delete($"{BaseUri}/{item.Id}");
    }
}
