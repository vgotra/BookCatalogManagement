namespace BCMS.Api.BusinessLayer.Models.Books;

public class UpdateBookRequest
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
}