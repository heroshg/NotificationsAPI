namespace Notifications.Domain.Events;

public record NotificationSentEvent(Guid NotificationId, DateTime SentAt) : IDomainEvent;
