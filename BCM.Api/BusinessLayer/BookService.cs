using BCM.Api.BusinessLayer.Models.Books;
using BCM.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api.BusinessLayer;

public class BookService(ApplicationDbContext dbContext) : IBookService
{
    public async Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        //TODO Add mapper 
        var books = await dbContext.Books.ToListAsync(cancellationToken);
        return books.Select(x => new BookResponse { Id = x.Id, Title = x.Title, Author = x.Author, Genre = x.Genre });
    }
    
    public Task<BookResponse?> GetByIdAsync(int id) => throw new NotImplementedException();
    
    public Task<BookResponse?> CreateAsync(CreateBookRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}