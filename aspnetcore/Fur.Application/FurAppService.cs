using Fur.DynamicApiController;

namespace Fur.Application
{
    /// <summary>
    /// 控制器注释
    /// </summary>
    public class FurAppService : IDynamicApiController
    {
        /// <summary>
        /// 动作方法注释
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return nameof(Fur);
        }
    }
}