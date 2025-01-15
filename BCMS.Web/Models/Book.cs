using System.ComponentModel.DataAnnotations;

namespace BCMS.Web.Models;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(255)] public string Title { get; set; } = string.Empty;
    [Required, MaxLength(255)] public string Author { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string Genre { get; set; } = string.Empty;
}