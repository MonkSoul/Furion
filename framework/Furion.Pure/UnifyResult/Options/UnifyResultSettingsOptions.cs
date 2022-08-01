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

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Furion.UnifyResult;

/// <summary>
/// 规范化配置选项
/// </summary>
public sealed class UnifyResultSettingsOptions : IConfigurableOptions<UnifyResultSettingsOptions>
{
    /// <summary>
    /// 设置返回 200 状态码列表
    /// <para>默认：401，403，如果设置为 null，则标识所有状态码都返回 200 </para>
    /// </summary>
    public int[] Return200StatusCodes { get; set; }

    /// <summary>
    /// 适配（篡改）Http 状态码（只支持短路状态码，比如 401，403，500 等）
    /// </summary>
    public int[][] AdaptStatusCodes { get; set; }

    /// <summary>
    /// 是否支持 MVC 控制台规范化处理
    /// </summary>
    public bool? SupportMvcController { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(UnifyResultSettingsOptions options, IConfiguration configuration)
    {
        options.Return200StatusCodes ??= new[] { 401, 403 };
        options.SupportMvcController ??= false;
    }
}