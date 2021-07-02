using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    internal sealed class NewCmdletGenerator: ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new UpdateCmdletReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not UpdateCmdletReceiver receiver)
                return;
            foreach(var target in receiver.Targets.Where(a => null != a.PostType))
            {
                var builder = new StringBuilder($@"
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{{
    [Cmdlet(VerbsCommon.New, ""SnipeIt{target.Model.Name}"")]
    [OutputType(typeof({target.Model.ToDisplayString()}))]
    public sealed class NewSnipeIt{target.Model.Name}Cmdlet: PSAsyncCmdlet
    {{
        [Parameter]
        [ValidateNotNull]
        public SnipeItApi Client {{ get; set; }} = default!;
");
                foreach(var property in target.PostProperties)
                {
                    var type = (property.Type.TryGetAnyGenericInterface("SnipeSharp.IApiObject", out var genericType)
                                && receiver.ModelBindingMap.TryGetValue(genericType.ToDisplayString(), out var bindingType))
                                    ? bindingType
                                    : property.Type.ToDisplayString().TrimEnd('?');
                    if(property.IsRequired){
                        builder.Append($@"
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
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
            var property = new {target.PostType!.ToDisplayString()}(");
                var separator = "";
                foreach(var property in target.PostProperties.Where(a => a.IsRequired))
                {
                    builder.Append($"{separator}{property.Name.FirstToLower()}: {property.Name}");
                    separator = ", ";
                }
                builder.AppendLine(");");

                foreach(var property in target.PostProperties.Where(a => !a.IsRequired))
                {
                    builder.Append($@"
            if(MyInvocation.BoundParameters.ContainsKey(nameof({property.Name})))
                property.{property.Name} = {property.Name};
");
                }
                builder.Append($@"
            var results = await Client.{target.EndPointPropertyName}.CreateAsync(property);
            if(null == results)
                throw new ApiReturnedNullException();
            WriteObject(results);
        }}
    }}
}}
");
                context.AddSource(target.Binding.Name + "_NewCmdlet.generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }
    }
}
