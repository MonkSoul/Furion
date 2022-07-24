// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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