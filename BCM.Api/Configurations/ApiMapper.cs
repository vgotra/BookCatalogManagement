using BCM.Api.Apis;

namespace BCM.Api.Configurations;

public static class ApiMapper
{
    public static WebApplication MapApi(this WebApplication app)
    {
        app.MapBooksApi();
       
        return app;
    }
}