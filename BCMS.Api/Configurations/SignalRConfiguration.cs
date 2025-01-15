namespace BCMS.Api.Configurations;

public static class SignalRConfiguration
{
    public static IServiceCollection ConfigureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        
        return services;
    }
}