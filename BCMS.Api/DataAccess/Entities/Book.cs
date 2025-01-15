namespace BCMS.Api.DataAccess.Entities;

public class Book : BaseEntity<int>
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
}