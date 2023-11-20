// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司
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