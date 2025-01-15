using BCMS.Api.BusinessLayer.Mappers;

namespace BCMS.Api.Configurations;

public static class MapperConfiguration
{
    public static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        // Mapperly generates and registers mapper implementations automatically
        // No explicit registration needed unless custom behavior is required

        services.AddScoped<IBookMapper, BookMapper>();
       
        return services;
    }
}