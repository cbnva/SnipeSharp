using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SnipeSharp.Generator
{
    [Generator]
    public sealed class ConverterGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new ConverterSyntaxReceiver());

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxContextReceiver is not ConverterSyntaxReceiver receiver)
                return;
            foreach(var definition in receiver.Classes.Values)
            {
                var builder = new StringBuilder($@"
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using SnipeSharp;

namespace {definition.Namespace}
{{
    internal sealed class {definition.Name}Converter{definition.GenericArguments}: JsonConverter<{definition.FullName}>{definition.GenericConstraints}
    {{
        public override void Write(Utf8JsonWriter writer, {definition.FullName} value, JsonSerializerOptions options)
            => throw new NotImplementedException();
");
                if(definition.Symbol.IsReferenceType)
                {
                    builder.Append($@"
        public override {definition.NullableName} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {{
            var partial = JsonSerializer.Deserialize<{definition.PartialName}{definition.GenericArguments}>(ref reader, options);
            if(null == partial)
                return null;
            return new {definition.FullName}(partial);
        }}
    }}
");
                } else
                {
                    builder.Append($@"
        public override {definition.FullName} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {{
            var partial = JsonSerializer.Deserialize<{definition.PartialName}{definition.GenericArguments}>(ref reader, options);
            return new {definition.FullName}(partial);
        }}
    }}
");
                }
                if(definition.IsGenericType){
                    builder.Append($@"
    internal sealed class {definition.Name}ConverterFactory: JsonConverterFactory
    {{
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof({definition.Namespace}.{definition.Name}<>);
        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter?)Activator.CreateInstance(
                typeof({definition.Name}Converter<>).MakeGenericType(typeToConvert.GetGenericArguments()),
                BindingFlags.Public | BindingFlags.Instance,
                binder: null, args: Array.Empty<object>(), culture: null);
    }}
");
                }
                builder.Append($@"
}}");
                context.AddSource($"{definition.Name}Converter_generated", SourceText.From(builder.ToString(), Encoding.UTF8));
            }
        }
    }
}
