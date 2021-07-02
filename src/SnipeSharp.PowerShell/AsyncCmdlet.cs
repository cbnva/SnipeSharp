using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{
    public abstract class AsyncCmdlet: Cmdlet
    {
        protected override void BeginProcessing() => AsyncContext.RunSync(BeginProcessingAsync());
        protected override void ProcessRecord() => AsyncContext.RunSync(ProcessRecordAsync());
        protected override void EndProcessing() => AsyncContext.RunSync(EndProcessingAsync());

        protected virtual Task BeginProcessingAsync() => Task.CompletedTask;
        protected virtual Task ProcessRecordAsync() => Task.CompletedTask;
        protected virtual Task EndProcessingAsync() => Task.CompletedTask;
    }
}
