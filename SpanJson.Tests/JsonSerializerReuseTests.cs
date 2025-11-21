using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public sealed class JsonSerializerReuseTests
    {
        private sealed class ReusablePoint : IReusableJsonFormatter<ReusablePoint, byte>
        {
            public int X { get; set; }
            public int Y { get; set; }

            // IJsonFormatter<T, byte> implementation (not used by helper, but required by interface)
            public void Serialize(ref JsonWriter<byte> writer, ReusablePoint value)
            {
                writer.WriteBeginObject();
                writer.WriteName("X");
                writer.WriteInt32(value.X);
                writer.WriteValueSeparator();
                writer.WriteName("Y");
                writer.WriteInt32(value.Y);
                writer.WriteEndObject();
            }

            public ReusablePoint Deserialize(ref JsonReader<byte> reader)
            {
                // Basic reflection-like parsing to satisfy interface; create new instance
                if (reader.ReadIsNull())
                {
                    return null;
                }

                var result = new ReusablePoint();
                reader.ReadBeginObjectOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
                {
                    var name = reader.ReadEscapedName();
                    if (name == "X")
                    {
                        result.X = reader.ReadInt32();
                    }
                    else if (name == "Y")
                    {
                        result.Y = reader.ReadInt32();
                    }
                    else
                    {
                        reader.SkipNextSegment();
                    }
                }

                return result;
            }

            // IReusableJsonFormatter<T, byte>
            public void Deserialize(ref JsonReader<byte> reader, ref ReusablePoint value)
            {
                if (reader.ReadIsNull())
                {
                    value = null;
                    return;
                }

                if (value == null)
                {
                    value = new ReusablePoint();
                }

                reader.ReadBeginObjectOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
                {
                    var name = reader.ReadEscapedName();
                    if (name == "X")
                    {
                        value.X = reader.ReadInt32();
                    }
                    else if (name == "Y")
                    {
                        value.Y = reader.ReadInt32();
                    }
                    else
                    {
                        reader.SkipNextSegment();
                    }
                }
            }
        }

        private sealed class SimpleDto
        {
            public int A { get; set; }
        }

        [Fact]
        public void DeserializeInto_ReusesExistingInstance_WhenTypeImplementsIReusable()
        {
            var existing = new ReusablePoint { X = 1, Y = 1 };
            var originalRef = existing;
            var json = Encoding.UTF8.GetBytes("{\"X\":10,\"Y\":20}");

            JsonSerializerReuse.DeserializeInto(json, ref existing);

            Assert.Same(originalRef, existing); // instance reused
            Assert.Equal(10, existing.X);
            Assert.Equal(20, existing.Y);
        }

        [Fact]
        public void DeserializeInto_ReplacesInstance_WhenTypeDoesNotImplementIReusable()
        {
            var existing = new SimpleDto { A = 1 };
            var originalRef = existing;
            var json = Encoding.UTF8.GetBytes("{\"A\":5}");

            JsonSerializerReuse.DeserializeInto(json, ref existing);

            Assert.NotSame(originalRef, existing); // new instance created
            Assert.Equal(5, existing.A);
        }

        [Fact]
        public void DeserializeInto_CreatesInstance_WhenNull()
        {
            SimpleDto existing = null;
            var json = Encoding.UTF8.GetBytes("{\"A\":42}");

            JsonSerializerReuse.DeserializeInto(json, ref existing);

            Assert.NotNull(existing);
            Assert.Equal(42, existing.A);
        }
    }
}
