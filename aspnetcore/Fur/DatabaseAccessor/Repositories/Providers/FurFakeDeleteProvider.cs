using Fur.DatabaseAccessor.Entities;

namespace Fur.DatabaseAccessor.Repositories.Providers
{
    public class FurFakeDeleteProvider : IFakeDeleteProvider
    {
        public string Property => nameof(DbEntity.IsDeleted);

        public object FlagValue => true;
    }
}