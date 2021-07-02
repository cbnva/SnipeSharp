using System;

namespace SnipeSharp.PowerShell
{
    internal class ApiConnectionException : Exception
    {
        public Uri Uri { get; }
        public ApiConnectionException(Uri uri): base($"Encountered an error while connecting to SnipeIt at Uri \"{uri}\".")
        {
            Uri = uri;
        }
    }
}
