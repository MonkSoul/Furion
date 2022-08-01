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
using Microsoft.Extensions.Hosting;

namespace Furion;

/// <summary>
/// 应用全局配置
/// </summary>
public sealed class AppSettingsOptions : IConfigurableOptions<AppSettingsOptions>
{
    /// <summary>
    /// 集成 MiniProfiler 组件
    /// </summary>
    public bool? InjectMiniProfiler { get; set; }

    /// <summary>
    /// 是否启用规范化文档
    /// </summary>
    public bool? InjectSpecificationDocument { get; set; }

    /// <summary>
    /// 是否启用引用程序集扫描
    /// </summary>
    public bool? EnabledReferenceAssemblyScan { get; set; }

    /// <summary>
    /// 外部程序集
    /// </summary>
    /// <remarks>扫描 dll 文件，如果是单文件发布，需拷贝放在根目录下</remarks>
    public string[] ExternalAssemblies { get; set; }

    /// <summary>
    /// 是否打印数据库连接信息到 MiniProfiler 中
    /// </summary>
    public bool? PrintDbConnectionInfo { get; set; }

    /// <summary>
    /// 是否输出原始 Sql 执行日志（ADO.NET）
    /// </summary>
    public bool? OutputOriginalSqlExecuteLog { get; set; }

    /// <summary>
    /// 配置支持的包前缀名
    /// </summary>
    public string[] SupportPackageNamePrefixs { get; set; }

    /// <summary>
    /// 【部署】二级虚拟目录
    /// </summary>
    public string VirtualPath { get; set; }

    /// <summary>
    /// 后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(AppSettingsOptions options, IConfiguration configuration)
    {
        // 非 Web 环境总是 false，如果是生产环境且不配置 InjectMiniProfiler，默认总是false，MiniProfiler 生产环境耗内存
        if (App.WebHostEnvironment == default
            || (App.HostEnvironment.IsProduction() && options.InjectMiniProfiler == null)) options.InjectMiniProfiler = false;
        else options.InjectMiniProfiler ??= true;

        options.InjectSpecificationDocument ??= true;
        options.EnabledReferenceAssemblyScan ??= false;
        options.ExternalAssemblies ??= Array.Empty<string>();
        options.PrintDbConnectionInfo ??= true;
        options.OutputOriginalSqlExecuteLog ??= true;
        options.SupportPackageNamePrefixs ??= Array.Empty<string>();
        options.VirtualPath ??= string.Empty;
    }
}