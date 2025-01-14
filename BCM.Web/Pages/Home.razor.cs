namespace BCM.Web.Pages;

public partial class Home(IBookApiService bookApiService)
{
    private List<Book> _books = new();
    private string _searchTerm = string.Empty;
    private int _currentPage = 1;
    private int _itemsPerPage = 10;

    private int ItemsPerPage
    {
        get => _itemsPerPage;
        set => _itemsPerPage = value < 1 ? 10 : value;
    }

    private bool CanGoToNextPage => _currentPage < TotalPages;
    private bool CanGoToPreviousPage => _currentPage > 1;
    private int TotalPages => (int)Math.Ceiling((double)_books.Count / ItemsPerPage);

    protected override async Task OnInitializedAsync() => await ApplySearchAsync();

    private async Task ApplySearchAsync()
    {
        try
        {
            var response = await bookApiService.GetBooksAsync(_searchTerm, BookSort.TitleAsc, _currentPage, ItemsPerPage);
            _books = response?.Books ?? new List<Book>();
        }
        catch
        {
            _books = new();
        }
    }

    private async Task ResetSearchAsync()
    {
        _searchTerm = string.Empty;
        await ApplySearchAsync();
    }

    private async Task NextPageAsync()
    {
        if (CanGoToNextPage)
        {
            _currentPage++;
            await ApplySearchAsync();
        }
    }

    private async Task PreviousPageAsync()
    {
        if (CanGoToPreviousPage)
        {
            _currentPage--;
            await ApplySearchAsync();
        }
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