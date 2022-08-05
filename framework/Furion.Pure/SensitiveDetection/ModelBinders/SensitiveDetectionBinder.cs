// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Furion.SensitiveDetection;

/// <summary>
/// 脱敏词汇（脱敏）模型绑定器
/// </summary>
[SuppressSniffer]
public class SensitiveDetectionBinder : IModelBinder
{
    /// <summary>
    /// 绑定模型处理
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // 获取参数名称
        var modelName = bindingContext.ModelName;

        // 获取初始参数值提供器
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        // 如果未提供，则直接返回
        if (valueProviderResult == ValueProviderResult.None)
        {
            await bindingContext.DefaultAsync();
            return;
        }

        // 设置模型验证信息
        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        // 获取 Http 初始绑定值
        var value = valueProviderResult.FirstValue;

        // 模型绑定元数据
        var metadata = (bindingContext.ModelMetadata as DefaultModelMetadata);

        if (string.IsNullOrEmpty(value))
        {
            await bindingContext.DefaultAsync();
            return;
        }

        // 获取 [SensitiveDetection] 特性
        var sensitiveDetectionAttribute = metadata.Attributes.ParameterAttributes.FirstOrDefault(u => u.GetType() == typeof(SensitiveDetectionAttribute)) as SensitiveDetectionAttribute;

        // 替换字符
        var sensitiveWordsProvider = bindingContext.HttpContext.RequestServices.GetRequiredService<ISensitiveDetectionProvider>();
        var newValue = await sensitiveWordsProvider.ReplaceAsync(value, sensitiveDetectionAttribute.Transfer);

        // 替换模型绑定为最后值
        bindingContext.Result = ModelBindingResult.Success(newValue);
    }
}