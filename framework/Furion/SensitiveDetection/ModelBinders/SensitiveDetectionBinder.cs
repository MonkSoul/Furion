// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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