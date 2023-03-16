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