using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibraryAPIDemo.Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public CachingBehavior(IMemoryCache cache, MemoryCacheEntryOptions cacheOptions)
    {
        _cache = cache;
        _cacheOptions = cacheOptions;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheKey(request);

        if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
        {
            return cachedResponse;
        }

        var response = await next();

        _cache.Set(cacheKey, response, _cacheOptions);

        return response;
    }

    private static string GenerateCacheKey(TRequest request)
    {
        return $"{typeof(TRequest).FullName}:{System.Text.Json.JsonSerializer.Serialize(request)}";
    }
}