using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    public sealed class GetCmdletGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new GetCmdletReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not GetCmdletReceiver receiver)
                return;
            foreach(var target in receiver.Targets)
            {
                context.AddSource(target.Name + "_GetCmdlet.generated", SourceText.From($@"
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{{
    [Cmdlet(VerbsCommon.Get, ""SnipeIt{target.Name}"")]
    [OutputType(typeof({target.ModelType}))]
    public sealed class GetSnipeIt{target.Name}Cmdlet: AsyncCmdlet
    {{
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        public {target.BindingType} Identity {{ get; set; }}

        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client {{ get; set; }} = default!;

        protected override void BeginProcessing()
        {{
            Client = Client ?? ApiHelper.Instance;
        }}

        protected override async Task ProcessRecordAsync()
        {{
            WriteObject(await Client.{target.EndPointPropertyName}.GetAsync(Identity));
        }}
    }}
}}
", Encoding.UTF8));
            }
        }

    }
}
