// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Hosting;

namespace System;

/// <summary>
/// Web 泛型主机方式配置选项
/// </summary>
[SuppressSniffer]
public sealed class LegacyRunOptions : GenericRunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal LegacyRunOptions()
        : base()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static new LegacyRunOptions Default { get; } = new LegacyRunOptions();

    /// <summary>
    /// 配置 <see cref="IWebHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public LegacyRunOptions ConfigureWebDefaults(Action<IWebHostBuilder> configureAction)
    {
        ActionWebHostBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public LegacyRunOptions UseComponent<TComponent>()
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
    public LegacyRunOptions UseComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : IApplicationComponent
    {
        ApplicationComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// <see cref="IWebHostBuilder"/>
    /// </summary>
    internal IWebHostBuilder WebHostBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="IWebHostBuilder"/> 委托
    /// </summary>
    internal Action<IWebHostBuilder> ActionWebHostBuilder { get; set; }

    /// <summary>
    /// 应用中间件组件
    /// </summary>
    internal Dictionary<Type, object> ApplicationComponents { get; set; } = new();
}