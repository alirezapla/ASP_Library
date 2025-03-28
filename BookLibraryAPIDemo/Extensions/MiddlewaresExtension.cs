using BookLibraryAPIDemo.MiddleWares;

namespace BookLibraryAPIDemo.Extensions;

public static class MiddlewaresExtension
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsProduction())
            app.UseHsts();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.ConfigureMyCustomMiddlewares();

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Library Demon API"); });

        app.MapControllers();
    }

    private static void ConfigureMyCustomMiddlewares(this WebApplication app)
    {
        // app.UseMiddleware<AutomaticDbMigratorMiddleware>();
        app.UseMiddleware<TraceIdMiddleware>();
        app.UseMiddleware<ResponseEnrichmentMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}