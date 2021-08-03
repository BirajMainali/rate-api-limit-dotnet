using api_limit.RateLimiting;
using AspNetCoreRateLimit;
using AspNetCoreRateLimit.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace api_limit
{
    public static class DiConfig
    {
        public static void UseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            // services.AddMemoryCache(); 
            services.AddDistributedMemoryCache(); // Redis
            // services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            // services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(configuration.GetSection("ClientRateLimitPolicies"));
            // services.AddInMemoryRateLimiting();
            services.AddRedisRateLimiting();
            Installer(services);
        }

        private static void Installer(IServiceCollection services)
            => services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>()
                // .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
                .AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>()
                .AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>()
                .AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect("localhost:6379")); // Redis Connection 
    }
}