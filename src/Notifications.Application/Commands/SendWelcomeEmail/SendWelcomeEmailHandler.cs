using MediatR;
using Notifications.Domain.Interfaces;

namespace Notifications.Application.Commands.SendWelcomeEmail;

public class SendWelcomeEmailHandler(IEmailNotificationService emailService)
    : IRequestHandler<SendWelcomeEmailCommand>
{
    public async Task Handle(SendWelcomeEmailCommand request, CancellationToken ct)
    {
        await emailService.SendWelcomeEmailAsync(request.Email, request.Name, ct);
    }
}
