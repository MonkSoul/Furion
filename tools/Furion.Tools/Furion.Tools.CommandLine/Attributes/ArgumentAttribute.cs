// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数定义特性
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ArgumentAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="shortName">短参数名</param>
    /// <param name="longName">长参数名</param>
    /// <param name="helpText">帮助文本</param>
    public ArgumentAttribute(char shortName, string longName, string helpText = null)
    {
        ShortName = shortName;
        LongName = longName;
        HelpText = helpText;
    }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; set; }
}
