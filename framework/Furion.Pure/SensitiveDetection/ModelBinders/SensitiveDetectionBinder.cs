// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
        if (valueProviderResult == ValueProviderResult.None) return;

        // 设置模型验证信息
        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        // 获取 Http 初始绑定值
        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value)) return;

        // 获取 [SensitiveDetection] 特性
        var sensitiveDetectionAttribute = (bindingContext.ModelMetadata as DefaultModelMetadata).Attributes.ParameterAttributes.FirstOrDefault(u => u.GetType() == typeof(SensitiveDetectionAttribute)) as SensitiveDetectionAttribute;

        // 替换字符
        var sensitiveWordsProvider = bindingContext.HttpContext.RequestServices.GetRequiredService<ISensitiveDetectionProvider>();
        var newValue = await sensitiveWordsProvider.ReplaceAsync(value, sensitiveDetectionAttribute.Transfer);

        // 如果不包含敏感词汇直接返回
        if (newValue == value) return;

        // 替换模型绑定为最后值
        bindingContext.Result = ModelBindingResult.Success(newValue);
    }
}