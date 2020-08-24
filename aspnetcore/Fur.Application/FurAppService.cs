using Fur.DynamicApiController;

namespace Fur.Application
{
    [ApiDescriptionSettings(Tag = "合并所有标签")]
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

    [ApiDescriptionSettings(Tag = "合并所有标签")]
    public class TestAppService : IDynamicApiController
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