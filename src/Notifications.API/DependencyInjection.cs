using Microsoft.OpenApi.Models;

namespace Notifications.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications API", Version = "v1" }));

        return services;
    }

    public static WebApplication UseApiPipeline(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifications API v1"));
        app.MapControllers();
        return app;
    }
}
