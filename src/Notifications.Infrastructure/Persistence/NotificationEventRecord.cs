namespace Notifications.Infrastructure.Persistence;

public class NotificationEventRecord
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }       // = NotificationId
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;  // JSON
    public DateTime OccurredAt { get; set; }
    public int Version { get; set; }
}
