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