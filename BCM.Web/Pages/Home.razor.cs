namespace BCM.Web.Pages;

public partial class Home(IBookApiService bookApiService)
{
    //TODO Add sorting, improve error handling, add loading spinner, improve code quality
    private List<Book> _books = new();

    private string _searchTerm = string.Empty;
    private int _currentPage = 1;
    private int _itemsPerPage = 10;
    
    private int ItemsPerPage
    {
        get => _itemsPerPage;
        set
        {
            _itemsPerPage = value < 1 ? 10 : value;
            _ = ChangePageCountAsync();
        }
    }
    
    private bool CanGoToNextPage => _currentPage < TotalPages;
    private bool CanGoToPreviousPage => _currentPage > 1;
    private int TotalPages => (int)Math.Ceiling((double)_books.Count / _itemsPerPage);

    protected override async Task OnInitializedAsync() => await ApplySearchAsync();

    private async Task ApplySearchAsync()
    {
        try
        {
            var response = await bookApiService.GetBooksAsync(_searchTerm, BookSort.TitleAsc, _currentPage, _itemsPerPage); 
            _books = response?.Books ?? new();
            _currentPage = response?.Page ?? 1;
        }
        catch (Exception)
        {
            _books = new();
        }
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
    
    private async Task ChangePageCountAsync() => await ApplySearchAsync();
    
    private async Task DeleteBook(int id)
    {
        try
        {
            await bookApiService.DeleteBookAsync(id);
            await ApplySearchAsync();
        }
        catch (Exception)
        {
            //Exception handling
        }
    }
}