// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 手动提交标识
/// <para>默认情况下，框架会自动在方法结束之时调用 SaveChanges 方法，贴此特性可以忽略该行为</para>
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public sealed class ManualCommitAttribute : Attribute
{
}