using System.Threading.RateLimiting;

namespace BCM.Api.Configurations;

public static class ApiRateLimitingConfiguration
{
    public static IServiceCollection ConfigureApiRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        // 100 requests per 1 minute for each host
                        AutoReplenishment = true,
                        PermitLimit = 100, 
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
        
        return services;
    }
}