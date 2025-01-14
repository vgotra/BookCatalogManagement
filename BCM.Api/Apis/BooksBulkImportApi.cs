using BCM.Api.BusinessLayer;

namespace BCM.Api.Apis;

public static class BooksBulkImportApi
{
    const string BaseRoute = "/api/books";

    public static void MapBooksBulkImportApi(this WebApplication app)
    {
        var group = app.MapGroup(BaseRoute).WithTags("Books");

        var bulkImport = group.MapPost("/bulkimport", async (IBookBulkImportService bookBulkImportService, IFormFile file, CancellationToken cancellationToken = default) =>
            {
                await bookBulkImportService.ImportBooksAsync(file, cancellationToken);
                return Results.Ok();
            })
            .WithName("BulkImport").WithDescription("Import books from csv file.")
            .Produces(StatusCodes.Status200OK);

        if (app.Environment.IsDevelopment())
            bulkImport.DisableAntiforgery(); //For test purposes
    }
}