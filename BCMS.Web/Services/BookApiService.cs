using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Web;
using BCMS.Web.Models;

namespace BCMS.Web.Services;

public class BookApiService(HttpClient http) : IBookApiService
{
    public async Task<BooksResponse?> GetBooksAsync(string? search, BookSort? sortBy, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var query = CreateQuery(search, sortBy, page, pageSize);
        try
        {
            return await http.GetFromJsonAsync<BooksResponse>($"books?{query}", cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Book?> GetBookAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await http.GetFromJsonAsync<Book?>($"books/{id}", cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public async Task PostBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        await http.PostAsJsonAsync("books", book, cancellationToken);
    }

    public async Task PutBookAsync(int id, Book book, CancellationToken cancellationToken = default)
    {
        await http.PutAsJsonAsync($"books/{id}", book, cancellationToken);
    }

    public async Task DeleteBookAsync(int id, CancellationToken cancellationToken = default)
    {
        await http.DeleteAsync($"books/{id}", cancellationToken);
    }

    private NameValueCollection CreateQuery(string? search, BookSort? sortBy, int page, int pageSize)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        
        if (!string.IsNullOrEmpty(search)) 
            query["search"] = search;

        if (sortBy.HasValue) 
            query["sortBy"] = sortBy.ToString();
        
        query["page"] = page.ToString();
        
        query["pageSize"] = pageSize.ToString();
        
        return query;
    }
}