namespace SpanJson.Resolvers
{
    public sealed class ReuseExcludeNullsOriginalCaseResolver<TSymbol>() : ResolverBase<TSymbol, ReuseExcludeNullsOriginalCaseResolver<TSymbol>>(
        new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            NamingConvention = NamingConventions.OriginalCase,
            EnumOption = EnumOptions.String
        })
        where TSymbol : struct;
}