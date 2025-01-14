namespace BCM.Api.BusinessLayer;

public class BookBulkImportService : IBookBulkImportService
{
    public Task ImportBooksAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        //TODO Check for csv file, etc
        throw new NotImplementedException();
    }
}