using BookLibraryAPIDemo.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.MiddleWares;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Business exception has been thrown");

            context.Response.StatusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                BadRequestException => StatusCodes.Status400BadRequest,
                AuthorNotFoundException => StatusCodes.Status404NotFound,
                BookNotFoundException => StatusCodes.Status404NotFound,
                CategoryNotFoundException => StatusCodes.Status404NotFound,
                NotFoundException => StatusCodes.Status404NotFound,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                RepositoryException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await WriteResponseAsync(context, ex);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            Error = ex.Message.Replace(Environment.NewLine, " - "),
            StackTrace = context.Response.StatusCode == StatusCodes.Status500InternalServerError ? ex.StackTrace : null
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}