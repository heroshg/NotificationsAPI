using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.Interfaces;
using Notifications.Infrastructure.Email;
using Notifications.Infrastructure.Messaging;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Persistence.Repositories;

namespace Notifications.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationsDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("Notifications")));

        services.AddScoped<IEmailNotificationService, LoggingEmailNotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<PaymentProcessedConsumer>();
            x.AddConsumer<OrderCancelledConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"] ?? throw new InvalidOperationException("RabbitMQ:Username is missing."));
                    h.Password(configuration["RabbitMQ:Password"] ?? throw new InvalidOperationException("RabbitMQ:Password is missing."));
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
