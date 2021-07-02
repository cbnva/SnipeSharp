using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.Generator
{
    [Generator]
    public sealed class SerializationGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new SerializationSyntaxReceiver());

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not SerializationSyntaxReceiver receiver)
                return;
            foreach(var definition in receiver.Classes.Values)
            {
                var extraProperties = GetExtraPropertyArguments(definition);
                var builder = new StringBuilder($@"
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SnipeSharp;
using SnipeSharp.Serialization;

namespace {definition.Namespace}
{{
    {definition.Modifiers} static class {definition.Name}PropertyExtensions
    {{
        public static {definition.Name}Property AsProperty(this {definition.Name} {definition.Name.FirstToLower()}{extraProperties.Definition})
            => new {definition.Name}Property({definition.Name.FirstToLower()}{extraProperties.List});
    }}

    {definition.Modifiers} sealed partial class {definition.Name}Property : IPutable<{definition.FullName}>, IPostable<{definition.FullName}>");
                if(definition.AllPatchable)
                    builder.Append($@", IPatchable<{definition.FullName}>");
                builder.Append(@"
    {");
                GeneratePropertyProperties(builder, definition);
                GeneratePropertyConstructors(builder, definition, extraProperties);
                if(definition.AllPatchable)
                    GetToPatch(builder, definition);
                if(!definition.AllPatchable && definition.AnyPatchable)
                    GeneratePatchCastOperator(builder, definition);
                builder.Append(@"
    }
");

                if(!definition.AllPatchable && definition.AnyPatchable)
                {
                    var patchableProperties = definition.Properties.Where(a => a.CanPatch).ToList();
                    var source = definition.Name.FirstToLower();
                    builder.Append($@"
    {definition.Modifiers} sealed partial class {definition.Name}Patch : IPatchable<{definition.FullName}>
    {{

        public {definition.Name}Patch()
        {{
        }}

        public {definition.Name}Patch({definition.FullName} {source})
        {{");
                    foreach(var property in definition.Properties.Where(a => a.CanPatch))
                    {
                        builder.Append($@"
            {property.Name} = {source}.{property.Name};");
                    }
                    builder.Append($@"
        }}");
                    foreach(var property in patchableProperties)
                    {
                        builder.Append($@"
        [JsonPropertyName(""{property.Key}"")]
        public {property.Type.Nullable()} {property.Name} {{ get; set; }}");
                    }
                    GetToPatch(builder, definition);
                    builder.Append($@"
    }}
");
                }

                if(definition.AnyPatchable)
                {
                    var patchableProperties = definition.Properties.Where(a => a.CanPatch).ToList();
                    builder.Append($@"
    {definition.Modifiers} sealed partial class {definition.Name}ToPatch : IToPatch<{definition.FullName}>
    {{");
                    foreach(var property in patchableProperties)
                        builder.Append($@"
        [JsonPropertyName(""{property.Key}"")]
        public {property.Type.Nullable()} {property.Name} {{ get; init; }}
");
                    builder.Append(@"
    }");
                }

                builder.Append(@"
}");
                context.AddSource($"{definition.Name}Property_generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }

        private (string Definition, string List) GetExtraPropertyArguments(UpdatableClass definition)
        {
            var definitions = new StringBuilder();
            var list = new StringBuilder();
            foreach(var property in definition.Properties.Where(a => a.IsRequired && a.IsExtra))
            {
                definitions.Append($", {property.Type} {property.Name.FirstToLower()}");
                list.Append($", {property.Name.FirstToLower()}");
            }
            return (definitions.ToString(), list.ToString());
        }

        private void GetToPatch(StringBuilder builder, UpdatableClass definition)
        {
            builder.Append($@"
        IToPatch<{definition.FullName}> IPatchable<{definition.FullName}>.GetPatchable({definition.FullName} main)
            => new {definition.Name}ToPatch
            {{");
            foreach(var property in definition.Properties.Where(a => a.CanPatch && !a.IsExtra))
            {
                if(property.IsApiObject)
                {
                    builder.Append($@"
                {property.Name} = {property.Name}?.Id == main.{property.Name}?.Id ? null : {property.Name},");
                } else
                {
                    builder.Append($@"
                {property.Name} = {property.Name} == main.{property.Name} ? null : {property.Name},");
                }
            }
            builder.AppendLine(@"
            };");
        }

        private void GeneratePropertyConstructors(StringBuilder builder, UpdatableClass definition, (string Definition, string List) extraProperties)
        {
            var source = definition.Name.FirstToLower();

            builder.Append($@"
        public {definition.Name}Property(");
            var separator = "";
            foreach(var property in definition.Properties.Where(a => a.IsRequired && !a.IsExtra))
            {
                builder.Append($"{separator}{property.Type} {property.Name.FirstToLower()}");
                separator = ", ";
            }
            builder.Append($@"{extraProperties.Definition})
        {{");
            foreach(var property in definition.Properties.Where(a => a.IsRequired))
            {
                builder.Append($@"
            {property.Name} = {property.Name.FirstToLower()};");
            }
            builder.Append($@"
        }}

        public {definition.Name}Property({definition.FullName} {source}{extraProperties.Definition}) : this(");

            separator = "";
            foreach(var property in definition.Properties.Where(a => a.IsRequired && !a.IsExtra))
            {
                builder.Append($"{separator}{source}.{property.Name}");
                separator = ", ";
            }
            builder.Append($@"{extraProperties.List})
        {{");
            foreach(var property in definition.Properties.Where(a => !a.IsRequired && !a.IsExtra))
            {
                if(property.IsExtra)
                {
                    builder.Append($@"
            {property.Name} = {property.Name.FirstToLower()};");
                } else
                {
                    builder.Append($@"
            {property.Name} = {source}.{property.Name};");
                }
            }
            builder.Append(@"
        }
");
        }

        private void GeneratePatchCastOperator(StringBuilder builder, UpdatableClass definition)
        {
            builder.Append($@"
        public static explicit operator {definition.Name}Property({definition.Name}Patch patch)
            => new {definition.Name}Property(");
            var separator = "";
            foreach(var property in definition.Properties.Where(a => a.IsRequired))
            {
                builder.Append($"{separator}{property.Name.FirstToLower()}: patch.{property.Name} ?? throw new ArgumentNullException(nameof({property.Name}))");
                separator = ", ";
            }
            builder.Append(");");
        }

        private void GeneratePropertyProperties(StringBuilder builder, UpdatableClass definition)
        {
            foreach(var property in definition.Properties.Where(a => !a.IsExtra))
            {
                if(property.IsRequired){
                    builder.Append($@"
        [Required]");

                }
                if(property.IsRequired && property.Type == "string")
                {
                    builder.Append($@"
        [JsonPropertyName(""{property.Key}"")]
        public string {property.Name}
        {{
            get => _{property.Name};
            set => _{property.Name} = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException(Static.Error.VALUE_EMPTY);
        }}
        private string _{property.Name} = """";
");
                } else if(property.IsRequired && property.IsNullable)
                {
                    builder.Append($@"
        [JsonPropertyName(""{property.Key}"")]
        public {property.Type} {property.Name}
        {{
            get => _{property.Name};
            set => _{property.Name} = null != value ? value : throw new ArgumentException(Static.Error.VALUE_EMPTY);
        }}
        private {property.Type} _{property.Name} = default;
");
                } else
                {
                    builder.Append($@"
        [JsonPropertyName(""{property.Key}"")]
        public {property.Type} {property.Name} {{ get; set; }}
");
                }
            }
        }
    }
}
