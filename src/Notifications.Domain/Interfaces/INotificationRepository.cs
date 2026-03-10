using Notifications.Domain.Entities;

namespace Notifications.Domain.Interfaces;

public interface INotificationRepository
{
    Task SaveAsync(Notification notification, CancellationToken ct);
}
