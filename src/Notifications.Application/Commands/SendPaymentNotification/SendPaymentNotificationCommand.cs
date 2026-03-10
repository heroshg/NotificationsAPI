using MediatR;

namespace Notifications.Application.Commands.SendPaymentNotification;

public record SendPaymentNotificationCommand(
    string UserEmail,
    string GameName,
    decimal Price,
    Guid OrderId,
    string Status) : IRequest;
