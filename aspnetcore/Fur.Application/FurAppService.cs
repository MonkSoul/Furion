using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public string Get()
        {
            return nameof(Fur);
        }

        public int Get(int id)
        {
            return id;
        }
    }
}