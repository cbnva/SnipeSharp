using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.PowerShell.Generator
{
    [Generator]
    public sealed class GenerateBindingGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new GenerateBindingReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not GenerateBindingReceiver receiver)
                return;
            foreach(var target in receiver.Targets)
            {
                context.AddSource(target.Symbol.Name + "_Generated", SourceText.From($@"
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace {target.Symbol.ContainingNamespace}
{{
    public {(target.Symbol.IsSealed ? "sealed" : "")} partial class {target.Symbol.Name}
    {{
        public int Id {{ get; }}
        public bool HasObject {{ get; }}
        public {target.model}? Object {{ get; }}

        public {target.Symbol.Name}(int id)
        {{
            Id = id;
        }}

        public {target.Symbol.Name}(IApiObject<{target.model}> obj)
        {{
            if(null == obj)
                throw new ArgumentNullException(nameof(obj));
            Id = obj.Id;
            if(obj is {target.model} modelObject)
            {{
                HasObject = true;
                Object = modelObject;
            }}
        }}
    }}
}}
", Encoding.UTF8));
            }
        }

    }
}
