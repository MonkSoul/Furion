using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证器
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// 验证类类型对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="validateAllProperties">是否验证所有属性</param>
        /// <returns>验证结果</returns>
        public static DataValidationResult TryValidateObject(object obj, bool validateAllProperties = true)
        {
            // 如果该类型贴有 [NonVaildate] 特性，则跳过验证
            if (obj.GetType().IsDefined(typeof(NonVaildateAttribute), true))
                return new DataValidationResult
                {
                    IsVaild = true
                };

            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isVaild = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties);

            // 返回验证结果
            return new DataValidationResult
            {
                IsVaild = isVaild,
                ValidationResults = results
            };
        }

        /// <summary>
        /// 验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationAttributes">验证特性</param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, params ValidationAttribute[] validationAttributes)
        {
            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isVaild = Validator.TryValidateValue(value, new ValidationContext(value), results, validationAttributes);

            // 返回验证结果
            return new DataValidationResult
            {
                IsVaild = isVaild,
                ValidationResults = results
            };
        }
    }
}