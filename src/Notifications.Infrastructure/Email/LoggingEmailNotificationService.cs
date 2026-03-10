using Microsoft.Extensions.Logging;
using Notifications.Domain.Interfaces;

namespace Notifications.Infrastructure.Email;

public class LoggingEmailNotificationService(ILogger<LoggingEmailNotificationService> logger)
    : IEmailNotificationService
{
    public Task SendWelcomeEmailAsync(string email, string name, CancellationToken ct)
    {
        logger.LogInformation(
            "[EMAIL SIMULADO] Boas-vindas enviado para {Email} | Olá {Name}, seu cadastro foi realizado com sucesso na FiapCloudGames!",
            email, name);
        return Task.CompletedTask;
    }

    public Task SendPaymentConfirmationAsync(string email, string gameName, decimal price, Guid orderId, CancellationToken ct)
    {
        logger.LogInformation(
            "[EMAIL SIMULADO] Confirmação de compra enviado para {Email} | Parabéns! Você adquiriu '{GameName}' por R$ {Price:F2}. OrderId={OrderId}",
            email, gameName, price, orderId);
        return Task.CompletedTask;
    }

    public Task SendPaymentRejectedAsync(string email, string gameName, Guid orderId, CancellationToken ct)
    {
        logger.LogInformation(
            "[EMAIL SIMULADO] Pagamento rejeitado para {Email} | OrderId={OrderId} Jogo={GameName}",
            email, orderId, gameName);
        return Task.CompletedTask;
    }
}
