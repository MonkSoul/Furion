// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Reflection;

namespace Furion.Reflection;

/// <summary>
/// 方法参数信息
/// </summary>
internal class MethodParameterInfo
{
    /// <summary>
    /// 参数
    /// </summary>
    internal ParameterInfo Parameter { get; set; }

    /// <summary>
    /// 参数名
    /// </summary>
    internal string Name { get; set; }

    /// <summary>
    /// 参数值
    /// </summary>
    internal object Value { get; set; }
}