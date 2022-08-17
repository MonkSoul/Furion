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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// 泛型主机方式配置选项
/// </summary>
[SuppressSniffer]
public class GenericRunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal GenericRunOptions()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static GenericRunOptions Default { get; } = new GenericRunOptions();

    /// <summary>
    /// 默认配置（带启动参数）
    /// </summary>
    public static GenericRunOptions Main(string[] args) => Default.WithArgs(args);

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static GenericRunOptions DefaultSilence { get; } = new GenericRunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static GenericRunOptions MainSilence(string[] args) => DefaultSilence.WithArgs(args);

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="GenericRunOptions"/></returns>
    public virtual GenericRunOptions ConfigureBuilder(Func<IHostBuilder, IHostBuilder> configureAction)
    {
        ActionBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="IConfigurationBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="RunOptions"/></returns>
    public virtual GenericRunOptions ConfigureConfiguration(Action<IHostEnvironment, IConfigurationBuilder> configureAction)
    {
        ActionConfigurationManager = configureAction;
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public virtual GenericRunOptions AddComponent<TComponent>()
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
    public virtual GenericRunOptions AddComponent<TComponent, TComponentOptions>(TComponentOptions options)
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
    public virtual GenericRunOptions AddComponent(Type componentType, object options)
    {
        ServiceComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 标识主机静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <returns></returns>
    public virtual GenericRunOptions Silence(bool silence = true, bool logging = false)
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
    public virtual GenericRunOptions WithArgs(string[] args)
    {
        Args = args;
        return this;
    }

    /// <summary>
    /// 自定义 <see cref="IHostBuilder"/> 委托
    /// </summary>
    internal Func<IHostBuilder, IHostBuilder> ActionBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="IConfigurationBuilder"/> 委托
    /// </summary>
    internal Action<IHostEnvironment, IConfigurationBuilder> ActionConfigurationManager { get; set; }

    /// <summary>
    /// 应用服务组件
    /// </summary>
    internal Dictionary<Type, object> ServiceComponents { get; set; } = new();

    /// <summary>
    /// 静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    internal bool IsSilence { get; private set; }

    /// <summary>
    /// 启用静默启动日志
    /// </summary>
    internal bool SilenceLogging { get; set; }

    /// <summary>
    /// 命令行参数
    /// </summary>
    internal string[] Args { get; set; }
}