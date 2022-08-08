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