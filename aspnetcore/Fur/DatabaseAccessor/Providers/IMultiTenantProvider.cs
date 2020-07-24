namespace Fur.DatabaseAccessor.Providers
{
    public interface IMultiTenantProvider
    {
        int GetTenantId();
    }
}