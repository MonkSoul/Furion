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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Furion.AspNetCore;

/// <summary>
/// [FromConvert] 模型绑定器
/// </summary>
[SuppressSniffer]
public class FromConvertBinder : IModelBinder
{
    /// <summary>
    /// 定义模型绑定转换器集合
    /// </summary>
    private readonly ConcurrentDictionary<Type, Type> _modelBinderConverts;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="modelBinderConverts">定义模型绑定转换器集合</param>
    public FromConvertBinder(ConcurrentDictionary<Type, Type> modelBinderConverts)
    {
        _modelBinderConverts = modelBinderConverts;
    }

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

        // 获取参数名称
        var modelName = bindingContext.ModelName;

        // 模型绑定元数据
        var metadata = (bindingContext.ModelMetadata as DefaultModelMetadata);

        // 获取 [FromConvert] 特性
        var fromConvertAttribute = metadata.Attributes.ParameterAttributes.FirstOrDefault(u => u.GetType() == typeof(FromConvertAttribute)) as FromConvertAttribute;

        // 获取初始参数值提供器
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        // 是否完全自定义
        if (fromConvertAttribute.Customize != true)
        {
            // 如果未提供，则直接返回
            if (valueProviderResult == ValueProviderResult.None) return bindingContext.DefaultAsync();

            // 设置模型验证信息
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            // 处理空字符串问题
            if (fromConvertAttribute.AllowStringEmpty == false && string.IsNullOrEmpty(valueProviderResult.FirstValue)) return bindingContext.DefaultAsync();
        }

        // 获取转换后的值
        var (Value, ConvertBinder) = GetConvertValue(bindingContext
            , metadata
            , valueProviderResult
            , fromConvertAttribute
            , bindingContext.HttpContext.RequestServices);
        if (ConvertBinder == null) return bindingContext.DefaultAsync();

        // 如果已经自定义了 ModelBindingResult 则不再执行
        if (bindingContext.Result.IsModelSet) return Task.CompletedTask;

        // 替换模型绑定为最后值
        bindingContext.Result = ModelBindingResult.Success(Value);

        // 默认返回（必须）
        return Task.CompletedTask;
    }

    /// <summary>
    /// 创建模型转换绑定器
    /// </summary>
    /// <param name="valueType"></param>
    /// <param name="fromConvertAttribute"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    private IModelConvertBinder CreateConvertBinder(Type valueType, FromConvertAttribute fromConvertAttribute, IServiceProvider serviceProvider)
    {
        // 解析模型绑定器
        var modelConvertBinder = fromConvertAttribute.ModelConvertBinder;
        if (modelConvertBinder == null)
        {
            var canGet = _modelBinderConverts.TryGetValue(valueType, out var convert);
            if (canGet) modelConvertBinder = convert;
        }

        if (modelConvertBinder == null) return default;

        // 创建转换绑定器对象
        return ActivatorUtilities.CreateInstance(serviceProvider, modelConvertBinder) as IModelConvertBinder;
    }

    /// <summary>
    /// 获取转换后的值
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <param name="metadata"></param>
    /// <param name="valueProviderResult"></param>
    /// <param name="fromConvertAttribute"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    private (object Value, IModelConvertBinder ConvertBinder) GetConvertValue(ModelBindingContext bindingContext
        , DefaultModelMetadata metadata
        , ValueProviderResult valueProviderResult
        , FromConvertAttribute fromConvertAttribute
        , IServiceProvider serviceProvider)
    {
        // 创建转换绑定器对象
        var convertBinder = CreateConvertBinder(bindingContext.ModelType, fromConvertAttribute, serviceProvider);
        if (convertBinder == null) return (default, default);

        // 调用转换器
        var newValue = convertBinder.ConvertTo(bindingContext, metadata, valueProviderResult, fromConvertAttribute.Extras);

        return (newValue, convertBinder);
    }
}