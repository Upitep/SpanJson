using System;
using System.Buffers;

namespace SpanJson
{
    /// <summary>
    /// A simple pooled list implementation that rents and returns arrays from ArrayPool to avoid frequent allocations.
    /// </summary>
    public sealed class PooledList<T> : IDisposable
    {
        private T[] _buffer;
        private int _count;

        public PooledList(int capacity = 16)
        {
            _buffer = ArrayPool<T>.Shared.Rent(capacity);
            _count = 0;
        }

        /// <summary>Number of elements currently contained.</summary>
        public int Count => _count;

        /// <summary>Add an item to the list, growing the internal buffer if necessary.</summary>
        public void Add(T item)
        {
            if (_count >= _buffer.Length)
            {
                // Rent a larger buffer and copy existing data
                var newBuffer = ArrayPool<T>.Shared.Rent(_buffer.Length * 2);
                Array.Copy(_buffer, 0, newBuffer, 0, _count);
                ArrayPool<T>.Shared.Return(_buffer, clearArray: true);
                _buffer = newBuffer;
            }
            _buffer[_count++] = item;
        }

        /// <summary>Expose the used portion of the underlying buffer as a Span.</summary>
        public Span<T> AsSpan() => new Span<T>(_buffer, 0, _count);

        /// <summary>Clear the list without returning the array to the pool.</summary>
        public void Clear()
        {
            if (_count > 0)
            {
                Array.Clear(_buffer, 0, _count);
                _count = 0;
            }
        }

        /// <summary>Return the rented array to the pool.</summary>
        public void Dispose()
        {
            if (_buffer != null)
            {
                ArrayPool<T>.Shared.Return(_buffer, clearArray: true);
                _buffer = Array.Empty<T>();
                _count = 0;
            }
        }
    }
}
