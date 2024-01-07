// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Logging;

/// <summary>
/// LoggingMonitor 序列化属性命名规则选项
/// </summary>
[SuppressSniffer]
public enum ContractResolverTypes
{
    /// <summary>
    /// CamelCase 小驼峰
    /// </summary>
    /// <remarks>默认值</remarks>
    CamelCase = 0,

    /// <summary>
    /// 保持原样
    /// </summary>
    Default = 1
}