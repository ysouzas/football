using F.API.Data;
using F.API.Data.Repository;
using F.API.Data.Repository.Interfaces;
using MediatR;

namespace F.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IRankRepository, RankRepository>();
        services.AddScoped<ApiContext>();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    }
}