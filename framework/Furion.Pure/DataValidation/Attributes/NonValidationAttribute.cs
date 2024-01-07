// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 跳过验证
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class NonValidationAttribute : Attribute
{
}