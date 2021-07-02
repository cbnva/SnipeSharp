using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.Generator
{
    [Generator]
    public sealed class FilterGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new FilterSyntaxReceiver());

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not FilterSyntaxReceiver receiver)
                return;
            foreach(var filter in receiver.Definitions.Values)
            {
                var builder = new StringBuilder($@"
using System;
using System.Collections.Generic;
using SnipeSharp;

namespace {filter.Symbol.ContainingNamespace.ToDisplayString()}
{{
    { filter.Modifier } {(filter.Symbol.IsSealed ? "sealed" : "")} partial class {filter.Symbol.Name}
    {{
        public int? Limit {{ get; set; }}
        public int? Offset {{ get; set; }}
        public SortOrder? SortOrder {{ get; set; }}
");
                foreach(var property in filter.GeneratedProperties)
                    builder.Append($@"
        public {property.Type} {property.Name} {{ get; set; }}");
                foreach(var type in filter.FilterTypes)
                {
                    builder.Append($@"
        {type} {type}.Clone()
            => new {filter.Symbol.Name}
            {{
                Limit = Limit,
                Offset = Offset,
                SortOrder = SortOrder,");
                    foreach(var property in filter.Properties)
                        builder.Append($@"
                {property.Name} = {property.Name},");
                    builder.Append($@"
            }};

        IReadOnlyDictionary<string, string?> {type}.GetParameters()
            => new Dictionary<string, string?>()
                .AddIfNotNull(Static.LIMIT, Limit?.ToString())
                .AddIfNotNull(Static.OFFSET, Offset?.ToString())
                .AddIfNotNull(Static.ORDER, SortOrder?.Serialize())");
                    foreach(var property in filter.Properties)
                        builder.Append($@"
                .AddIfNotNull(""{property.Key}"", {property.ConvertWith})");
                    builder.Append(";");
                }
                builder.Append(@"
    }
}");
                context.AddSource(filter.Symbol.Name + "_generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }
    }
}
