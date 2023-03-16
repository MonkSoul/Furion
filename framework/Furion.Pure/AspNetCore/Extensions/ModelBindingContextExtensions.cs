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

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// <see cref="ModelBindingContext"/> 拓展
/// </summary>
[SuppressSniffer]
public static class ModelBindingContextExtensions
{
    /// <summary>
    /// 解析默认模型绑定
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static async Task DefaultAsync(this ModelBindingContext bindingContext, Action<ModelBindingContext> configure = default)
    {
        // 判断模型是否已经设置
        if (bindingContext.Result.IsModelSet) return;

        // 获取绑定信息
        var bindingInfo = bindingContext.ActionContext.ActionDescriptor.Parameters.First(u => u.Name == bindingContext.OriginalModelName).BindingInfo;

        // 创建模型元数据
        var modelMetadata = bindingContext.ModelMetadata.GetMetadataForType(bindingContext.ModelType);

        // 获取模型绑定工厂对象
        var modelBinderFactory = bindingContext.HttpContext.RequestServices.GetRequiredService<IModelBinderFactory>();

        // 创建默认模型绑定器
        var modelBinder = modelBinderFactory.CreateBinder(new ModelBinderFactoryContext
        {
            BindingInfo = bindingInfo,
            Metadata = modelMetadata
        });

        // 调用默认模型绑定器
        await modelBinder.BindModelAsync(bindingContext);

        // 处理回调
        configure?.Invoke(bindingContext);

        // 确保数据验证正常运行
        bindingContext.ValidationState[bindingContext.Result.Model] = new ValidationStateEntry
        {
            Metadata = bindingContext.ModelMetadata,
        };
    }
}