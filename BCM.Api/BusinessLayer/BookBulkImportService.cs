using System.Text;
using BCM.Api.BusinessLayer.Csv;
using BCM.Api.DataAccess;
using BCM.Api.DataAccess.Entities;
using BCM.Api.SignalR;
using Microsoft.AspNetCore.SignalR;
using TinyCsvParser;

namespace BCM.Api.BusinessLayer;

public class BookBulkImportService(ApplicationDbContext dbContext, IHubContext<BookHub> hubContext) : IBookBulkImportService
{
    public async Task ImportBooksAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null", nameof(file));

        var books = new List<Book>();

        using var stream = new StreamReader(file.OpenReadStream());
        var csvParserOptions = new CsvParserOptions(true, ',');
        var csvMapper = new CsvBookMapping();
        var csvParser = new CsvParser<Book>(csvParserOptions, csvMapper);

        var records = csvParser.ReadFromStream(stream.BaseStream, Encoding.UTF8).ToList();

        foreach (var record in records)
            if (record.IsValid)
                books.Add(record.Result);
        
        if (books.Count == 0)
            throw new ArgumentException("No valid books found in the file", nameof(file));
        
        await dbContext.Books.AddRangeAsync(books, cancellationToken);
        var result = await dbContext.SaveChangesAsync(cancellationToken);
        if (result > 0)
            await hubContext.Clients.All.SendAsync(SignalRConstants.BooksUpdated, cancellationToken);
    }
}