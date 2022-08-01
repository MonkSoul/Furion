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

namespace Furion.DataValidation;

/// <summary>
/// 数据验证服务配置选项
/// </summary>
public sealed class DataValidationServiceOptions
{
    /// <summary>
    /// 启用全局数据验证
    /// </summary>
    public bool EnableGlobalDataValidation { get; set; } = true;

    /// <summary>
    /// 禁止C# 8.0 验证非可空引用类型
    /// </summary>
    public bool SuppressImplicitRequiredAttributeForNonNullableReferenceTypes { get; set; } = true;

    /// <summary>
    /// 是否禁用 MVC 模型验证过滤器
    /// </summary>
    /// <remarks>只会改变启用全局验证的情况，也就是 <see cref="EnableGlobalDataValidation"/> 为 true 的情况</remarks>
    public bool SuppressModelStateInvalidFilter { get; set; } = true;
}