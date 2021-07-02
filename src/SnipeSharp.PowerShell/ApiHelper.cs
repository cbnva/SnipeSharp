using System;

namespace SnipeSharp.PowerShell
{
    internal static class ApiHelper
    {
        public static SnipeItApi? InstanceField;
        public static SnipeItApi Instance
            => InstanceField ?? throw new InvalidOperationException("Not connected to an instance.");
    }
}
