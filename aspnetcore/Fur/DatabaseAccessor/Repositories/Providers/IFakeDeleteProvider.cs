namespace Fur.DatabaseAccessor.Providers
{
    public interface IFakeDeleteProvider
    {
        string Property { get; }
        object FlagValue { get; }
    }
}