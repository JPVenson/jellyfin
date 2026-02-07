using System;
using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration;

/// <summary>
/// Configuration for BaseItemStudioItemMap.
/// </summary>
public class BaseItemGenreConfiguration : IEntityTypeConfiguration<BaseItemGenreItemMap>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BaseItemGenreItemMap> builder)
    {
        builder.HasKey(e => new { e.ChildId, e.GenreId });
        builder.HasOne(e => e.Child);
        builder.HasOne(e => e.Genre);
    }
}
