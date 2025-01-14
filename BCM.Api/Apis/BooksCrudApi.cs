using BCM.Api.BusinessLayer;
using BCM.Api.BusinessLayer.Models.Books;

namespace BCM.Api.Apis;

public static class BooksCrudApi
{
    const string BaseRoute = "/api/books";

    public static void MapBooksApi(this WebApplication app)
    {
        //TODO Check for null, etc
        var group = app.MapGroup(BaseRoute).WithTags("Books");

        //! No need to use name of column for simple tasks if we have predefined enum (also easier to use API)
        group.MapGet("/", async (IBookService bookService, string? search, BookSort? sortBy, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default) =>
            {
                var books = await bookService.GetAllAsync(search, sortBy, page, pageSize, cancellationToken);
                return Results.Ok(books);
            })
            .WithName("GetAllBooks").WithDescription("Get all books with optional search, sorting, and pagination.")
            .Produces<IEnumerable<BookResponse>>();

        group.MapGet("/{id}", async (int id, IBookService bookService) =>
                await bookService.GetByIdAsync(id) is BookResponse book ? Results.Ok(book) : Results.NotFound())
            .WithName("GetBookById").WithDescription("Get book by unique id.")
            .Produces<BookResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateBookRequest request, IBookService bookService, CancellationToken cancellationToken = default) =>
            {
                var result = await bookService.CreateAsync(request, cancellationToken);
                return result is null ? Results.InternalServerError() : Results.Created($"{BaseRoute}/{result.Id}", result);
            })
            .WithName("CreateBook").WithDescription("Creates a new book record.")
            .Accepts<CreateBookRequest>("application/json")
            .Produces<BookResponse>(StatusCodes.Status201Created);

        group.MapPut("/{id}", async (int id, UpdateBookRequest request, IBookService bookService, CancellationToken cancellationToken = default) =>
            {
                var result = await bookService.UpdateAsync(id, request, cancellationToken);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateBook").WithDescription("Updates an existing book.")
            .Accepts<UpdateBookRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", async (int id, IBookService bookService, CancellationToken cancellationToken = default) =>
            {
                var result = await bookService.DeleteAsync(id, cancellationToken);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteBook").WithDescription("Deletes a book by unique id.")
            .Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);
    }
}