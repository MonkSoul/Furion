using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.Localization;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 数据类型验证特性
    /// </summary>
    [SkipScan]
    public class DataValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationPattern">验证逻辑</param>
        /// <param name="validationTypes"></param>
        public DataValidationAttribute(ValidationPattern validationPattern, params object[] validationTypes)
        {
            ValidationPattern = validationPattern;
            ValidationTypes = validationTypes;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationTypes"></param>
        public DataValidationAttribute(params object[] validationTypes)
        {
            ValidationPattern = ValidationPattern.AllOfThem;
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
            // 如果值等于默认值，则跳过验证
            if (value == default || (AllowNullOrEmptyStrings && value.Equals(string.Empty))) return ValidationResult.Success;

            // 执行值验证
            var dataValidationResult = value.TryValidate(ValidationPattern, ValidationTypes);
            dataValidationResult.MemberOrValue = validationContext.MemberName;

            // 验证失败
            if (!dataValidationResult.IsValid)
            {
                var resultMessage = dataValidationResult.ValidationResults.FirstOrDefault().ErrorMessage;

                // 进行多语言处理
                var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                    ? L.Text == null ? resultMessage : L.Text[resultMessage]
                    : ErrorMessage;

                return new ValidationResult(string.Format(errorMessage, validationContext.MemberName));
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
        public ValidationPattern ValidationPattern { get; set; }

        /// <summary>
        /// 允许NULL或空字符串（也就是空字符串和Null都是通过的）
        /// </summary>
        public bool AllowNullOrEmptyStrings { get; set; } = true;
    }
}