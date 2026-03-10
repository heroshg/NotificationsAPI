using System.Text.Json;
using Notifications.Domain.Entities;
using Notifications.Domain.Events;
using Notifications.Domain.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories;

public class NotificationRepository(NotificationsDbContext dbContext) : INotificationRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task SaveAsync(Notification notification, CancellationToken ct)
    {
        var version = notification.Version - notification.UncommittedEvents.Count;
        foreach (var @event in notification.UncommittedEvents)
        {
            version++;
            var record = new NotificationEventRecord
            {
                Id = Guid.NewGuid(),
                AggregateId = notification.Id,
                EventType = @event.GetType().Name,
                EventData = JsonSerializer.Serialize(@event, @event.GetType(), JsonOptions),
                OccurredAt = DateTime.UtcNow,
                Version = version
            };
            await dbContext.NotificationEvents.AddAsync(record, ct);
        }
        await dbContext.SaveChangesAsync(ct);
    }
}
