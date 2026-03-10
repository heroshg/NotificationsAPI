using Notifications.Domain.Events;

namespace Notifications.Domain.Entities;

public enum NotificationType
{
    WelcomeEmail,
    PaymentConfirmation,
    PaymentRejection,
    OrderCancelled
}

public class Notification
{
    private readonly List<IDomainEvent> _uncommittedEvents = new();

    public Guid Id { get; private set; }
    public string RecipientEmail { get; private set; } = string.Empty;
    public NotificationType Type { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime? SentAt { get; private set; }
    public int Version { get; private set; }

    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    private Notification() { }

    /// <summary>Reconstitui o agregado a partir do histórico de eventos (Event Sourcing).</summary>
    public static Notification LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        var notification = new Notification();
        foreach (var @event in events)
            notification.Apply(@event);
        return notification;
    }

    public static Notification Create(string recipientEmail, NotificationType type, string subject)
    {
        var notification = new Notification();
        notification.RaiseEvent(new NotificationCreatedEvent(
            Guid.NewGuid(), recipientEmail, type, subject, DateTime.UtcNow));
        return notification;
    }

    public void MarkSent()
    {
        if (Status != "Created")
            throw new InvalidOperationException($"Cannot mark notification as sent in status '{Status}'.");

        RaiseEvent(new NotificationSentEvent(Id, DateTime.UtcNow));
    }

    private void RaiseEvent(IDomainEvent @event)
    {
        Apply(@event);
        _uncommittedEvents.Add(@event);
    }

    private void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case NotificationCreatedEvent e:
                Id = e.NotificationId;
                RecipientEmail = e.RecipientEmail;
                Type = e.Type;
                Subject = e.Subject;
                Status = "Created";
                break;
            case NotificationSentEvent e:
                Status = "Sent";
                SentAt = e.SentAt;
                break;
        }
        Version++;
    }
}
