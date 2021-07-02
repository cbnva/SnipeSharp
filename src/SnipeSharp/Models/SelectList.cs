using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    [JsonConverter(typeof(PaginationConverter))]
    [GenerateConverter, GeneratePartial]
    public struct Pagination
    {
        [DeserializeAs("per_page", IsNonNullable = true)]
        int PerPage { get; }

        [DeserializeAs("more", IsNonNullable = true)]
        bool HasMore { get; }

        internal Pagination(PartialPagination partial)
        {
            PerPage = partial.PerPage;
            HasMore = partial.HasMore;
        }
    }

    [JsonConverter(typeof(SelectListConverterFactory))]
    [GeneratePartial, GenerateConverter]
    public sealed class SelectList<T>: IEnumerable<SelectItem<T>>, IReadOnlyList<SelectItem<T>>
        where T: class
    {
        [DeserializeAs("page")]
        public int Page { get; }

        [DeserializeAs("page_count")]
        public int PageCount { get; }

        [DeserializeAs("total_count")]
        public int TotalCount { get; }

        [DeserializeAs("results")]
        public IReadOnlyList<SelectItem<T>> Results { get; }

        [DeserializeAs("pagination")]
        public readonly Pagination Pagination;

        internal SelectList(PartialSelectList<T> partial)
        {
            Page = partial.Page ?? throw new ArgumentNullException(nameof(Page));
            PageCount = partial.PageCount ?? throw new ArgumentNullException(nameof(Page));
            TotalCount = partial.TotalCount ?? throw new ArgumentNullException(nameof(Page));
            Results = partial.Results ?? throw new ArgumentNullException(nameof(Page));
            Pagination = partial.Pagination ?? throw new ArgumentNullException(nameof(Pagination));
        }

        int IReadOnlyCollection<SelectItem<T>>.Count => Results.Count;
        public SelectItem<T> this[int index] => Results[index];
        public IEnumerator<SelectItem<T>> GetEnumerator() => ((IEnumerable<SelectItem<T>>)Results).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Results.GetEnumerator();
    }

    [JsonConverter(typeof(SelectItemConverterFactory))]
    [GeneratePartial, GenerateConverter]
    public sealed class SelectItem<T>: IApiObject<T>
    {
        [DeserializeAs(Static.ID)]
        public int Id { get; }

        [DeserializeAs("text")]
        public string Text { get; }

        [DeserializeAs(Static.IMAGE)]
        public string? Image { get; }

        internal SelectItem(PartialSelectItem<T> partial)
        {
            Id = partial.Id ?? throw new ArgumentNullException(nameof(Id));
            Text = partial.Text ?? throw new ArgumentNullException(nameof(Text));
            Image = partial.Image;
        }
    }
}
