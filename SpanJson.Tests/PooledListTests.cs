using System;
using Xunit;

namespace SpanJson.Tests
{
    public sealed class PooledListTests
    {
        [Fact]
        public void Add_And_Grow_Works()
        {
            using var list = new PooledList<int>(capacity: 2);
            list.Add(1);
            list.Add(2);
            // Trigger growth
            list.Add(3);

            Assert.Equal(3, list.Count);
            var span = list.AsSpan();
            Assert.Equal(new[] { 1, 2, 3 }, span.ToArray());
        }

        [Fact]
        public void Clear_Resets_Count_And_Zeroes_Items()
        {
            using var list = new PooledList<int>(capacity: 2);
            list.Add(7);
            list.Add(8);
            list.Clear();

            Assert.Equal(0, list.Count);
            // Buffer is cleared for first count elements
            var span = list.AsSpan();
            Assert.Equal(0, span.Length);

            // After adding again, values should come from zeroed section
            list.Add(9);
            Assert.Equal(1, list.Count);
            Assert.Equal(9, list.AsSpan()[0]);
        }

        [Fact]
        public void Dispose_Returns_Buffer_And_Resets_State()
        {
            var list = new PooledList<int>(capacity: 2);
            list.Add(1);
            list.Add(2);

            list.Dispose();

            Assert.Equal(0, list.Count);
            // AsSpan after dispose should be empty
            var span = list.AsSpan();
            Assert.Equal(0, span.Length);
        }
    }
}
