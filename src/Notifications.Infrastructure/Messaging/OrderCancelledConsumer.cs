using FiapCloudGames.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notifications.Domain.Entities;
using Notifications.Domain.Interfaces;

namespace Notifications.Infrastructure.Messaging;

public class OrderCancelledConsumer(
    INotificationRepository notificationRepository,
    ILogger<OrderCancelledConsumer> logger)
    : IConsumer<OrderCancelledEvent>
{
    public async Task Consume(ConsumeContext<OrderCancelledEvent> context)
    {
        var evt = context.Message;
        logger.LogInformation(
            "[SAGA COMPENSAÇÃO] OrderCancelledEvent recebido. OrderId={OrderId} UserId={UserId} Game={GameName}",
            evt.OrderId, evt.UserId, evt.GameName);

        var notification = Notification.Create(
            evt.UserEmail,
            NotificationType.OrderCancelled,
            $"Pedido cancelado: {evt.GameName} (OrderId={evt.OrderId})");

        notification.MarkSent();
        await notificationRepository.SaveAsync(notification, context.CancellationToken);
    }
}
