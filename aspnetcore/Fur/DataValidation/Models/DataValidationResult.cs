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
        public bool IsVaild { get; set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
}