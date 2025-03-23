using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Infrastructure.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var traceId = context.HttpContext.Response.Headers["X-Trace-Id"];
        _logger.LogInformation($"Starting execution of {context.ActionDescriptor.DisplayName} with Trace ID: {traceId} at {DateTime.UtcNow}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var traceId = context.HttpContext.Response.Headers["X-Trace-Id"];
        var statusCode = context.Result is ObjectResult result ? result.StatusCode : null;
        _logger.LogInformation($"Finished execution of {context.ActionDescriptor.DisplayName} with Trace ID: {traceId} and with Status Code: {statusCode} at {DateTime.UtcNow}");
    }
}