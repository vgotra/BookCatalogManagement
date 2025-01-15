using BCMS.Api.BusinessLayer;

namespace BCMS.Api.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookBulkImportService, BookBulkImportService>();
      
        return services;
    }
}