#nullable disable

#pragma warning disable CA1819, CS1591

using System;

namespace MediaBrowser.Controller.Entities.Audio;

public interface IHasMusicGenres
{
    ReferencedItemModel[] Genres { get; }
}
