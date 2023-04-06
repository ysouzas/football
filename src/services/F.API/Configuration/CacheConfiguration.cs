namespace F.API.Configuration;

public static class CacheConfiguration
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Cache");
            options.InstanceName = "main";
        });

        return services;
    }
}
