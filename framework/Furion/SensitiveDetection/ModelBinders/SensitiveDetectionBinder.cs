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