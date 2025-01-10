using BCM.Api.BusinessLayer.Models.Books;

namespace BCM.Api.BusinessLayer;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<BookResponse?> CreateAsync(CreateBookRequest? request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, UpdateBookRequest? request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}