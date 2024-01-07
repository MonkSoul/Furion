// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数转换选项
/// </summary>
public class ArgumentParseOptions
{
    /// <summary>
    /// 目标类型
    /// </summary>
    public Type TargetType { get; set; }

    /// <summary>
    /// 合并多行
    /// </summary>
    public bool CombineAllMultiples { get; set; }

    /// <summary>
    /// 合并参数
    /// </summary>
    public string[] CombinableArguments { get; set; } = Array.Empty<string>();
}
