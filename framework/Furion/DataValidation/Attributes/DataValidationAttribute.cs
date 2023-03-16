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

using Furion.DataValidation;
using Furion.Localization;

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// 数据类型验证特性
/// </summary>
[SuppressSniffer]
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
        // 判断是否允许 空值
        if (AllowNullValue && value == null) return ValidationResult.Success;

        // 是否忽略空字符串
        if (AllowEmptyStrings && value is string && string.IsNullOrEmpty(value?.ToString())) return ValidationResult.Success;

        // 执行值验证
        var dataValidationResult = value.TryValidate(ValidationPattern, ValidationTypes);
        dataValidationResult.MemberOrValue = validationContext.MemberName;

        // 验证失败
        if (!dataValidationResult.IsValid)
        {
            var resultMessage = dataValidationResult.ValidationResults.FirstOrDefault().ErrorMessage;

            // 进行多语言处理
            var errorMessage = !string.IsNullOrWhiteSpace(ErrorMessage) ? ErrorMessage : resultMessage;

            return new ValidationResult(string.Format(L.Text == null ? errorMessage : L.Text[errorMessage], validationContext.MemberName));
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
    ///是否允许空字符串
    /// </summary>
    public bool AllowEmptyStrings { get; set; } = false;

    /// <summary>
    /// 允许空值，有值才验证，默认 false
    /// </summary>
    public bool AllowNullValue { get; set; } = false;
}