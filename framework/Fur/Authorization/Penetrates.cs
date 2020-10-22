using Fur.DependencyInjection;

namespace Fur.Authorization
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    [SkipScan]
    internal static class Penetrates
    {
        /// <summary>
        /// 授权策略前缀
        /// </summary>
        internal const string AppAuthorizePrefix = "<Fur.Authorization.AppAuthorizeRequirement>";
    }
}