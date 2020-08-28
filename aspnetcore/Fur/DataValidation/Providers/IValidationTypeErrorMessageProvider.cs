using System.Collections.Generic;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证类型消息提供器
    /// </summary>
    public interface IValidationTypeErrorMessageProvider
    {
        /// <summary>
        /// 错误消息提供器
        /// </summary>
        Dictionary<string, string> Definitions { get; }
    }
}