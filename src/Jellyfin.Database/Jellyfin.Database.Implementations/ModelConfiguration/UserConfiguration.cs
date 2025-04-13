using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration
{
    /// <summary>
    /// FluentAPI configuration for the User entity.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(user => user.Username);

            builder
                .HasOne(u => u.ProfileImage)
                .WithOne();

            builder
                .HasMany(u => u.Permissions)
                .WithOne()
                .HasForeignKey(p => p.UserId);

            builder
                .HasMany(u => u.Preferences)
                .WithOne()
                .HasForeignKey(p => p.UserId);

            builder
                .HasMany(u => u.AccessSchedules)
                .WithOne(a => a.User);

            builder
                .HasMany(u => u.DisplayPreferences)
                .WithOne(d => d.User);

            builder
                .HasMany(u => u.ItemDisplayPreferences)
                .WithOne(d => d.User);

            builder
                .HasIndex(entity => entity.Username)
                .IsUnique();
        }
    }
}
