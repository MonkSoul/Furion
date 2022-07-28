// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
}