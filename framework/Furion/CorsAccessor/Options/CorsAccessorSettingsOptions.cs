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
using System.ComponentModel.DataAnnotations;

namespace Furion.CorsAccessor;

/// <summary>
/// 跨域配置选项
/// </summary>
public sealed class CorsAccessorSettingsOptions : IConfigurableOptions<CorsAccessorSettingsOptions>
{
    /// <summary>
    /// 策略名称
    /// </summary>
    [Required]
    public string PolicyName { get; set; }

    /// <summary>
    /// 允许来源域名，没有配置则允许所有来源
    /// </summary>
    public string[] WithOrigins { get; set; }

    /// <summary>
    /// 请求表头，没有配置则允许所有表头
    /// </summary>
    public string[] WithHeaders { get; set; }

    /// <summary>
    /// 设置客户端可获取的响应标头
    /// </summary>
    public string[] WithExposedHeaders { get; set; }

    /// <summary>
    /// 设置跨域允许请求谓词，没有配置则允许所有
    /// </summary>
    public string[] WithMethods { get; set; }

    /// <summary>
    /// 是否允许跨域请求中的凭据
    /// </summary>
    public bool? AllowCredentials { get; set; }

    /// <summary>
    /// 设置预检过期时间
    /// </summary>
    public int? SetPreflightMaxAge { get; set; }

    /// <summary>
    /// 修正前端无法获取 Token 问题
    /// </summary>
    public bool? FixedClientToken { get; set; }

    /// <summary>
    /// 启用 SignalR 跨域支持
    /// </summary>
    public bool? SignalRSupport { get; set; }

    /// <summary>
    /// 后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(CorsAccessorSettingsOptions options, IConfiguration configuration)
    {
        PolicyName ??= "App.Cors.Policy";
        WithOrigins ??= Array.Empty<string>();
        AllowCredentials ??= true;
        FixedClientToken ??= true;
        SignalRSupport ??= false;
    }
}