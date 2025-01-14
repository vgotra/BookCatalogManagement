using Microsoft.AspNetCore.Components;

namespace BCM.Web.Pages;

public partial class Edit(IBookApiService bookApiService, NavigationManager navigationManager)
{
    [Parameter] public int Id { get; set; }
    [SupplyParameterFromForm] private Book? Book { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var book = await bookApiService.GetBookAsync(Id);
        Book = book ?? new();
    }

    private void HandleValidSubmit()
    {
        if (Book is not null)
        {
            _ = bookApiService.PutBookAsync(Id, Book); //Better reuse Patch
            navigationManager.NavigateTo("/");
        }
    }
}