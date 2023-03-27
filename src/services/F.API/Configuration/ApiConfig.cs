using F.API.Data;
using Microsoft.EntityFrameworkCore;

namespace F.API.Configuration;

public static class ApiConfig
{

    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddCaching(configuration);
        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

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

        app.UseCors("Total");

        app.UseAuthorization();

        app.MapControllers();
    }

}
