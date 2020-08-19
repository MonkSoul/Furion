using Fur.DynamicApiController;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public string GetVersion()
        {
            return $"v1.0.0";
        }

        public string ChangeProfile()
        {
            return "修改成功";
        }

        public string DeleteUser()
        {
            return "删除成功";
        }
    }
}