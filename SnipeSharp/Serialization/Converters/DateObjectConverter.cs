using System;
using Newtonsoft.Json;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class DateObjectConverter : JsonConverter<DateTime?>
    {
        public static readonly DateObjectConverter Instance = new DateObjectConverter();

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.String)
            {
                // This is probably one of the broken API endpoints that returns the model directly
                // instead of properly transforming it.
                if(serializer.Deserialize<string>(reader) is string response && DateTime.TryParse(response, out var datetime))
                    return datetime;
            }
            else if(reader.TokenType == JsonToken.Date && reader.Value != null)
            {
                return (DateTime)reader.Value;
            }
            else if(serializer.Deserialize<DateObjectResponse>(reader) is DateObjectResponse response && DateTime.TryParse(response.DateTime, out var datetime))
                return datetime;
            return null;
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
            => throw new NotImplementedException();

        private sealed class DateObjectResponse
        {
            [DeserializeAs("date")]
            public string DateTime { get; set; }
        }
    }
}
