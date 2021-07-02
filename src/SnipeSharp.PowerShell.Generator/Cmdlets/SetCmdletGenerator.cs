using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    internal sealed class SetCmdletGenerator: ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new UpdateCmdletReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not UpdateCmdletReceiver receiver)
                return;
            foreach(var target in receiver.Targets.Where(a => null != a.PutType && null != a.PatchType))
            {
                var builder = new StringBuilder($@"
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{{
    [Cmdlet(VerbsCommon.Set, ""SnipeIt{target.Model.Name}"", DefaultParameterSetName=""Patch"")]
    [OutputType(typeof({target.Model.ToDisplayString()}))]
    public sealed class SetSnipeIt{target.Model.Name}Cmdlet: PSAsyncCmdlet
    {{
        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client {{ get; set; }} = default!;

        [Parameter(Mandatory = true, ParameterSetName = ""Put"")]
        public SwitchParameter Replace {{ get; set; }}

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias(""Id"")]
        [ValidateNotNull]
        public {target.Binding.ToDisplayString()} Identity {{ get; set; }}
");
                foreach(var property in target.PutProperties)
                {
                    var type = (property.Type.TryGetAnyGenericInterface("SnipeSharp.IApiObject", out var genericType)
                                && receiver.ModelBindingMap.TryGetValue(genericType.ToDisplayString(), out var bindingType))
                                    ? bindingType
                                    : property.Type.ToDisplayString().TrimEnd('?');
                    if(property.IsRequired){
                        builder.Append($@"
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = ""Put"")]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = ""Patch"")]
        [ValidateNotNull{(property.Type.ToDisplayString() == "System.String" ? "OrEmpty" : "")}]
        public {type} {property.Name} {{ get; set; }} = default!;
");
                    } else {
                        builder.Append($@"
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public {type} {property.Name} {{ get; set; }} = default!;
");
                    }
                }

                builder.Append($@"
        protected override void BeginProcessing()
        {{
            Client = Client ?? ApiHelper.Instance;
        }}

        protected override async Task ProcessRecordAsync()
        {{
            if(Replace)
            {{
                var property = new {target.PutType!.ToDisplayString()}(");
                var separator = "";
                foreach(var property in target.PutProperties.Where(a => a.IsRequired))
                {
                    builder.Append($"{separator}{property.Name.FirstToLower()}: {property.Name}");
                    separator = ", ";
                }
                builder.AppendLine(");");

                foreach(var property in target.PutProperties.Where(a => !a.IsRequired))
                {
                    builder.Append($@"
                if(MyInvocation.BoundParameters.ContainsKey(nameof({property.Name})))
                    property.{property.Name} = {property.Name};
");
                }
                builder.Append($@"
                var results = await Client.{target.EndPointPropertyName}.SetAsync(Identity, property);
                if(null == results)
                    throw new ApiReturnedNullException();
                WriteObject(results);
            }} else
            {{
                var obj = await Client.{target.EndPointPropertyName}.GetAsync(Identity.Id);
                var patch = new {target.PatchType!.ToDisplayString()}(obj);
");
                foreach(var property in target.PatchProperties)
                {
                    builder.Append($@"
                if(MyInvocation.BoundParameters.ContainsKey(nameof({property.Name})))
                    patch.{property.Name} = {property.Name};
");
                }
                builder.Append($@"
                var results = await Client.{target.EndPointPropertyName}.UpdateAsync(obj, patch);
                if(null == results)
                    throw new ApiReturnedNullException();
                WriteObject(results);
            }}
        }}
    }}
}}
");
                context.AddSource(target.Binding.Name + "_SetCmdlet.generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }
    }
}
