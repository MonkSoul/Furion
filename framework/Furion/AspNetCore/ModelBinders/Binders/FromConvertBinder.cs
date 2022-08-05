// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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