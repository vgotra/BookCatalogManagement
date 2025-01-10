using BCM.Api.BusinessLayer;
using BCM.Api.BusinessLayer.Models.Books;

namespace BCM.Api.Apis;

public static class BooksCrudApi
{
    const string BaseRoute = "/api/books";

    public static void MapBooksApi(this WebApplication app)
    {
        //TODO Add support for search and filtering, check for null, etc
        var group = app.MapGroup(BaseRoute).WithTags("Books");

        group.MapGet("/", async (IBookService bookService, CancellationToken cancellationToken) => 
                await bookService.GetAllAsync(cancellationToken) is IEnumerable<BookResponse> books ? Results.Ok((object?)books) : Results.Ok(Enumerable.Empty<BookResponse>()))
            .WithName("GetAllBooks").WithDescription("Get all books.")
            .Produces<IEnumerable<BookResponse>>();

        group.MapGet("/{id}", async (int id, IBookService bookService) => 
                await bookService.GetByIdAsync(id) is BookResponse book ? Results.Ok(book) : Results.NotFound())
            .WithName("GetBookById").WithDescription("Get book by unique id.")
            .Produces<BookResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateBookRequest request, IBookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.CreateAsync(request, cancellationToken);
                return result is null ? Results.InternalServerError() : Results.Created($"{BaseRoute}/{result.Id}", result);
            })
            .WithName("CreateBook").WithDescription("Creates a new book record.")
            .Accepts<CreateBookRequest>("application/json")
            .Produces<BookResponse>(StatusCodes.Status201Created);

        group.MapPut("/{id}", async (int id, UpdateBookRequest request, IBookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.UpdateAsync(id, request, cancellationToken);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateBook").WithDescription("Updates an existing book.")
            .Accepts<UpdateBookRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", async (int id, IBookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.DeleteAsync(id, cancellationToken);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteBook").WithDescription("Deletes a book by unique id.")
            .Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);
    }
}