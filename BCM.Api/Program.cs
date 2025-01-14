using BCM.Api.Configurations;
using BCM.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        //TODO Add caching
        var builder = WebApplication.CreateSlimBuilder(args);
        builder.Services
            .ConfigureApiRateLimiting()
            .AddOpenApi()
            .AddCors()
            .AddDbContext(builder)
            .AddServices()
            .AddMappers();

        var app = builder.Build();
        app.UseRateLimiter();
        app.MapApi();
        app.UseCors();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }

        app.UseHttpsRedirection();

        await using (var scope = app.Services.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        await app.RunAsync();
    }
}