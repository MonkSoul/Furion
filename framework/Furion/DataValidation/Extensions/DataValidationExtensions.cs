// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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