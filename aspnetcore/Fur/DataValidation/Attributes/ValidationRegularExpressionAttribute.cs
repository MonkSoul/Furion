using System;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证表达式特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidationRegularExpressionAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regularExpression">正则表达式</param>
        /// <param name="defaultErrorMessage">默认验证失败类型</param>
        public ValidationRegularExpressionAttribute(string regularExpression, string defaultErrorMessage, RegexOptions regexOptions = RegexOptions.None)
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