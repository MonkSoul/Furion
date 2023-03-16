// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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