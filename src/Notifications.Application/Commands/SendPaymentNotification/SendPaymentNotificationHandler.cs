using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Interfaces;

namespace Notifications.Application.Commands.SendPaymentNotification;

public class SendPaymentNotificationHandler(
    IEmailNotificationService emailService,
    INotificationRepository notificationRepository)
    : IRequestHandler<SendPaymentNotificationCommand>
{
    public async Task Handle(SendPaymentNotificationCommand request, CancellationToken ct)
    {
        var (type, subject) = request.Status == "Approved"
            ? (NotificationType.PaymentConfirmation, $"Compra confirmada: {request.GameName}")
            : (NotificationType.PaymentRejection, $"Pagamento rejeitado: {request.GameName}");

        var notification = Notification.Create(request.UserEmail, type, subject);

        if (request.Status == "Approved")
            await emailService.SendPaymentConfirmationAsync(
                request.UserEmail, request.GameName, request.Price, request.OrderId, ct);
        else
            await emailService.SendPaymentRejectedAsync(
                request.UserEmail, request.GameName, request.OrderId, ct);

        notification.MarkSent();
        await notificationRepository.SaveAsync(notification, ct);
    }
}
