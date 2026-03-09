using FiapCloudGames.Contracts.Events;
using MassTransit;

namespace NotificationsAPI.Consumers;

public class PaymentProcessedConsumer(ILogger<PaymentProcessedConsumer> logger) : IConsumer<PaymentProcessedEvent>
{
    public Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var evt = context.Message;

        if (evt.Status != "Approved")
        {
            logger.LogInformation(
                "[EMAIL SIMULADO] Pagamento rejeitado para {Email} | OrderId={OrderId} Jogo={GameName}",
                evt.UserEmail, evt.OrderId, evt.GameName);
            return Task.CompletedTask;
        }

        logger.LogInformation(
            "[EMAIL SIMULADO] Confirmação de compra enviado para {Email} | Parabéns! Você adquiriu '{GameName}' por R$ {Price:F2}. OrderId={OrderId}",
            evt.UserEmail, evt.GameName, evt.Price, evt.OrderId);

        return Task.CompletedTask;
    }
}
