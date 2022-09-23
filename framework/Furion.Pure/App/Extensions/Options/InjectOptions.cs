// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Furion;

/// <summary>
/// Inject 配置选项
/// </summary>
public sealed class InjectOptions
{
    /// <summary>
    /// 外部程序集名称
    /// </summary>
    public string AssemblyName { get; set; }

    /// <summary>
    /// 是否自动注册 BackgroundService
    /// </summary>
    public bool AutoRegisterBackgroundService { get; set; } = true;

    /// <summary>
    /// 配置 ConfigurationScanDirectories
    /// </summary>
    /// <param name="directories"></param>
    public void ConfigurationScanDirectories(params string[] directories)
    {
        InternalConfigurationScanDirectories = directories ?? Array.Empty<string>();
    }

    /// <summary>
    /// 配置 IgnoreConfigurationFiles
    /// </summary>
    /// <param name="files"></param>
    public void IgnoreConfigurationFiles(params string[] files)
    {
        InternalIgnoreConfigurationFiles = files ?? Array.Empty<string>();
    }

    /// <summary>
    /// 配置 ConfigureAppConfiguration
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configure)
    {
        AppConfigurationConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureAppConfiguration（Web）
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureWebAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configure)
    {
        WebAppConfigurationConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureServices
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureServices(Action<HostBuilderContext, IServiceCollection> configure)
    {
        ServicesConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureServices（Web）
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureWebServices(Action<WebHostBuilderContext, IServiceCollection> configure)
    {
        WebServicesConfigure = configure;
    }

    /// <summary>
    /// 配置配置文件扫描目录
    /// </summary>
    internal static IEnumerable<string> InternalConfigurationScanDirectories { get; private set; } = Array.Empty<string>();

    /// <summary>
    /// 配置配置文件忽略注册文件
    /// </summary>
    internal static IEnumerable<string> InternalIgnoreConfigurationFiles { get; private set; } = Array.Empty<string>();

    /// <summary>
    /// AppConfiguration 配置
    /// </summary>
    internal static Action<HostBuilderContext, IConfigurationBuilder> AppConfigurationConfigure { get; private set; }

    /// <summary>
    /// AppConfiguration 配置（Web）
    /// </summary>
    internal static Action<WebHostBuilderContext, IConfigurationBuilder> WebAppConfigurationConfigure { get; private set; }

    /// <summary>
    /// Services 配置
    /// </summary>
    internal static Action<HostBuilderContext, IServiceCollection> ServicesConfigure { get; private set; }

    /// <summary>
    /// Services 配置（Web）
    /// </summary>
    internal static Action<WebHostBuilderContext, IServiceCollection> WebServicesConfigure { get; private set; }
}