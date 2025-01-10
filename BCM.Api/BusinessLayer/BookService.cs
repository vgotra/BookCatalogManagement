using BCM.Api.BusinessLayer.Mappers;
using BCM.Api.BusinessLayer.Models.Books;
using BCM.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api.BusinessLayer;

public class BookService(ApplicationDbContext dbContext, IBookMapper bookMapper) : IBookService
{
    public async Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var books = await dbContext.Books.ToListAsync(cancellationToken);
        return books.Select(bookMapper.ToResponse)!;
    }
    
    public async Task<BookResponse?> GetByIdAsync(int id)
    {
        var book = await dbContext.Books.FindAsync(id);
        return bookMapper.ToResponse(book);
    }

    public async Task<BookResponse?> CreateAsync(CreateBookRequest? request, CancellationToken cancellationToken = default)
    {
        var book = bookMapper.ToEntity(request);
        if (book is null) 
            return null;
        
        dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync(cancellationToken);
        return bookMapper.ToResponse(book);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookRequest? request, CancellationToken cancellationToken = default)
    {
        var book = await dbContext.Books.FindAsync(id, cancellationToken);
        if (book is null || request is null) 
            return false;
        
        book.Title = request.Title;
        book.Author = request.Author;
        book.Genre = request.Genre;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Books.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        return result > 0;
    }
}