using BCM.Api.BusinessLayer;

namespace BCM.Api.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookBulkImportService, BookBulkImportService>();
      
        return services;
    }
}