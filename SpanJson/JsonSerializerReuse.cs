using System;

namespace SpanJson
{
    /// <summary>
    /// Helper methods to deserialize JSON into existing objects when supported by IReusableJsonFormatter.
    /// </summary>
    public static class JsonSerializerReuse
    {
        /// <summary>
        /// Deserialize the provided UTF-8 JSON payload into an existing value. If the formatter implements IReusableJsonFormatter,
        /// the existing object is populated; otherwise a new instance is created and assigned back to <paramref name="value"/>.
        /// </summary>
        public static void DeserializeInto<T>(ReadOnlySpan<byte> utf8Json, ref T value)
        {
            var reader = new JsonReader<byte>(utf8Json);
            var formatter = JsonSerializer.Generic.Utf8.Deserialize<T>(utf8Json);
            if (value is IReusableJsonFormatter<T, byte> reusable)
            {
                reusable.Deserialize(ref reader, ref value);
            }
            else
            {
                value = formatter;
            }
        }
    }
}
