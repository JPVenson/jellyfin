using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration
{
    /// <summary>
    /// FluentAPI configuration for the DisplayPreferencesConfiguration entity.
    /// </summary>
    public class DisplayPreferencesConfiguration : IEntityTypeConfiguration<DisplayPreferences>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DisplayPreferences> builder)
        {
            builder
                .HasMany(d => d.HomeSections)
                .WithOne();

            builder
                .HasIndex(entity => new { entity.UserId, entity.ItemId, entity.Client })
                .IsUnique();

            builder.HasOne(e => e.User).WithMany(e => e.DisplayPreferences);
            builder.HasOne(e => e.Item).WithMany(e => e.DisplayPreferences);
        }
    }
}
