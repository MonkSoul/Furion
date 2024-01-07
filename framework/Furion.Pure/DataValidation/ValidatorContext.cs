// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Furion.DataValidation;

/// <summary>
/// 验证上下文
/// </summary>
internal static class ValidatorContext
{
    /// <summary>
    /// 获取验证错误信息
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    internal static ValidationMetadata GetValidationMetadata(object errors)
    {
        ModelStateDictionary _modelState = null;
        object validationResults = null;
        (string message, string firstErrorMessage, string firstErrorProperty) = (default, default, default);

        // 判断是否是集合类型
        if (errors is IEnumerable && errors is not string)
        {
            // 如果是模型验证字典类型
            if (errors is ModelStateDictionary modelState)
            {
                _modelState = modelState;
                // 将验证错误信息转换成字典并序列化成 Json
                validationResults = modelState.Where(u => modelState[u.Key].ValidationState == ModelValidationState.Invalid)
                        .ToDictionary(u => u.Key, u => modelState[u.Key].Errors.Select(c => c.ErrorMessage).ToArray());
            }
            // 如果是 ValidationProblemDetails 特殊类型
            else if (errors is ValidationProblemDetails validation)
            {
                validationResults = validation.Errors
                    .ToDictionary(u => u.Key, u => u.Value.ToArray());
            }
            // 如果是字典类型
            else if (errors is Dictionary<string, string[]> dicResults)
            {
                validationResults = dicResults;
            }

            message = JsonSerializer.Serialize(validationResults, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            });
            firstErrorMessage = (validationResults as Dictionary<string, string[]>).First().Value[0];
            firstErrorProperty = (validationResults as Dictionary<string, string[]>).First().Key;
        }
        // 其他类型
        else
        {
            validationResults = firstErrorMessage = message = errors?.ToString();
        }

        return new ValidationMetadata
        {
            ValidationResult = validationResults,
            Message = message,
            ModelState = _modelState,
            FirstErrorProperty = firstErrorProperty,
            FirstErrorMessage = firstErrorMessage
        };
    }
}