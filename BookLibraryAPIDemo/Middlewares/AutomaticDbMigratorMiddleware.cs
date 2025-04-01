using BookLibraryAPIDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.MiddleWares;

public class AutomaticDbMigratorMiddleware
{
    private readonly RequestDelegate _next;

    public AutomaticDbMigratorMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var db = httpContext.RequestServices.GetRequiredService<BookLibraryContext>();
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync(httpContext.RequestAborted);
        }

        await _next(httpContext);
    }
}