using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public TestDto GetTest(TestDto testDto)
        {
            return testDto;
        }
    }
}