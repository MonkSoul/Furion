using Fur.DynamicApiController;
using Fur.FriendlyException;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public int Get(int id)
        {
            if (id < 3) throw Oops.Oh($"{id} 不能小于3。");

            return id;
        }
    }
}