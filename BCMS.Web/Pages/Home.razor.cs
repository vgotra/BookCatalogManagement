using BCMS.Web.Options;
using Microsoft.AspNetCore.SignalR.Client;

namespace BCMS.Web.Pages;

public partial class Home(IBookApiService bookApiService, ServicesOptions options) : IAsyncDisposable
{
    private readonly Pagination _pagination = new();
    private List<Book> _books = [];
    private HubConnection? _hubConnection;
    private string _searchTerm = string.Empty;
    private string _sortColumn = nameof(Book.Title);
    private bool _sortAscending = true;

    protected override async Task OnInitializedAsync()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl($"{options.ServerBaseUrl}/booksHub").Build();
        _hubConnection.On("BooksUpdated", async () =>
        {
            await ApplySearchAsync();
            StateHasChanged();
        });
        await _hubConnection.StartAsync();
        await ApplySearchAsync();
    }

    private async Task SortTable(string column)
    {
        if (_sortColumn != column)
        {
            _sortColumn = column;
            _sortAscending = true;
        }
        else
            _sortAscending = !_sortAscending;

        await ApplySearchAsync();
    }

    private BookSort GetSorting =>
        _sortColumn switch
        {
            nameof(Book.Title) => _sortAscending ? BookSort.TitleAsc : BookSort.TitleDesc,
            nameof(Book.Author) => _sortAscending ? BookSort.AuthorAsc : BookSort.AuthorDesc,
            nameof(Book.Genre) => _sortAscending ? BookSort.GenreAsc : BookSort.GenreDesc,
            _ => BookSort.TitleAsc
        };

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
            await _hubConnection.DisposeAsync();
    }

    private async Task ApplySearchAsync()
    {
        try
        {
            var response = await bookApiService.GetBooksAsync(_searchTerm, GetSorting, _pagination.CurrentPage, _pagination.ItemsPerPage);
            _books = response?.Books ?? [];
            _pagination.TotalCount = response?.TotalCount ?? 0;
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
        if (await bookApiService.DeleteBookAsync(id))
            await ApplySearchAsync();
    }
}