namespace BCM.Web.Models;

public record Pagination
{
    private int _itemsPerPage = 10;
    private int _totalItems = 0;
    
    public int CurrentPage { get; private set; } = 1;

    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set => _itemsPerPage = value < 1 ? 10 : value;
    }
    
    public int ItemsCount
    {
        set => _totalItems = value < 0 ? 0 : value;
    }
    
    public int TotalPages => (int)Math.Ceiling((double)_totalItems / _itemsPerPage);

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
        _totalItems = 0;
        _itemsPerPage = 10;
    }
}