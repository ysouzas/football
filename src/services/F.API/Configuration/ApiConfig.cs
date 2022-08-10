using F.API.Data;
using Microsoft.EntityFrameworkCore;

namespace F.API.Configuration;

public static class ApiConfig
{

    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }

}
