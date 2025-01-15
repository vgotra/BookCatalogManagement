using Microsoft.AspNetCore.Components;

namespace BCMS.Web.Pages;

public partial class View(IBookApiService bookApiService)
{
    //TODO Add loading, also not found content
    [Parameter] public int Id { get; set; }
    private Book? Book { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Book = await bookApiService.GetBookAsync(Id);
    }
}