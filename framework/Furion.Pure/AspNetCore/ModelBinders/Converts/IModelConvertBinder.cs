// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Furion.AspNetCore;

/// <summary>
/// 模型转换绑定器接口
/// </summary>
public interface IModelConvertBinder
{
    /// <summary>
    /// 模型绑定转换方法
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <param name="metadata"></param>
    /// <param name="valueProviderResult"></param>
    /// <param name="extras"></param>
    /// <returns></returns>
    object ConvertTo(ModelBindingContext bindingContext, DefaultModelMetadata metadata, ValueProviderResult valueProviderResult, object extras = default);
}