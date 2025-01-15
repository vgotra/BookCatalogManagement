using BCMS.Web.Models;
using BCMS.Web.Services;
using Microsoft.AspNetCore.Components;

namespace BCMS.Web.Pages;

public partial class Edit(IBookApiService bookApiService, NavigationManager navigationManager)
{
    [Parameter] public int Id { get; set; }
    [SupplyParameterFromForm] private Book? Book { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Book = await bookApiService.GetBookAsync(Id);
    }

    private void HandleValidSubmit()
    {
        if (string.IsNullOrWhiteSpace(Book?.Title)) // simple check for empty book 
            return;
        
        _ = bookApiService.PutBookAsync(Id, Book); //Better reuse Patch
        navigationManager.NavigateTo("/");
    }
}