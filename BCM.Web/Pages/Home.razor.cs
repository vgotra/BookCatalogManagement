using Microsoft.AspNetCore.SignalR.Client;

namespace BCM.Web.Pages;

public partial class Home(IBookApiService bookApiService) : IAsyncDisposable
{
    private readonly Pagination _pagination = new();
    private List<Book> _books = [];
    private HubConnection? _hubConnection;
    private string _searchTerm = string.Empty;
    

    protected override async Task OnInitializedAsync()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5212/bookHub").Build(); //TODO Add configuration
        _hubConnection.On("BooksUpdated", async () =>
        {
            await ApplySearchAsync();
            StateHasChanged();
        });
        await _hubConnection.StartAsync();
        await ApplySearchAsync();
    }

    // public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected; // for testing

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
            await _hubConnection.DisposeAsync();
    }

    private async Task ApplySearchAsync()
    {
        try
        {
            var response = await bookApiService.GetBooksAsync(_searchTerm, BookSort.TitleAsc, _pagination.CurrentPage, _pagination.ItemsPerPage);
            _books = response?.Books ?? [];
            _pagination.ItemsCount = response?.Books.Count ?? 0;
        }
        catch
        {
            _books = [];
            _pagination.Reset();
        }
    }

    private async Task ResetSearchAsync()
    {
        _searchTerm = string.Empty;
        await ApplySearchAsync();
    }

    private async Task NextPageAsync()
    {
        _pagination.NextPage();
        await ApplySearchAsync();
    }

    private async Task PreviousPageAsync()
    {
        _pagination.PreviousPage();
        await ApplySearchAsync();
    }

    private async Task DeleteBook(int id)
    {
        try
        {
            await bookApiService.DeleteBookAsync(id);
            await ApplySearchAsync();
        }
        catch
        {
            // Handle exception
        }
    }
}