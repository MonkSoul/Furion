using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据类型验证特性
    /// </summary>
    public class DataValidateAttribute : ValidationAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationLogic">验证逻辑</param>
        /// <param name="validationTypes"></param>
        public DataValidateAttribute(ValidationLogicOptions validationLogic, params object[] validationTypes)
        {
            ValidationLogic = validationLogic;
            ValidationTypes = validationTypes;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationTypes"></param>
        public DataValidateAttribute(params object[] validationTypes)
        {
            ValidationLogic = ValidationLogicOptions.And;
            ValidationTypes = validationTypes;
        }

        /// <summary>
        /// 验证逻辑
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 执行值验证
            var dataValidationResult = value.TryValidate(ValidationLogic, ValidationTypes);

            // 验证失败
            if (!dataValidationResult.IsValid)
            {
                return new ValidationResult(string.IsNullOrEmpty(ErrorMessage) ? dataValidationResult.ValidationResults.FirstOrDefault().ErrorMessage : ErrorMessage);
            }

            // 验证成功
            return ValidationResult.Success;
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        public object[] ValidationTypes { get; set; }

        /// <summary>
        /// 验证逻辑
        /// </summary>
        public ValidationLogicOptions ValidationLogic { get; set; }
    }
}