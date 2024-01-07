// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.RegularExpressions;

namespace Furion.DataValidation;

/// <summary>
/// 验证项元数据
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Field)]
public class ValidationItemMetadataAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="regularExpression">正则表达式</param>
    /// <param name="defaultErrorMessage">失败提示默认消息</param>
    /// <param name="regexOptions">正则表达式匹配选项</param>
    public ValidationItemMetadataAttribute(string regularExpression, string defaultErrorMessage, RegexOptions regexOptions = RegexOptions.None)
    {
        RegularExpression = regularExpression;
        DefaultErrorMessage = defaultErrorMessage;
        RegexOptions = regexOptions;
    }

    /// <summary>
    /// 正则表达式
    /// </summary>
    public string RegularExpression { get; set; }

    /// <summary>
    /// 默认验证失败类型
    /// </summary>
    public string DefaultErrorMessage { get; set; }

    /// <summary>
    /// 正则表达式选项
    /// </summary>
    public RegexOptions RegexOptions { get; set; }
}