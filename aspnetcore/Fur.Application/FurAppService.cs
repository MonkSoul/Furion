using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public string GetUser(int id)
        {
            return $"{id}";
        }

        public string GetUser(int id, string name)
        {
            return $"{id} {name}";
        }

        public TestDto Add(TestDto testDto)
        {
            return testDto;
        }
    }
}