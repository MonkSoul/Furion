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

#if !NET5_0
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
#else

using Microsoft.AspNetCore.Hosting;

#endif

namespace System;

#if !NET5_0
/// <summary>
/// <see cref="WebApplication"/> 方式配置选项
/// </summary>
#else
/// <summary>
/// <see cref="IWebHostBuilder"/> 方式配置选项
/// </summary>
#endif

[SuppressSniffer]
public sealed class RunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal RunOptions()
    {
    }

#if !NET5_0
    /// <summary>
    /// 默认配置
    /// </summary>
    public static RunOptions Default { get; } = new RunOptions();

    /// <summary>
    /// 默认配置（带启动参数）
    /// </summary>
    public static RunOptions Main(string[] args) => Default.WithArgs(args);

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static RunOptions DefaultSilence { get; } = new RunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static RunOptions MainSilence(string[] args) => DefaultSilence.WithArgs(args);
#else

    /// <summary>
    /// 默认配置
    /// </summary>
    public static LegacyRunOptions Default { get; } = new LegacyRunOptions();

    /// <summary>
    /// 默认配置（带启动参数）
    /// </summary>
    public static LegacyRunOptions Main(string[] args) => Default.WithArgs(args);

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static LegacyRunOptions DefaultSilence { get; } = new LegacyRunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static LegacyRunOptions MainSilence(string[] args) => DefaultSilence.WithArgs(args);

#endif

#if !NET5_0
    /// <summary>
    /// 配置 <see cref="WebApplicationOptions"/>
    /// </summary>
    /// <param name="options"></param>
    /// <returns><see cref="RunOptions"/></returns>
    public RunOptions ConfigureOptions(WebApplicationOptions options)
    {
        Options = options;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="WebApplicationBuilder"/>
    /// </summary>
    /// <param name="configureAction"></param>
    /// <returns><see cref="RunOptions"/></returns>
    public RunOptions ConfigureBuilder(Action<WebApplicationBuilder> configureAction)
    {
        ActionBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="WebApplication"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="RunOptions"/></returns>
    public RunOptions Configure(Action<WebApplication> configureAction)
    {
        ActionConfigure = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="ConfigurationManager"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="RunOptions"/></returns>
    public RunOptions ConfigureConfiguration(Action<IHostEnvironment, ConfigurationManager> configureAction)
    {
        ActionConfigurationManager = configureAction;
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public RunOptions AddComponent<TComponent>()
        where TComponent : class, IServiceComponent, new()
    {
        ServiceComponents.Add(typeof(TComponent), null);
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public RunOptions AddComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IServiceComponent, new()
    {
        ServiceComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public RunOptions AddComponent(Type componentType, object options)
    {
        ServiceComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public RunOptions UseComponent<TComponent>()
        where TComponent : class, IApplicationComponent, new()
    {
        ApplicationComponents.Add(typeof(TComponent), null);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public RunOptions UseComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IApplicationComponent, new()
    {
        ApplicationComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public RunOptions UseComponent(Type componentType, object options)
    {
        ApplicationComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 标识主机静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <returns></returns>
    public RunOptions Silence(bool silence = true, bool logging = false)
    {
        IsSilence = silence;
        SilenceLogging = logging;
        return this;
    }

    /// <summary>
    /// 设置进程启动参数
    /// </summary>
    /// <param name="args">启动参数</param>
    /// <returns></returns>
    public RunOptions WithArgs(string[] args)
    {
        Args = args;
        return this;
    }

    /// <summary>
    /// <see cref="WebApplicationOptions"/>
    /// </summary>
    internal WebApplicationOptions Options { get; set; }

    /// <summary>
    /// 自定义 <see cref="WebApplicationBuilder"/> 委托
    /// </summary>
    internal Action<WebApplicationBuilder> ActionBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="WebApplication"/> 委托
    /// </summary>
    internal Action<WebApplication> ActionConfigure { get; set; }

    /// <summary>
    /// 自定义 <see cref="ConfigurationManager"/> 委托
    /// </summary>
    internal Action<IHostEnvironment, ConfigurationManager> ActionConfigurationManager { get; set; }

    /// <summary>
    /// 应用服务组件
    /// </summary>
    internal Dictionary<Type, object> ServiceComponents { get; set; } = new();

    /// <summary>
    /// 应用中间件组件
    /// </summary>
    internal Dictionary<Type, object> ApplicationComponents { get; set; } = new();

    /// <summary>
    /// 静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    internal bool IsSilence { get; private set; }

    /// <summary>
    /// 静默启动日志状态
    /// </summary>
    internal bool SilenceLogging { get; set; }

    /// <summary>
    /// 命令行参数
    /// </summary>
    internal string[] Args { get; set; }
#endif
}