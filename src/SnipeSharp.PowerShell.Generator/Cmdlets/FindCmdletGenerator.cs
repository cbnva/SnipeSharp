using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    public sealed class FindCmdletGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new FindCmdletReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not FindCmdletReceiver receiver)
                return;
            if(receiver.Targets.Count == 0)
                throw new Exception("Empty!");
            foreach(var target in receiver.Targets)
            {
                var builder = new StringBuilder($@"
#nullable enable
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{{
    [Cmdlet(VerbsCommon.Get, ""SnipeIt{target.Model.Name}"", SupportsPaging = true)]
    [OutputType(typeof({target.Model.Name}))]
    public sealed class FindSnipeIt{target.Model.Name}Cmdlet: PSAsyncCmdlet
    {{
        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client {{ get; set; }} = default!;

        [Parameter]
        public SwitchParameter AsCollection {{ get; set; }}
");
                foreach(var property in target.Properties.Values)
                {
                    var type = (property.Type.TryGetAnyGenericInterface("SnipeSharp.IApiObject", out var genericType)
                                && receiver.ModelBindingMap.TryGetValue(genericType.ToDisplayString(), out var bindingType))
                                    ? bindingType
                                    : property.Type.ToDisplayString().TrimEnd('?');
                    builder.Append($@"
        [Parameter{(property.Name == "SearchString" ? "(Position = 0)" : "")}]
        public {type} {property.Name} {{ get; set; }} = default!;
");
                }
                builder.Append($@"
        protected override void BeginProcessing()
        {{
            Client = Client ?? ApiHelper.Instance;
        }}

        protected override async Task ProcessRecordAsync()
        {{
            var filter = new {target.Filter.ToDisplayString()}();
");
                foreach(var property in target.Properties.Values)
                {
                    builder.Append($@"
            if(MyInvocation.BoundParameters.ContainsKey(nameof({property.Name})))
                filter.{property.Name} = {property.Name};
");
                }
                builder.Append($@"
            if(MyInvocation.BoundParameters.ContainsKey(nameof(PagingParameters.First)))
                filter.Limit = (int) PagingParameters.First;
            if(MyInvocation.BoundParameters.ContainsKey(nameof(PagingParameters.Skip)))
                filter.Offset = (int) PagingParameters.Skip;
            var results = await Client.{target.EndPointPropertyName}.FindAsync(filter);
            if(null == results)
                throw new ApiReturnedNullException();
            if(PagingParameters.IncludeTotalCount)
                WriteObject(results.Total);
            WriteObject(results, !AsCollection);
        }}
    }}
}}
");
                context.AddSource(target.Binding.Name + "_FindCmdlet.generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }

    }
}
