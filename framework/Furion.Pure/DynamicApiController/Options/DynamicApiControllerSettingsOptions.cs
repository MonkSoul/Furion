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

namespace Furion.DynamicApiController;

/// <summary>
/// 动态接口控制器配置
/// </summary>
public sealed class DynamicApiControllerSettingsOptions : IConfigurableOptions<DynamicApiControllerSettingsOptions>
{
    /// <summary>
    /// 默认路由前缀
    /// </summary>
    public string DefaultRoutePrefix { get; set; }

    /// <summary>
    /// 默认请求谓词
    /// </summary>
    [Required]
    public string DefaultHttpMethod { get; set; }

    /// <summary>
    /// 默认模块名称
    /// </summary>
    public string DefaultModule { get; set; }

    /// <summary>
    /// 小写路由
    /// </summary>
    public bool? LowercaseRoute { get; set; }

    /// <summary>
    /// 小驼峰命名（首字符小写）
    /// </summary>
    public bool? AsLowerCamelCase { get; set; }

    /// <summary>
    /// 保留行为名称谓词
    /// </summary>
    public bool? KeepVerb { get; set; }

    /// <summary>
    /// 保留名称
    /// </summary>
    public bool? KeepName { get; set; }

    /// <summary>
    /// 骆驼命名分隔符
    /// </summary>
    public string CamelCaseSeparator { get; set; }

    /// <summary>
    /// 版本号分隔符
    /// </summary>
    [Required]
    public string VersionSeparator { get; set; }

    /// <summary>
    /// 模型转查询参数（只有GET、HEAD请求有效）
    /// </summary>
    public bool? ModelToQuery { get; set; }

    /// <summary>
    /// 支持Mvc控制器处理
    /// </summary>
    public bool? SupportedMvcController { get; set; }

    /// <summary>
    /// 配置参数 [FromQuery] 化，默认 false ([FromRoute])
    /// </summary>
    public bool? UrlParameterization { get; set; }

    /// <summary>
    /// 被舍弃的控制器名称前后缀
    /// </summary>
    public string[] AbandonControllerAffixes { get; set; }

    /// <summary>
    /// 被舍弃的行为名称前后缀
    /// </summary>
    public string[] AbandonActionAffixes { get; set; }

    /// <summary>
    /// 复写默认配置路由规则配置
    /// </summary>
    public object[][] VerbToHttpMethods { get; set; }

    /// <summary>
    /// 默认区域
    /// </summary>
    public string DefaultArea { get; set; }

    /// <summary>
    /// 强制携带路由前缀，即使使用 [Route] 重写
    /// </summary>
    public bool? ForceWithRoutePrefix { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(DynamicApiControllerSettingsOptions options, IConfiguration configuration)
    {
        options.DefaultRoutePrefix ??= "api";
        options.DefaultHttpMethod ??= "POST";
        options.LowercaseRoute ??= true;
        options.AsLowerCamelCase ??= false;
        options.KeepVerb ??= false;
        options.KeepName ??= false;
        options.CamelCaseSeparator ??= "-";
        options.VersionSeparator ??= "@";
        options.ModelToQuery ??= false;
        options.SupportedMvcController ??= false;
        options.ForceWithRoutePrefix ??= false;
        options.AbandonControllerAffixes ??= new string[]
        {
                "AppServices",
                "AppService",
                "ApiController",
                "Controller",
                "Services",
                "Service"
        };
        options.AbandonActionAffixes ??= new string[]
        {
                "Async"
        };
    }
}