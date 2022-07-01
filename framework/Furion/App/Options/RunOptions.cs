// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Builder;

namespace System;

/// <summary>
/// <see cref="WebApplication"/> 方式配置选项
/// </summary>
[SuppressSniffer]
public sealed class RunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal RunOptions()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static RunOptions Default { get; } = new RunOptions();

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
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public RunOptions AddComponent<TComponent>()
        where TComponent : IServiceComponent
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
        where TComponent : IServiceComponent
    {
        ServiceComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public RunOptions UseComponent<TComponent>()
        where TComponent : IApplicationComponent
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
        where TComponent : IApplicationComponent
    {
        ApplicationComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// <see cref="WebApplicationBuilder"/>
    /// </summary>
    internal WebApplicationBuilder Builder { get; set; }

    /// <summary>
    /// <see cref="WebApplication"/>
    /// </summary>
    internal WebApplication Application { get; set; }

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
    /// 应用服务组件
    /// </summary>
    internal Dictionary<Type, object> ServiceComponents { get; set; } = new();

    /// <summary>
    /// 应用中间件组件
    /// </summary>
    internal Dictionary<Type, object> ApplicationComponents { get; set; } = new();
}