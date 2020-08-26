using Fur.DynamicApiController;
using Fur.FriendlyException;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        [IfException(ErrorCodes.x1000, args: "百小僧")]
        public string Get()
        {
            throw Oops.Oh(ErrorCodes.x1000);

            return nameof(Fur);
        }

        public int Get(int id)
        {
            throw Oops.Oh("哈哈哈哈");
            return id;
        }
    }
}