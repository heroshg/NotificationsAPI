using Notifications.Domain.Entities;

namespace Notifications.Domain.Events;

public record NotificationCreatedEvent(
    Guid NotificationId,
    string RecipientEmail,
    NotificationType Type,
    string Subject,
    DateTime OccurredAt) : IDomainEvent;
