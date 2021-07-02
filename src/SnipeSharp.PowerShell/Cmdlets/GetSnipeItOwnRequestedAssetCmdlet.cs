using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "SnipeItOwnRequestedAsset")]
    public sealed class GetSnipeItOwnRequestedAssetCmdlet: AsyncCmdlet
    {
        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client { get; set; } = default!;

        [Parameter]
        public SwitchParameter AsCollection { get; set; }

        protected override void BeginProcessing()
        {
            Client = Client ?? ApiHelper.Instance;
        }

        protected override async Task EndProcessingAsync()
        {
            WriteObject(await Client.Account.FindRequestedAssetsAsync(), !AsCollection);
        }
    }
}
