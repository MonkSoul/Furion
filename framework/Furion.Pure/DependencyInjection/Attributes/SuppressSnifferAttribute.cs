// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DependencyInjection;

/// <summary>
/// 不被扫描和发现的特性
/// </summary>
/// <remarks>用于程序集扫描类型或方法时候</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Struct)]
public class SuppressSnifferAttribute : Attribute
{
}