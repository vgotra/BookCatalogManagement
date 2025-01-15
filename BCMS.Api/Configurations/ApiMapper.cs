using BCMS.Api.Apis;

namespace BCMS.Api.Configurations;

public static class ApiMapper
{
    public static WebApplication MapApi(this WebApplication app)
    {
        app.MapBooksApi();
        app.MapBooksBulkImportApi();
       
        return app;
    }
}