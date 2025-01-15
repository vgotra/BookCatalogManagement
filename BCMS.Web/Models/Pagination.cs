namespace BCMS.Web.Models;

public record Pagination
{
    private int _itemsPerPage = 10;
    private int _itemsCount = 0;
    
    public int CurrentPage { get; private set; } = 1;

    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set => _itemsPerPage = value < 1 ? 10 : value;
    }
    
    public int ItemsCount
    {
        set => _itemsCount = value < 0 ? 0 : value;
    }
    
    public int TotalPages => (int)Math.Ceiling((double)_itemsCount / _itemsPerPage);

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
        _itemsCount = 0;
        _itemsPerPage = 10;
    }
}