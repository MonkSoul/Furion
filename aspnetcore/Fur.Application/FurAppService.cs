using Fur.DynamicApiController;

namespace Fur.Application
{
    [ApiDescriptionSettings("Group1")]
    public class FurAppService : IDynamicApiController
    {
        public string Post()
        {
            return nameof(Fur);
        }

        [ApiDescriptionSettings("Group1", "Group3")]
        public string Get()
        {
            return nameof(Fur);
        }

        [ApiDescriptionSettings("Group2")]
        public int Get(int id)
        {
            return id;
        }
    }
}