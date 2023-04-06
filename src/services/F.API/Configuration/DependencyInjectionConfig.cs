using F.API.Data;
using F.API.Data.Cache;
using F.API.Data.Repository;
using F.API.Data.Repository.Interfaces;
using F.API.Data.Storage;
using F.API.Data.Storage.Interfaces;
using F.Core.Data;
using F.Dealer.Interfaces;
using F.Dealer.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace F.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var tableConnectionString = configuration.GetConnectionString("TableStorage");

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddTransient<ICache, Cache>();
        services.AddScoped<IDealer, SortDealer>();
        services.AddScoped<IRankRepository, RankRepository>();
        services.AddScoped<IPlayerTableStorage>(x => new PlayerTableStorage(tableConnectionString));
        services.AddScoped<ApiContext>();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    }
}