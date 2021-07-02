using System;
using System.Management.Automation;

namespace SnipeSharp.PowerShell
{
    [Cmdlet(VerbsCommon.New, "SnipeItClient")]
    public sealed class NewSnipeItClientCmdlet: Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNull]
        public Uri Uri { get; set; } = default!;

        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Token { get; set; } = default!;

        protected override void EndProcessing() => WriteObject(new SnipeItApi(Uri, Token));
    }
}
