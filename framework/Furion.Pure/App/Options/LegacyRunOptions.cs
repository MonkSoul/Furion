// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// 泛型主机方式配置选项
/// </summary>
public sealed class LegacyRunOptions
{
    /// <summary>
    /// 私有构造函数
    /// </summary>
    private LegacyRunOptions()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static LegacyRunOptions Default { get; } = new LegacyRunOptions();

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public LegacyRunOptions ConfigureHostBuilder(Action<IHostBuilder> configureAction)
    {
        ActionBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="IWebHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public LegacyRunOptions ConfigureWebHostBuilder(Action<IWebHostBuilder> configureAction)
    {
        ActionWebHostBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// <see cref="IHostBuilder"/>
    /// </summary>
    internal IHostBuilder HostBuilder { get; set; }

    /// <summary>
    /// <see cref="IWebHostBuilder"/>
    /// </summary>
    internal IWebHostBuilder WebHostBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="IHostBuilder"/> 委托
    /// </summary>
    internal Action<IHostBuilder> ActionBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="IWebHostBuilder"/> 委托
    /// </summary>
    internal Action<IWebHostBuilder> ActionWebHostBuilder { get; set; }
}