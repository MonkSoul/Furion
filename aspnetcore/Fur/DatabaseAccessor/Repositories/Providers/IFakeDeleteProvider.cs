namespace Fur.DatabaseAccessor.Repositories.Providers
{
    public interface IFakeDeleteProvider
    {
        string Property { get; }
        object FlagValue { get; }
    }
}
