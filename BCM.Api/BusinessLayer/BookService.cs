using BCM.Api.BusinessLayer.Mappers;
using BCM.Api.BusinessLayer.Models.Books;
using BCM.Api.DataAccess;
using BCM.Api.DataAccess.Entities;
using BCM.Api.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api.BusinessLayer;

public class BookService(ApplicationDbContext dbContext, IBookMapper bookMapper, IHubContext<BookHub> hubContext) : IBookService
{
    public async Task<BooksResponse> GetAllAsync(string? search, BookSort? sortBy, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        //TODO Add try catch
        //? Do we need total?
        var booksQueryable = dbContext.Books.AsNoTracking();
        if (search is not null)
            booksQueryable = booksQueryable.Where(x => x.Title.Contains(search) || x.Author.Contains(search) || x.Genre.Contains(search));

        List<Book>? books = null;
        if (sortBy is not null)
        {
            var orderedQueryable = sortBy switch
            {
                BookSort.TitleAsc => booksQueryable.OrderBy(x => x.Title), //! No need to use name of column for simple tasks if we have predefined enum (also easier to use API)
                BookSort.TitleDesc => booksQueryable.OrderByDescending(x => x.Title),
                BookSort.AuthorAsc => booksQueryable.OrderBy(x => x.Author),
                BookSort.AuthorDesc => booksQueryable.OrderByDescending(x => x.Author),
                BookSort.GenreAsc => booksQueryable.OrderBy(x => x.Genre),
                BookSort.GenreDesc => booksQueryable.OrderByDescending(x => x.Genre),
                _ => booksQueryable
            };
            books = await orderedQueryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }
        else
            books = await booksQueryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        var responses = books.Select(bookMapper.ToResponse).ToList();
        return new BooksResponse
        {
            Books = responses,
            Page = page,
            PageSize = pageSize
        };
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
        await hubContext.Clients.All.SendAsync(SignalRConstants.BooksUpdated, cancellationToken);
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
        if (result > 0)
            await hubContext.Clients.All.SendAsync(SignalRConstants.BooksUpdated, cancellationToken);
        return result > 0;
    }
}