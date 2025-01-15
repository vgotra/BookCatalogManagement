namespace BCMS.Web.Models;

public record Pagination
{
    private const int DefaultItemsPerPage = 10;
    
    private int _itemsPerPage = DefaultItemsPerPage;
    private int _totalCount = 0;
    
    public int CurrentPage { get; private set; } = 1;

    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set => _itemsPerPage = value < 1 ? DefaultItemsPerPage : value;
    }
    
    public int TotalCount
    {
        set => _totalCount = value < 0 ? 0 : value;
    }
    
    public int TotalPages
    {
        get
        {
            var totalPages = (int)Math.Ceiling((double)_totalCount / _itemsPerPage);
            return totalPages < 1 ? 1 : totalPages;
        }
    }

    public bool CanGoToNextPage => CurrentPage < TotalPages;
    public bool CanGoToPreviousPage => CurrentPage > 1;

    public void NextPage()
    {
        if (CanGoToNextPage)
            CurrentPage++;
    }

    public void PreviousPage()
    {
        if (CanGoToPreviousPage)
            CurrentPage--;
    }
    
    public void Reset()
    {
        CurrentPage = 1;
        _totalCount = 0;
        _itemsPerPage = DefaultItemsPerPage;
    }
}