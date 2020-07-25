namespace Fur.DatabaseAccessor.MultipleTenants.Providers
{
    public interface IMultipleTenantProvider
    {
        int GetTenantId();
    }
}