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
        /// <param name="validateFailedMessage">验证失败消息</param>
        public ValidationRegularExpressionAttribute(string regularExpression, string validateFailedMessage, RegexOptions regexOptions = RegexOptions.None)
        {
            RegularExpression = regularExpression;
            ValidateFailedMessage = validateFailedMessage;
            RegexOptions = regexOptions;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegularExpression { get; set; }

        /// <summary>
        /// 验证失败消息
        /// </summary>
        public string ValidateFailedMessage { get; set; }

        /// <summary>
        /// 正则表达式选项
        /// </summary>
        public RegexOptions RegexOptions { get; set; }
    }
}