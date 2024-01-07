// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Localization;
using Furion.SensitiveDetection;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// 脱敏词汇检查（脱敏处理）
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class SensitiveDetectionAttribute : ValidationAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SensitiveDetectionAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="transfer"></param>
    public SensitiveDetectionAttribute(char transfer)
    {
        Transfer = transfer;
    }

    /// <summary>
    /// 替换为指定字符
    /// </summary>
    public char Transfer { get; set; }

    /// <summary>
    /// 验证逻辑
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // null 、非字符串和空字符串跳过检查
        if (value == null || value.GetType() != typeof(string) || (value is string s && string.IsNullOrWhiteSpace(s))) return ValidationResult.Success;

        // 获取脱敏提供器（如果未注册，直接跳过，而不是抛异常）
        if (validationContext.GetService(typeof(ISensitiveDetectionProvider)) is not ISensitiveDetectionProvider sensitiveWordsProvider) return ValidationResult.Success;

        var strValue = value.ToString();

        // 如果没有传入替换字符，则直接校验
        if (Transfer == default)
        {
            // 判断符合
            var isVaild = sensitiveWordsProvider.VaildedAsync(strValue).GetAwaiter().GetResult();

            if (!isVaild)
            {
                // 进行多语言处理
                var errorMessage = !string.IsNullOrWhiteSpace(ErrorMessage) ? ErrorMessage : "Characters contain sensitive words.";

                return new ValidationResult(string.Format(L.Text == null ? errorMessage : L.Text[errorMessage], validationContext.MemberName));
            }

            // 验证成功
            return ValidationResult.Success;
        }
        // 替换敏感词汇
        else
        {
            // 单个值已在模型绑定中处理
            if (validationContext.ObjectType == typeof(string)) return ValidationResult.Success;

            // 替换字符
            var newValue = sensitiveWordsProvider.ReplaceAsync(strValue, Transfer).GetAwaiter().GetResult();

            // 如果不包含敏感词汇直接返回
            if (newValue == strValue) return ValidationResult.Success;

            // 将对象属性值进行替换
            validationContext.ObjectType
                                .GetProperty(validationContext.MemberName, BindingFlags.Public | BindingFlags.Instance)
                                .SetValue(validationContext.ObjectInstance, newValue);

            // 验证成功
            return ValidationResult.Success;
        }
    }
}