using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.Interfaces;
using Notifications.Infrastructure.Email;
using Notifications.Infrastructure.Messaging;

namespace Notifications.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailNotificationService, LoggingEmailNotificationService>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<PaymentProcessedConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"] ?? "guest");
                    h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
