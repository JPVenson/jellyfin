using System.Text;
using System.Threading;

namespace Jellyfin.Extensions.SharedStringBuilder;

/// <summary>
/// Provides methods for using async optimised string builder.
/// </summary>
public static class AsyncStringBuilderCache
{
    // The value 360 was chosen in discussion with performance experts as a compromise between using
    // as litle memory (per thread) as possible and still covering a large part of short-lived
    // StringBuilder creations on the startup path of VS designers.
    private const int MaxBuilderSize = 360;

    private static AsyncLocal<StringBuilder> _cachedInstance = new AsyncLocal<StringBuilder>();

    /// <summary>
    /// Gets a string builder from its internal cache or creates a new one.
    /// </summary>
    /// <param name="capacity">The mininimum initial capacity of the builder.</param>
    /// <returns>Either a cached or new <see cref="StringBuilder"/>.</returns>
    public static StringBuilder Acquire(int capacity = 16)
    {
        if (capacity <= MaxBuilderSize)
        {
            var sb = _cachedInstance.Value;

            if (sb != null)
            {
                // Avoid stringbuilder block fragmentation by getting a new StringBuilder
                // when the requested size is larger than the current capacity
                if (capacity <= sb.Capacity)
                {
                    _cachedInstance.Value = null!;
                    sb.Clear();
                    return sb;
                }
            }
        }

        return new StringBuilder(capacity);
    }

    /// <summary>
    /// Returns a cached string builder to its cache.
    /// </summary>
    /// <param name="sb">A builder that shall be used as Cache.</param>
    public static void Release(StringBuilder sb)
    {
        if (sb.Capacity <= MaxBuilderSize)
        {
            _cachedInstance.Value = sb;
        }
    }

    /// <summary>
    /// Gets the string from the builder and Releases it.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> to read from and cache.</param>
    /// <returns>The Contents of the <see cref="StringBuilder"/>.</returns>
    public static string GetStringAndRelease(StringBuilder sb)
    {
        var result = sb.ToString();
        Release(sb);
        return result;
    }
}
