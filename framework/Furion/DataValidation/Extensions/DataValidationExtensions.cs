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

using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Furion.DataValidation;

/// <summary>
/// 数据验证拓展类
/// </summary>
[SuppressSniffer]
public static class DataValidationExtensions
{
    /// <summary>
    /// 拓展方法，验证类类型对象
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="validateAllProperties">是否验证所有属性</param>
    /// <returns>验证结果</returns>
    public static DataValidationResult TryValidate(this object obj, bool validateAllProperties = true)
    {
        return DataValidator.TryValidateObject(obj, validateAllProperties);
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationAttributes">验证特性</param>
    /// <returns></returns>
    public static DataValidationResult TryValidate(this object value, params ValidationAttribute[] validationAttributes)
    {
        return DataValidator.TryValidateValue(value, validationAttributes);
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationTypes">验证类型</param>
    /// <returns></returns>
    public static DataValidationResult TryValidate(this object value, params object[] validationTypes)
    {
        return DataValidator.TryValidateValue(value, validationTypes);
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationOptionss">验证逻辑</param>
    /// <param name="validationTypes">验证类型</param>
    /// <returns></returns>
    public static DataValidationResult TryValidate(this object value, ValidationPattern validationOptionss, params object[] validationTypes)
    {
        return DataValidator.TryValidateValue(value, validationOptionss, validationTypes);
    }

    /// <summary>
    /// 拓展方法，验证类类型对象
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="validateAllProperties">是否验证所有属性</param>
    public static void Validate(this object obj, bool validateAllProperties = true)
    {
        DataValidator.TryValidateObject(obj, validateAllProperties).ThrowValidateFailedModel();
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationAttributes">验证特性</param>
    public static void Validate(this object value, params ValidationAttribute[] validationAttributes)
    {
        DataValidator.TryValidateValue(value, validationAttributes).ThrowValidateFailedModel();
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationTypes">验证类型</param>
    public static void Validate(this object value, params object[] validationTypes)
    {
        DataValidator.TryValidateValue(value, validationTypes).ThrowValidateFailedModel();
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="validationOptionss">验证逻辑</param>
    /// <param name="validationTypes">验证类型</param>
    public static void Validate(this object value, ValidationPattern validationOptionss, params object[] validationTypes)
    {
        DataValidator.TryValidateValue(value, validationOptionss, validationTypes).ThrowValidateFailedModel();
    }

    /// <summary>
    /// 拓展方法，验证单个值
    /// </summary>
    /// <param name="value">单个值</param>
    /// <param name="regexPattern">正则表达式</param>
    /// <param name="regexOptions">正则表达式选项</param>
    /// <returns></returns>
    public static bool TryValidate(this object value, string regexPattern, RegexOptions regexOptions = RegexOptions.None)
    {
        return DataValidator.TryValidateValue(value, regexPattern, regexOptions);
    }

    /// <summary>
    /// 直接抛出异常信息
    /// </summary>
    /// <param name="dataValidationResult"></param>
    public static void ThrowValidateFailedModel(this DataValidationResult dataValidationResult)
    {
        if (!dataValidationResult.IsValid)
        {
            // 解析验证失败消息，输出统一格式
            var validationFailMessage =
                  dataValidationResult.ValidationResults
                  .Select(u => new {
                      MemberNames = u.MemberNames.Any() ? u.MemberNames : new[] { $"{dataValidationResult.MemberOrValue}" },
                      u.ErrorMessage
                  })
                  .OrderBy(u => u.MemberNames.First())
                  .GroupBy(u => u.MemberNames.First())
                  .ToDictionary(x => x.Key, u => u.Select(c => c.ErrorMessage).ToArray());

            // 抛出验证失败异常
            throw new AppFriendlyException(default, default, new ValidationException())
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ValidationException = true,
                ErrorMessage = validationFailMessage,
            };
        }
    }
}