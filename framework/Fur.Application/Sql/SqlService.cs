using Fur.DatabaseAccessor;
using Fur.DynamicApiController;

namespace Fur.Application
{
    /// <summary>
    /// Sql 操作
    /// </summary>
    [DynamicApiController]
    public class SqlService
    {
        private ISqlRepository _sqlRepository;

        public SqlService(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }
    }
}