// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Reflection;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数元数据
/// </summary>
public sealed class ArgumentMetadata
{
    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; internal set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; internal set; }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; internal set; }

    /// <summary>
    /// 参数值
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 是否传参
    /// </summary>
    public bool IsTransmission { get; set; }

    /// <summary>
    /// 是否集合
    /// </summary>
    public bool IsCollection { get; internal set; }

    /// <summary>
    /// 属性对象
    /// </summary>
    public PropertyInfo Property { get; internal set; }

    /// <summary>
    /// 是否传入短参数
    /// </summary>
    public bool IsShortName { get; set; }

    /// <summary>
    /// 是否传入长参数
    /// </summary>
    public bool IsLongName { get; set; }
}
