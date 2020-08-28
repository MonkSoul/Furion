using System;

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
        public ValidationRegularExpressionAttribute(string regularExpression, string validateFailedMessage)
        {
            RegularExpression = regularExpression;
            ValidateFailedMessage = validateFailedMessage;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegularExpression { get; set; }

        /// <summary>
        /// 验证失败消息
        /// </summary>
        public string ValidateFailedMessage { get; set; }
    }
}