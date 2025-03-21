namespace BookLibraryAPIDemo.MiddleWares;

public class AutomaticDbMigratorMiddleware
{
    private readonly RequestDelegate _next;

    public AutomaticDatabaseMigratorMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var db = httpContext.RequestServices.GetRequiredService<CleanArchitecturesDbContext>();
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync(httpContext.RequestAborted);
        }

        await _next(httpContext);
    }
}