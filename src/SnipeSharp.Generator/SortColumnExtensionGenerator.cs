using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.Generator
{
    [Generator]
    public sealed class SortColumnExtensionGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new SortColumnSyntaxReceiver());

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not SortColumnSyntaxReceiver receiver)
                return;

            foreach(var sortEnum in receiver.SortColumnEnums)
            {
                var builder = new StringBuilder($@"
namespace {sortEnum.Namespace}
{{
    internal static class {sortEnum.Name}Extensions
    {{
        internal static string? Serialize(this {sortEnum.FullName} value)
            => value switch
            {{");

                foreach(var member in sortEnum.Values)
                    builder.Append($@"
                {sortEnum.FullName}.{member.Name} => ""{member.Key}"",");

                builder.Append(@"
                _ => null
            };
    }
}
");
                context.AddSource($"{sortEnum.Name}_generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }
    }
}
