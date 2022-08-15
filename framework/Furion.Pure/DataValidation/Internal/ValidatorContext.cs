// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
        // 其他类型
        else validationResults = errors?.ToString();

        return new ValidationMetadata
        {
            ValidationResult = validationResults,
            Message = JsonSerializer.Serialize(validationResults, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            }),
            ModelState = _modelState
        };
    }
}