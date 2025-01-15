using BCMS.Api.BusinessLayer.Models.Books;

namespace BCMS.Api.BusinessLayer;

public interface IBookService
{
    Task<BooksResponse> GetAllAsync(string? search, BookSort? sortBy, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<BookResponse?> CreateAsync(CreateBookRequest? request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, UpdateBookRequest? request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}