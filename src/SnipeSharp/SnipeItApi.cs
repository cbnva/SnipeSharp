using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SnipeSharp.Client;
using SnipeSharp.Exceptions;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public struct TestConnectionResult
    {
        public const HttpStatusCode GENERAL_ERROR = (HttpStatusCode)601;
        public readonly HttpStatusCode Result;
        internal TestConnectionResult(HttpStatusCode status)
            => Result = status;

        public static implicit operator bool(TestConnectionResult result)
            => result.Result == HttpStatusCode.OK;
        public static bool operator ==(TestConnectionResult a, TestConnectionResult b)
            => a.Result == b.Result;
        public static bool operator !=(TestConnectionResult a, TestConnectionResult b)
            => a.Result != b.Result;
        public override bool Equals(object? obj)
            => obj is TestConnectionResult a ? a.Result == this.Result : false;
        public override int GetHashCode()
            => this.Result.GetHashCode();

        public static TestConnectionResult OK { get; } = new TestConnectionResult(HttpStatusCode.OK);
        public static TestConnectionResult ERROR { get; } = new TestConnectionResult(TestConnectionResult.GENERAL_ERROR);
    }

    public sealed class SnipeItApi: IDisposable
    {
        public readonly ISnipeItClient Client;

        public SnipeItApi(Uri uri, string token): this(new SnipeItHttpClient(uri, token))
        {
        }

        internal SnipeItApi(ISnipeItClient client)
        {
            Client = client;

            // special endpoints
            Account = new AccountEndPoint(this);
            Settings = new SettingsEndPoint(this);
            Reports = new ReportEndPoint(this);

            // normal endpoints
            _endpoints = new Dictionary<Type, object>
            {
                [typeof(Models.Accessory)] = Accessories = new AccessoryEndPoint(this),
                [typeof(Models.Asset)] = Assets = new AssetEndPoint(this),
                [typeof(Models.Category)] = Categories = new CategoryEndPoint(this),
                [typeof(Models.Company)] = Companies = new CompanyEndPoint(this),
                [typeof(Models.Component)] = Components = new ComponentEndPoint(this),
                //[typeof(Models.Consumable)] = Consumables = new ConsumableEndPoint(this),
                //[typeof(Models.Department)] = Departments = new DepartmentEndPoint(this),
                [typeof(Models.Depreciation)] = Depreciations = new DepreciationEndPoint(this),
                //[typeof(Models.Field)] = Fields = new FieldEndPoint(this),
                //[typeof(Models.FieldSet)] = FieldSets = new FieldSetEndPoint(this),
                [typeof(Models.Group)] = Groups = new GroupEndPoint(this),
                //[typeof(Models.Import)] = Imports = new ImportEndPoint(this),
                //[typeof(Models.Kit)] = Kits = new KitEndPoint(this),
                [typeof(Models.License)] = Licenses = new LicenseEndPoint(this),
                [typeof(Models.Location)] = Locations = new LocationEndPoint(this),
                [typeof(Models.Manufacturer)] = Manufacturers = new ManufacturerEndPoint(this),
                [typeof(Models.Maintenance)] = Maintenance = new MaintenanceEndPoint(this),
                //[typeof(Models.Model)] = Models = new ModelEndPoint(this),
                [typeof(Models.StatusLabel)] = StatusLabels = new StatusLabelEndPoint(this),
                [typeof(Models.Supplier)] = Suppliers = new SupplierEndPoint(this),
                [typeof(Models.User)] = Users = new UserEndPoint(this),
            };
        }

        public async Task<TestConnectionResult> TestConnection()
        {
            try {
                await Users.Me;
                return TestConnectionResult.OK;
            } catch(ApiHttpException ex)
            {
                return new TestConnectionResult(ex.StatusCode);
            } catch
            {
                return TestConnectionResult.ERROR;
            }
        }

        void IDisposable.Dispose()
        {
            if(Client is IDisposable d)
                d.Dispose();
        }

        public readonly AccountEndPoint Account;
        public readonly SettingsEndPoint Settings;
        public readonly ReportEndPoint Reports;

        private Dictionary<Type, object> _endpoints;
        public EndPoint<T> GetEndPoint<T>() where T: class, IApiObject<T>
            => _endpoints[typeof(T)] as EndPoint<T> ?? throw new InvalidCastException();

        public readonly AccessoryEndPoint Accessories;
        public readonly AssetEndPoint Assets;
        public readonly CategoryEndPoint Categories;
        public readonly CompanyEndPoint Companies;
        public readonly ComponentEndPoint Components;
        //public readonly ConsumableEndPoint Consumables;
        //public readonly DepartmentEndPoint Departments;
        public readonly DepreciationEndPoint Depreciations;
        public readonly GroupEndPoint Groups;
        public readonly LicenseEndPoint Licenses;
        public readonly LocationEndPoint Locations;
        public readonly ManufacturerEndPoint Manufacturers;
        public readonly MaintenanceEndPoint Maintenance;
        //public readonly ModelEndPoint Models;
        public readonly UserEndPoint Users;
        public readonly StatusLabelEndPoint StatusLabels;
        public readonly SupplierEndPoint Suppliers;
    }

    internal static class SnipeItApiExtensions
    {
        public static async Task<DataTable<K>?> FindAsync<K>(this SnipeItApi api, string baseUri, IFilter<K> filter)
            where K: class, IApiObject<K>
        {
            // duplicate the filter so if we change properties the original isn't affected.
            filter = filter.Clone();

            // get initial batch
            var result = await api.Client.Get<DataTable<K>>($"{baseUri}?{filter.GetParameters().AsQueryParameters()}");
            if(null == result)
                return null;
            var baseOffset = filter.Offset ?? 0;
            // if we already have what we need, leave.
            if(null != filter.Limit || baseOffset + result.Count >= result.Total)
                return result;

            // otherwise, get subsequent batches.
            filter.Limit = null; // use limit hard-coded in request URI
            filter.Offset = null; // use offset variable below, encoded directly into request URI
            var offset = baseOffset + result.Count;// start here.
            var parameters = filter.GetParameters().AsQueryParameters();
            if(parameters.Length > 0)
                parameters = "&" + parameters;
            while(baseOffset + result.Count < result.Total)
            {
                var batch = await api.Client.Get<DataTable<K>>($"{baseUri}?limit=1000&offset={offset}{parameters}");
                // these shouldn't happen -- if it did, something went wrong.
                if(null == batch)
                    throw new ApiNullException();
                if(0 == batch.Count)
                    throw new ApiReturnedInsufficientValuesForRequestException();

                // add the batch to the ongoing result and increase offset.
                result.Value.AddRange(batch.Value);
                offset += batch.Count;
            }

            return result;
        }
    }
}
