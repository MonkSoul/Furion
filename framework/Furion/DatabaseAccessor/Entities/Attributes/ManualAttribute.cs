// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 手动配置实体特性
/// </summary>
/// <remarks>支持类和方法</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ManualAttribute : Attribute
{
}