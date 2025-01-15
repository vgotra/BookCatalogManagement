namespace BCMS.Api.BusinessLayer.Models.Books;

public class BooksResponse
{
    public List<BookResponse> Books { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}