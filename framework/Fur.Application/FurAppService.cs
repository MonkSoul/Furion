using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public TestDto Post(TestDto testDto)
        {
            return testDto;
        }
    }
}