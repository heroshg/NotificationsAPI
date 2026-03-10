using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Interfaces;

namespace Notifications.Application.Commands.SendWelcomeEmail;

public class SendWelcomeEmailHandler(
    IEmailNotificationService emailService,
    INotificationRepository notificationRepository)
    : IRequestHandler<SendWelcomeEmailCommand>
{
    public async Task Handle(SendWelcomeEmailCommand request, CancellationToken ct)
    {
        var notification = Notification.Create(
            request.Email,
            NotificationType.WelcomeEmail,
            $"Bem-vindo à FiapCloudGames, {request.Name}!");

        await emailService.SendWelcomeEmailAsync(request.Email, request.Name, ct);

        notification.MarkSent();
        await notificationRepository.SaveAsync(notification, ct);
    }
}
