using System.Threading.Tasks;
using SnipeSharp;
using SnipeSharp.Exceptions;
using SnipeSharp.Models;

namespace SnipeSharp
{
    public sealed class SettingsEndPoint
    {
        private readonly SnipeItApi Api;

        internal SettingsEndPoint(SnipeItApi api)
            => Api = api;

        public Task<DataTable<LoginAttempt>?> FindLoginAttemptsAsync()
            => FindLoginAttemptsAsync(new BasicFilter<LoginAttempt>());
        public Task<DataTable<LoginAttempt>?> FindLoginAttemptsAsync(IFilter<LoginAttempt> filter)
            => Api.FindAsync("settings/login-attempts", filter);
    }
}
