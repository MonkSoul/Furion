// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.5.1
// 源码地址：Gitee：https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.Localization;
using Furion.SensitiveDetection;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 脱敏词汇检查（脱敏处理）
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
            var isVaild = sensitiveWordsProvider.VaildedAsync(value.ToString()).GetAwaiter().GetResult();

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