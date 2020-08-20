using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public string Get()
        {
            return nameof(Fur);
        }

        public string GetV1()
        {
            return nameof(Fur);
        }

        public string GetV2_1()
        {
            return nameof(Fur);
        }
    }
}