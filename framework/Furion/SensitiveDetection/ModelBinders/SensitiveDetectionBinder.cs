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