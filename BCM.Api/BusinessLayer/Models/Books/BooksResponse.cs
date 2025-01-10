namespace BCM.Api.BusinessLayer.Models.Books;

public class BooksResponse
{
    public List<BookResponse?> Books { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
}