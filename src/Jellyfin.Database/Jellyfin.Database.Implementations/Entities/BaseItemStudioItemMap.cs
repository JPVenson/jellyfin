#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA2227 // Collection properties should be read only

using System;
using System.Collections.Generic;

namespace Jellyfin.Database.Implementations.Entities;

public class BaseItemStudioItemMap
{
    public Guid StudioId { get; set; }

    public Guid ChildId { get; set; }

    public BaseItemEntity? Studio { get; set; }

    public BaseItemEntity? Child { get; set; }
}
