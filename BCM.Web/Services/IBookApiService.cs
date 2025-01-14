namespace BCM.Web.Services;

public interface IBookApiService
{
    Task<BooksResponse?> GetBooksAsync(string? search, BookSort? sortBy, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task PostBookAsync(Book book, CancellationToken cancellationToken = default);
    Task PutBookAsync(int id, Book book, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(int id, CancellationToken cancellationToken = default);
}