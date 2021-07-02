using System;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(StubConverterFactory))]
    [GeneratePartial, GenerateConverter]
    public sealed class Stub<T>: IApiObject<T>
        where T : class, IApiObject<T>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs(Static.NAME)]
        public string Name { get; }

        internal Stub(PartialStub<T> partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Name = partial.Name ?? throw new ArgumentNullException(nameof(Name));
        }

        public override string ToString() => $"({Id}) {Name}";
    }
}
