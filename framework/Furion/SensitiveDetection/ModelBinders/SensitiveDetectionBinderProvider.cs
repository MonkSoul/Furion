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

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Furion.SensitiveDetection;

/// <summary>
/// 脱敏词汇（脱敏）提供器模型绑定
/// </summary>
/// <remarks>用于替换脱敏词汇</remarks>
[SuppressSniffer]
public class SensitiveDetectionBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// 返回自定义绑定器
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // 只处理字符串类型值
        if (context.Metadata.ModelType == typeof(string) && context.Metadata is DefaultModelMetadata actMetadata)
        {
            // 判断是否定义 [SensitiveDetection] 特性且 Transfer 不为空
            if (actMetadata.Attributes.ParameterAttributes != null
                && actMetadata.Attributes.ParameterAttributes.Count > 0
                && actMetadata.Attributes.ParameterAttributes.FirstOrDefault(u => u.GetType() == typeof(SensitiveDetectionAttribute))
                is SensitiveDetectionAttribute sensitiveDetectionAttribute && sensitiveDetectionAttribute.Transfer != default)
            {
                return new BinderTypeModelBinder(typeof(SensitiveDetectionBinder));
            }
        }

        return null;
    }
}