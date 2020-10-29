using Fur.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证项元数据
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Field)]
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
}