namespace BCM.Api.BusinessLayer.Models.Books;

public class BookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
}