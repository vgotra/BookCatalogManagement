namespace BCM.Api.DataAccess.Entities;

public class Book : BaseEntity<int>
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Genre { get; set; }
}