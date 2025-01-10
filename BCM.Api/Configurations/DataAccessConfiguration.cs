using BCM.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api.Configurations;

public static class DataAccessConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IHostApplicationBuilder context)
    {
        var connectionString = context.Configuration.GetConnectionString("Default");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));
        
        services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        return services;
    }
}