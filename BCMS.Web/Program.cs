using BCMS.Web.Options;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BCMS.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var options = builder.Configuration.GetSection("Services").Get<ServicesOptions>();
        if (options is null)
            throw new InvalidOperationException("Services configuration is missing.");
        
        builder.Services.AddSingleton(options);
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{sp.GetRequiredService<ServicesOptions>().ServerBaseUrl}/api/") });
        builder.Services.AddScoped<IBookApiService, BookApiService>();

        await builder.Build().RunAsync();
    }
}