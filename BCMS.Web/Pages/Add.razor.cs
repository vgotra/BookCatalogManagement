using Microsoft.AspNetCore.Components;

namespace BCMS.Web.Pages;

public partial class Add(IBookApiService bookApiService, NavigationManager navigationManager)
{
    [SupplyParameterFromForm] private Book Book { get; set; } = new();

    private async Task HandleValidSubmit()
    {
        await bookApiService.PostBookAsync(Book);
        navigationManager.NavigateTo("/");
    }
}