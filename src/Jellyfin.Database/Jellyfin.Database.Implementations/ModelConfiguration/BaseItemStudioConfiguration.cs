using System;
using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration;

/// <summary>
/// Configuration for BaseItemStudioItemMap.
/// </summary>
public class BaseItemStudioConfiguration : IEntityTypeConfiguration<BaseItemStudioItemMap>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BaseItemStudioItemMap> builder)
    {
        builder.HasKey(e => new { e.ChildId, e.StudioId });
        builder.HasOne(e => e.Child);
        builder.HasOne(e => e.Studio);
    }
}
