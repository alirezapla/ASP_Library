namespace BookLibraryAPIDemo.MiddleWares;

public class ResponseEnrichmentMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseEnrichmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate a trace ID (you can use a GUID or any unique identifier)
        var traceId = Guid.NewGuid().ToString();

        // Add the trace ID to the response headers
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add("X-Trace-Id", traceId);
            return Task.CompletedTask;
        });

        // Call the next middleware in the pipeline
        await _next(context);

        // Modify the response body only for 200 OK responses
        if (context.Response.StatusCode == StatusCodes.Status200OK)
        {
            var originalBodyStream = context.Response.Body;

            using (var newBodyStream = new MemoryStream())
            {
                context.Response.Body = newBodyStream;

                // Read the original response body
                newBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
                newBodyStream.Seek(0, SeekOrigin.Begin);

                // Create a new response object with the timestamp and trace ID
                var enrichedResponse = new
                {
                    Data = JsonConvert.DeserializeObject(responseBody), // Original response data
                    Timestamp = DateTime.UtcNow.ToString("o"), // ISO 8601 timestamp
                    TraceId = traceId
                };

                // Serialize the enriched response to JSON
                var jsonResponse = JsonConvert.SerializeObject(enrichedResponse);

                // Write the modified response to the original stream
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}