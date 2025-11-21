using System;
using SpanJson.Resolvers;

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
            DeserializeInto<T, ReuseExcludeNullsOriginalCaseResolver<byte>>(utf8Json, ref value);
        }

        public static void DeserializeInto<T, TResolver>(ReadOnlySpan<byte> utf8Json, ref T value)
            where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var reader = new JsonReader<byte>(utf8Json);

            if (value is IReusableJsonFormatter<T, byte> instanceFormatter)
            {
                instanceFormatter.Deserialize(ref reader, ref value);
                return;
            }

            var resolver = StandardResolvers.GetResolver<byte, TResolver>();
            var formatter = resolver.GetFormatter<T>();

            if (formatter is IReusableJsonFormatter<T, byte> reusableFormatter)
            {
                reusableFormatter.Deserialize(ref reader, ref value);
            }
            else
            {
                value = formatter.Deserialize(ref reader);
            }
        }
    }
}