namespace Notifications.Domain.Interfaces;

public interface IEmailNotificationService
{
    Task SendWelcomeEmailAsync(string email, string name, CancellationToken ct);
    Task SendPaymentConfirmationAsync(string email, string gameName, decimal price, Guid orderId, CancellationToken ct);
    Task SendPaymentRejectedAsync(string email, string gameName, Guid orderId, CancellationToken ct);
}
