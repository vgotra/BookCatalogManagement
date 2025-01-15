using BCMS.Api.BusinessLayer;

namespace BCMS.Api.Apis;

public static class BooksBulkImportApi
{
    const string BaseRoute = "/api/books";

    public static void MapBooksBulkImportApi(this WebApplication app)
    {
        var group = app.MapGroup(BaseRoute).WithTags("Books");

        group.MapPost("/upload", async (IBookBulkImportService bookBulkImportService, IFormFile file, CancellationToken cancellationToken = default) =>
            {
                await bookBulkImportService.ImportBooksAsync(file, cancellationToken);
                return Results.Ok();
            })
            .WithName("BulkImport").WithDescription("Import books from csv file.")
            .Produces(StatusCodes.Status200OK)
            .DisableAntiforgery();
    }
}