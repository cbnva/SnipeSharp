using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    public sealed class RemoveCmdletGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new RemoveCmdletReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not RemoveCmdletReceiver receiver)
                return;
            foreach(var target in receiver.Targets)
            {
                context.AddSource(target.Name + "_RemoveCmdlet.generated", SourceText.From($@"
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{{
    [Cmdlet(VerbsCommon.Remove, ""SnipeIt{target.Name}"")]
    [OutputType(typeof(SnipeSharp.Models.SimpleApiResult))]
    public sealed class RemoveSnipeIt{target.Name}Cmdlet: AsyncCmdlet
    {{
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        public {target.BindingType} Identity {{ get; set; }}

        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client {{ get; set; }} = default!;

        [Parameter]
        public SwitchParameter PassThruResult {{ get; set; }}

        protected override void BeginProcessing()
        {{
            Client = Client ?? ApiHelper.Instance;
        }}

        protected override async Task ProcessRecordAsync()
        {{
            var item = Identity.Object ?? await Client.{target.EndPointPropertyName}.GetAsync(Identity);
            var result = await Client.{target.EndPointPropertyName}.DeleteAsync(item);
            if(PassThruResult)
                WriteObject(result);
        }}
    }}
}}
", Encoding.UTF8));
            }
        }

    }
}
