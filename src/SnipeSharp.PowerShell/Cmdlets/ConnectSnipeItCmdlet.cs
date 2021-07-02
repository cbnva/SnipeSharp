using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{
    [Cmdlet(VerbsCommunications.Connect, Static.SnipeIt)]
    public sealed class ConnectSnipeItCmdlet: AsyncCmdlet
    {
        private enum ParameterSets
        {
            UseToken,
            UseClient
        }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = nameof(ParameterSets.UseToken))]
        [ValidateNotNull]
        public Uri Uri { get; set; } = default!;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = nameof(ParameterSets.UseToken))]
        [ValidateNotNullOrEmpty]
        public string Token { get; set; } = default!;

        [Parameter(Mandatory = true, ParameterSetName = nameof(ParameterSets.UseClient))]
        [ValidateNotNull]
        public SnipeItApi Client { get; set; } = default!;

        [Parameter]
        public SwitchParameter Force { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        [Parameter]
        public SwitchParameter SkipConnectionCheck { get; set; }

        protected override async Task EndProcessingAsync()
        {
            var instance = Client ?? new SnipeItApi(Uri, Token);
            if(instance != ApiHelper.InstanceField)
            {
                if(!SkipConnectionCheck && await instance.TestConnection() != TestConnectionResult.OK)
                    throw new ApiConnectionException(Uri);
                if(!Force && null != ApiHelper.InstanceField)
                    throw new InvalidOperationException("Cannot connect to an instance when already connected.");
                ApiHelper.InstanceField = instance;
            }
            if(PassThru)
                WriteObject(instance);
        }
    }
}
