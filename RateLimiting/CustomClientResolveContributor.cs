using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;

namespace api_limit.RateLimiting
{
    public class CustomClientResolveContributor : IClientResolveContributor
    {
        public async Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var headerValue = string.Empty;
            if (httpContext.Request.Headers.TryGetValue("X-api-value", out var values))
            {
                headerValue = values.First();
            }
            return await Task.FromResult(headerValue);
        }
    }
}