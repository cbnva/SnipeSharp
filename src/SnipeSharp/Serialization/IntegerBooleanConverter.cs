using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SnipeSharp.Serialization
{
    internal sealed class IntegerBooleanConverter: JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(!reader.Read())
                throw new JsonException();
            if(reader.TryGetByte(out var byteValue))
                return byteValue != 0;
            return false;
        }
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? 1 : 0);
        }
    }
}
