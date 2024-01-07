// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Configuration;

/// <summary>
/// Configuration 模块常量
/// </summary>
internal static class Constants
{
    /// <summary>
    /// 正则表达式常量
    /// </summary>
    internal static class Patterns
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        internal const string ConfigurationFileName = @"(?<fileName>(?<realName>.+?)(\.(?<environmentName>\w+))?(?<extension>\.(json|xml|ini)))";

        /// <summary>
        /// 配置文件参数
        /// </summary>
        internal const string ConfigurationFileParameter = @"\s+(?<parameter>\b\w+\b)\s*=\s*(?<value>\btrue\b|\bfalse\b)";
    }
}