using MediatR;
using Notifications.Domain.Interfaces;

namespace Notifications.Application.Commands.SendPaymentNotification;

public class SendPaymentNotificationHandler(IEmailNotificationService emailService)
    : IRequestHandler<SendPaymentNotificationCommand>
{
    public async Task Handle(SendPaymentNotificationCommand request, CancellationToken ct)
    {
        if (request.Status == "Approved")
            await emailService.SendPaymentConfirmationAsync(request.UserEmail, request.GameName, request.Price, request.OrderId, ct);
        else
            await emailService.SendPaymentRejectedAsync(request.UserEmail, request.GameName, request.OrderId, ct);
    }
}
