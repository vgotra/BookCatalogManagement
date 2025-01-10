using System.Text.Json.Serialization;

namespace BCM.Api.BusinessLayer;

/// <remarks>
/// Doesn't include None (default) because it doesn't make sense to sort by None, and in API it can be null.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookSort
{
    TitleAsc,
    TitleDesc,
    AuthorAsc,
    AuthorDesc,
    GenreAsc,
    GenreDesc,
}