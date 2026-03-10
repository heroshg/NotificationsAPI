using FiapCloudGames.Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Commands.SendPaymentNotification;

namespace Notifications.Infrastructure.Messaging;

public class PaymentProcessedConsumer(
    IMediator mediator,
    ILogger<PaymentProcessedConsumer> logger)
    : IConsumer<PaymentProcessedEvent>
{
    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var evt = context.Message;
        logger.LogInformation("PaymentProcessedEvent received. OrderId={OrderId} Status={Status}", evt.OrderId, evt.Status);
        await mediator.Send(new SendPaymentNotificationCommand(
            evt.UserEmail, evt.GameName, evt.Price, evt.OrderId, evt.Status), context.CancellationToken);
    }
}
