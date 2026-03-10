using FiapCloudGames.Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Commands.SendWelcomeEmail;

namespace Notifications.Infrastructure.Messaging;

public class UserCreatedConsumer(
    IMediator mediator,
    ILogger<UserCreatedConsumer> logger)
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var evt = context.Message;
        logger.LogInformation("UserCreatedEvent received. UserId={UserId}", evt.UserId);
        await mediator.Send(new SendWelcomeEmailCommand(evt.Email, evt.Name), context.CancellationToken);
    }
}
