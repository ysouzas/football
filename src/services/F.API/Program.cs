using F.API.Configuration;

namespace F.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApiConfiguration(builder.Configuration);
        builder.Services.RegisterServices(builder.Configuration);

        var app = builder.Build();

        app.UseApiConfiguration(app.Environment);
        app.Run();
    }
}