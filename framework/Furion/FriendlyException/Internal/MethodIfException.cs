// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Reflection;

namespace Furion.FriendlyException;

/// <summary>
/// 方法异常类
/// </summary>
internal sealed class MethodIfException
{
    /// <summary>
    /// 出异常的方法
    /// </summary>
    public MethodBase ErrorMethod { get; set; }

    /// <summary>
    /// 异常特性
    /// </summary>
    public IEnumerable<IfExceptionAttribute> IfExceptionAttributes { get; set; }
}