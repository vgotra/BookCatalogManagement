using BCM.Api.BusinessLayer.Models.Books;

namespace BCM.Api.BusinessLayer;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<BookResponse?> CreateAsync(CreateBookRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}