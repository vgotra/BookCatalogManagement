using BCM.Api.Configurations;
using BCM.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);
        builder.Services.AddOpenApi();

        builder.Services
            .AddDbContext(builder)
            .AddServices()
            .AddMappers();

        var app = builder.Build();
        app.MapApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); // host:port/openapi/v1.json
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
        }

        //TODO Add rate limiting middleware

        app.UseHttpsRedirection();

        await using (var scope = app.Services.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        await app.RunAsync();
    }
}