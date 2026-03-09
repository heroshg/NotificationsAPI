using FiapCloudGames.Contracts.Events;
using MassTransit;

namespace NotificationsAPI.Consumers;

public class UserCreatedConsumer(ILogger<UserCreatedConsumer> logger) : IConsumer<UserCreatedEvent>
{
    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var evt = context.Message;

        logger.LogInformation(
            "[EMAIL SIMULADO] Boas-vindas enviado para {Email} | Olá {Name}, seu cadastro foi realizado com sucesso na FiapCloudGames!",
            evt.Email, evt.Name);

        return Task.CompletedTask;
    }
}
