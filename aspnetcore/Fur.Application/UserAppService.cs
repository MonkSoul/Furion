using Fur.DynamicApiController;

namespace Fur.Application
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [ApiDescriptionSettings(Tag = "All Apis")]
    public class UserAppService : IDynamicApiController
    {
        /// <summary>
        /// 动作方法注释
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return nameof(Fur);
        }

        /// <summary>
        /// 动作方法注释
        /// </summary>
        /// <returns></returns>
        public string Get(int id)
        {
            return nameof(Fur);
        }
    }
}