// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace System;

/// <summary>
/// 控制跳过日志监视
/// </summary>
/// <remarks>作用于全局 <see cref="LoggingMonitorAttribute"/></remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public sealed class SuppressMonitorAttribute : Attribute
{
}