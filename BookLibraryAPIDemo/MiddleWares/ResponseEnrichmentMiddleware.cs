using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookLibraryAPIDemo.MiddleWares
{
    public class ResponseEnrichmentMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseEnrichmentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Guid.NewGuid().ToString();

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-Trace-Id", traceId);
                return Task.CompletedTask;
            });
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;
            using (var newBodyStream = new MemoryStream())
            {
                context.Response.Body = newBodyStream;

                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    newBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
                    newBodyStream.Seek(0, SeekOrigin.Begin);

                    var enrichedResponse = new
                    {
                        Data = string.IsNullOrEmpty(responseBody)
                            ? null
                            : Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody), // Original response data
                        Timestamp = DateTime.UtcNow.ToString("o"),
                        TraceId = traceId
                    };

                    var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(enrichedResponse);

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);
                }

                newBodyStream.Seek(0, SeekOrigin.Begin);
                await newBodyStream.CopyToAsync(originalBodyStream);
            }
        }
    }
}