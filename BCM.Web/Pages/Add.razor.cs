using Microsoft.AspNetCore.Components;

namespace BCM.Web.Pages;

public partial class Add
{
    [SupplyParameterFromForm] private Book? Book { get; set; }

    protected override void OnInitialized() => Book ??= new();

    private void HandleValidSubmit()
    {
        // Handle form submission
    }
}