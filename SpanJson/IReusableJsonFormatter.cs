using System;

namespace SpanJson
{
    public interface IReusableJsonFormatter<T, TSymbol> : IJsonFormatter<T, TSymbol> where TSymbol : struct
    {
        /// <summary>
        /// Deserialize JSON into the provided value instance. If value is null, create a new instance.
        /// </summary>
        /// <param name="reader">The JsonReader positioned at the start of the JSON value.</param>
        /// <param name="value">The existing value to populate, or null to create a new one.</param>
        void Deserialize(ref JsonReader<TSymbol> reader, ref T value);
    }
}
