using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration;

/// <summary>
/// FluentAPI configuration for the ActivityLog entity.
/// </summary>
public class AccessScheduleConfiguration : IEntityTypeConfiguration<AccessSchedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<AccessSchedule> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.HasOne(e => e.User).WithMany(e => e.AccessSchedules);
    }
}
