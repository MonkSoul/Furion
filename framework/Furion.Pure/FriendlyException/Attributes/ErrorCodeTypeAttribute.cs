// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// 错误代码类型特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Enum)]
public sealed class ErrorCodeTypeAttribute : Attribute
{
}