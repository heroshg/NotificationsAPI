using Microsoft.EntityFrameworkCore;

namespace Notifications.Infrastructure.Persistence;

public class NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : DbContext(options)
{
    public DbSet<NotificationEventRecord> NotificationEvents => Set<NotificationEventRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationEventRecord>(entity =>
        {
            entity.ToTable("notification_events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AggregateId).IsRequired();
            entity.Property(e => e.EventType).HasMaxLength(256).IsRequired();
            entity.Property(e => e.EventData).IsRequired();
            entity.HasIndex(e => new { e.AggregateId, e.Version }).IsUnique();
        });
    }
}
