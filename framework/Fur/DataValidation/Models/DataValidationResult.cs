// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证结果
    /// </summary>
    public sealed class DataValidationResult
    {
        /// <summary>
        /// 验证状态
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
}