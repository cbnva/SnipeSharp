using System.Management.Automation;

namespace SnipeSharp.PowerShell
{
    [Cmdlet(VerbsCommunications.Disconnect, Static.SnipeIt)]
    public sealed class DisconnectSnipeItCmdlet: Cmdlet
    {
        protected override void EndProcessing()
        {
            ApiHelper.InstanceField = null;
        }
    }
}
