using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration;

/// <summary>
/// Provides configuration for the BaseItemMetadataField entity.
/// </summary>
public class BaseItemImageInfoConfiguration : IEntityTypeConfiguration<BaseItemImageInfo>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BaseItemImageInfo> builder)
    {
        builder.HasKey(e => new { e.Id, e.ItemId });
        builder.HasOne(e => e.Item);
    }
}
