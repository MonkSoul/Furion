using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public int Get(int id)
        {
            return id;
        }
    }
}