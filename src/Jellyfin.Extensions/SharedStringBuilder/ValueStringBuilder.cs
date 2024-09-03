using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jellyfin.Extensions.SharedStringBuilder;

/// <summary>
/// A string builder shim that lives on the stack.
/// </summary>
public ref partial struct ValueStringBuilder
{
    private char[] _arrayToReturnToPool;
    private Span<char> _chars;
    private int _pos;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueStringBuilder"/> struct.
    /// </summary>
    /// <param name="initialBuffer">The initial contents of the new builder.</param>
    public ValueStringBuilder(Span<char> initialBuffer)
    {
        _arrayToReturnToPool = null!;
        _chars = initialBuffer;
        _pos = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueStringBuilder"/> struct.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of the builder.</param>
    public ValueStringBuilder(int initialCapacity = 256)
    {
        _arrayToReturnToPool = System.Buffers.ArrayPool<char>.Shared.Rent(initialCapacity);
        _chars = _arrayToReturnToPool;
        _pos = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueStringBuilder"/> struct.
    /// </summary>
    /// <param name="initialBuffer">The initial contents of the new builder.</param>
    /// <param name="initialCapacity">The initial capacity of the builder.</param>
    public ValueStringBuilder(ReadOnlySpan<char> initialBuffer, int initialCapacity)
    {
        Debug.Assert(initialBuffer.Length > initialCapacity, "Cannot create an buffer with an capacity smaller then the initial buffer.");
        _arrayToReturnToPool = System.Buffers.ArrayPool<char>.Shared.Rent(initialCapacity);
        initialBuffer.CopyTo(_arrayToReturnToPool);
        _chars = _arrayToReturnToPool;
        _pos = 0;
    }

    /// <summary>
    /// Gets or sets the length of the string.
    /// </summary>
    public int Length
    {
        get => _pos;
        set
        {
            Debug.Assert(value >= 0, "Cannot set the ValueStringBuilder length to less then zero.");
            Debug.Assert(value <= _chars.Length, "Cannot set the ValueStringBuilder length to less then its internal buffer.");
            _pos = value;
        }
    }

    /// <summary>
    /// Gets the Capacity of the internal cache.
    /// </summary>
    public int Capacity => _chars.Length;

    /// <summary>Gets the underlying storage of the builder.</summary>
    public Span<char> RawChars => _chars;

    /// <summary>
    /// Gets the character at the set index.
    /// </summary>
    /// <param name="index">Index of the character.</param>
    /// <returns>the char at the index location.</returns>
    public ref char this[int index]
    {
        get
        {
            Debug.Assert(index < _pos, "Index out of range for access in ValueStringBuilder.");
            return ref _chars[index];
        }
    }

    /// <summary>
    /// Expands the internal cache if nesseary.
    /// </summary>
    /// <param name="capacity">New total absolute capacity.</param>
    public void EnsureCapacity(int capacity)
    {
        // This is not expected to be called this with negative capacity
        Debug.Assert(capacity >= 0, "Cannot reduce the capacity of a ValueStringBuilder");

        // If the caller has a bug and calls this with negative capacity, make sure to call Grow to throw an exception.
        if ((uint)capacity > (uint)_chars.Length)
        {
            Grow(capacity - _pos);
        }
    }

    /// <summary>
    /// Get a pinnable reference to the builder.
    /// Does not ensure there is a null char after <see cref="Length"/>
    /// This overload is pattern matched in the C# 7.3+ compiler so you can omit
    /// the explicit method call, and write eg "fixed (char* c = builder)".
    /// </summary>
    /// <returns>a typed reference to the internal array of the builder.</returns>
    public ref char GetPinnableReference()
    {
        return ref MemoryMarshal.GetReference(_chars);
    }

    /// <summary>
    /// Get a pinnable reference to the builder.
    /// </summary>
    /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length"/>.</param>
    /// <returns>a typed reference to the internal array of the builder.</returns>
    public ref char GetPinnableReference(bool terminate)
    {
        if (terminate)
        {
            EnsureCapacity(Length + 1);
            _chars[Length] = '\0';
        }

        return ref MemoryMarshal.GetReference(_chars);
    }

    /// <summary>
    /// Returns the internal cache as string and disposes it afterwards.
    /// </summary>
    /// <returns>a new <see cref="string"/> containing the contents of the builder.</returns>
    public override string ToString()
    {
        var s = _chars.Slice(0, _pos).ToString();
        Dispose();
        return s;
    }

    /// <summary>
    /// Returns a span around the contents of the builder.
    /// </summary>
    /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length"/>.</param>
    /// <returns>The internal cache as Span.</returns>
    public ReadOnlySpan<char> AsSpan(bool terminate)
    {
        if (terminate)
        {
            EnsureCapacity(Length + 1);
            _chars[Length] = '\0';
        }

        return _chars.Slice(0, _pos);
    }

    /// <summary>
    /// Returns the internal buffer as memory.
    /// </summary>
    /// <returns>The internal cache as Memory.</returns>
    public ReadOnlyMemory<char> AsMemory()
    {
        return _arrayToReturnToPool.AsMemory(0, _pos);
    }

    /// <summary>
    /// Returns the internal buffer as a Span.
    /// </summary>
    /// <returns>The internal cache as Memory.</returns>
    public ReadOnlySpan<char> AsSpan() => _chars.Slice(0, _pos);

    /// <summary>
    /// Returns the internal buffer as a Span.
    /// </summary>
    /// <param name="start">The start index inside the internal buffer.</param>
    /// <returns>The internal cache as Memory.</returns>
    public ReadOnlySpan<char> AsSpan(int start) => _chars.Slice(start, _pos - start);

    /// <summary>
    /// Returns the internal buffer as a Span.
    /// </summary>
    /// <param name="start">The start index inside the internal buffer.</param>
    /// <param name="length">The length of characters to read.</param>
    /// <returns>The internal cache as Memory.</returns>
    public ReadOnlySpan<char> AsSpan(int start, int length) => _chars.Slice(start, length);

    /// <summary>
    /// Tries to copy its internal buffer to the given argument.
    /// </summary>
    /// <param name="destination">Destination memory to write to.</param>
    /// <param name="charsWritten">Number of characters written to the destination.</param>
    /// <returns>True if the operation was successfull otherwise false.</returns>
    public bool TryCopyTo(Span<char> destination, out int charsWritten)
    {
        if (_chars.Slice(0, _pos).TryCopyTo(destination))
        {
            charsWritten = _pos;
            Dispose();
            return true;
        }
        else
        {
            charsWritten = 0;
            Dispose();
            return false;
        }
    }

    /// <summary>
    /// Inserts the given character into its internal cache at the given index.
    /// </summary>
    /// <param name="index">The start index.</param>
    /// <param name="value">The character to insert.</param>
    /// <param name="count">The number of characters to insert.</param>
    public void Insert(int index, char value, int count)
    {
        if (_pos > _chars.Length - count)
        {
            Grow(count);
        }

        var remaining = _pos - index;
        _chars.Slice(index, remaining).CopyTo(_chars.Slice(index + count));
        _chars.Slice(index, count).Fill(value);
        _pos += count;
    }

    /// <summary>
    /// Inserts the given string at the set index.
    /// </summary>
    /// <param name="index">The start index.</param>
    /// <param name="s">The string to insert.</param>
    public void Insert(int index, string s)
    {
        if (s == null)
        {
            return;
        }

        var count = s.Length;

        if (_pos > _chars.Length - count)
        {
            Grow(count);
        }

        var remaining = _pos - index;
        _chars.Slice(index, remaining).CopyTo(_chars.Slice(index + count));
        s.AsSpan().CopyTo(_chars.Slice(index));
        _pos += count;
    }

    /// <summary>
    /// Appends the given character to its cache.
    /// </summary>
    /// <param name="c">The character.</param>
    /// <returns>The Builder.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueStringBuilder Append(char c)
    {
        var pos = _pos;

        if ((uint)pos < (uint)_chars.Length)
        {
            _chars[pos] = c;
            _pos = pos + 1;
        }
        else
        {
            GrowAndAppend(c);
        }

        return this;
    }

    /// <summary>
    /// Appends the given character to its cache.
    /// </summary>
    /// <param name="number">The number to add.</param>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <returns>The Builder.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueStringBuilder Append<T>(T number)
        where T : INumber<T>
    {
        if (number == null)
        {
            return this;
        }

        return Append(number.ToString()!);
    }

    /// <summary>
    /// Appends the given character to its cache.
    /// </summary>
    /// <param name="obj">The object to add.</param>
    /// <returns>The Builder.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueStringBuilder Append(object obj)
    {
        if (obj == null)
        {
            return this;
        }

        return Append(obj.ToString()!);
    }

    /// <summary>
    /// Appends the given string in its cache.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns>The builder.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueStringBuilder Append(string s)
    {
        if (s == null)
        {
            return this;
        }

        var pos = _pos;

        if (s.Length == 1 &&
            (uint)pos < (uint)_chars
                .Length) // very common case, e.g. appending strings from NumberFormatInfo like separators, percent symbols, etc.
        {
            _chars[pos] = s[0];
            _pos = pos + 1;
        }
        else
        {
            AppendSlow(s);
        }

        return this;
    }

    private void AppendSlow(string s)
    {
        var pos = _pos;

        if (pos > _chars.Length - s.Length)
        {
            Grow(s.Length);
        }

        s.AsSpan().CopyTo(_chars.Slice(pos));
        _pos += s.Length;
    }

    /// <summary>
    /// Appends the given character the set number of times.
    /// </summary>
    /// <param name="c">The character.</param>
    /// <param name="count">number of times to append.</param>
    public void Append(char c, int count)
    {
        if (_pos > _chars.Length - count)
        {
            Grow(count);
        }

        var dst = _chars.Slice(_pos, count);

        for (var i = 0; i < dst.Length; i++)
        {
            dst[i] = c;
        }

        _pos += count;
    }

    /// <summary>
    /// Appends the set Span.
    /// </summary>
    /// <param name="value">The memory to insert.</param>
    public void Append(ReadOnlySpan<char> value)
    {
        var pos = _pos;

        if (pos > _chars.Length - value.Length)
        {
            Grow(value.Length);
        }

        value.CopyTo(_chars.Slice(_pos));
        _pos += value.Length;
    }

    /// <summary>
    /// Appends an empty span of memory.
    /// </summary>
    /// <param name="length">The size of the memory to insert.</param>
    /// <returns>The span of memory appended.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<char> AppendSpan(int length)
    {
        var origPos = _pos;

        if (origPos > _chars.Length - length)
        {
            Grow(length);
        }

        _pos = origPos + length;
        return _chars.Slice(origPos, length);
    }

    /// <summary>
    /// Appents the given template with its format.
    /// </summary>
    /// <param name="culture">The culture to format the string.</param>
    /// <param name="format">The format string.</param>
    /// <param name="arg1">The first argument.</param>
    /// <returns>The builder.</returns>
    public ValueStringBuilder AppendFormat(CultureInfo culture, string format, string arg1)
    {
        return Append(string.Format(culture, format, arg1));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void GrowAndAppend(char c)
    {
        Grow(1);
        Append(c);
    }

    /// <summary>
    /// Resize the internal buffer either by doubling current buffer size or
    /// by adding <paramref name="additionalCapacityBeyondPos"/> to
    /// <see cref="_pos"/> whichever is greater.
    /// </summary>
    /// <param name="additionalCapacityBeyondPos">
    /// Number of chars requested beyond current position.
    /// </param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int additionalCapacityBeyondPos)
    {
        Debug.Assert(additionalCapacityBeyondPos > 0, "Cannot grow negative numbers.");
        Debug.Assert(_pos > _chars.Length - additionalCapacityBeyondPos, "Grow called incorrectly, no resize is needed.");

        // Make sure to let Rent throw an exception if the caller has a bug and the desired capacity is negative
        var poolArray
            = System.Buffers.ArrayPool<char>.Shared.Rent((int)Math.Max((uint)(_pos + additionalCapacityBeyondPos), (uint)_chars.Length * 2));

        _chars.Slice(0, _pos).CopyTo(poolArray);

        var toReturn = _arrayToReturnToPool;
        _chars = _arrayToReturnToPool = poolArray;

        if (toReturn != null)
        {
            System.Buffers.ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        var toReturn = _arrayToReturnToPool;
        this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again

        if (toReturn != null)
        {
            System.Buffers.ArrayPool<char>.Shared.Return(toReturn);
        }
    }
}
