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

using Furion.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Furion.AspNetCore;

/// <summary>
/// 时间戳转 DateTime 类型模型绑定
/// </summary>
[SuppressSniffer]
public sealed class TimestampToDateTimeModelBinder : IModelBinder
{
    /// <summary>
    /// 绑定模型处理
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // 获取模型名称（参数/属性/类名）
        var modelName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        // 获取值
        var value = valueProviderResult.FirstValue;

        var modelType = bindingContext.ModelType;
        var actType = modelType.IsGenericType && modelType.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? modelType.GenericTypeArguments[0]
            : modelType;

        DateTime dateTime;

        try
        {
            // 处理时间戳
            if (long.TryParse(value, out var timestamp))
            {
                dateTime = timestamp.ConvertToDateTime();
            }
            else
            {
                dateTime = Convert.ToDateTime(value);
            }

            if (actType == typeof(DateTime))
            {
                bindingContext.Result = ModelBindingResult.Success(dateTime);
            }
            else if (actType == typeof(DateTimeOffset))
            {
                bindingContext.Result = ModelBindingResult.Success(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
            }
        }
        catch
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"The value '{value}' is not valid for {modelName}.");
        }

        return Task.CompletedTask;
    }
}