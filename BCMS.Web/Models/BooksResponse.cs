namespace BCMS.Web.Models;

public class BooksResponse
{
    public List<Book> Books { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
}