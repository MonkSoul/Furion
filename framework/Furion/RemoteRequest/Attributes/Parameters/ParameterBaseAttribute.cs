// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 代理参数基类特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class ParameterBaseAttribute : Attribute
{
}