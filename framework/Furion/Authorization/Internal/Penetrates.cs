using Furion.DependencyInjection;

namespace Furion.Authorization
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
        internal const string AppAuthorizePrefix = "<Furion.Authorization.AppAuthorizeRequirement>";
    }
}