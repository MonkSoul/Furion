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