using Furion.DependencyInjection;
using Furion.Localization;
using Furion.SensitiveDetection;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 敏感词检查（脱敏处理）
    /// </summary>
    [SkipScan]
    public sealed class SensitiveDetectionAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证逻辑
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // null 和空字符串跳过检查
            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s))) return ValidationResult.Success;

            // 获取脱敏提供器
            var sensitiveWordsProvider = validationContext.GetService(typeof(ISensitiveDetectionProvider)) as ISensitiveDetectionProvider;

            // 判断符合
            var isVaild = sensitiveWordsProvider.IsVaildAsync(value.ToString()).GetAwaiter().GetResult();

            if (!isVaild)
            {
                // 默认提示
                var resultMessage = "Characters contain sensitive words.";

                // 进行多语言处理
                var errorMessage = string.IsNullOrWhiteSpace(ErrorMessage)
                    ? L.Text == null ? resultMessage : L.Text[resultMessage]
                    : ErrorMessage;

                return new ValidationResult(string.Format(errorMessage, validationContext.MemberName));
            }

            // 验证成功
            return ValidationResult.Success;
        }
    }
}