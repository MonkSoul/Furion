// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Microsoft.Extensions.Configuration;

/// <summary>
/// IConfiguration 接口拓展
/// </summary>
[SuppressSniffer]
public static class IConfigurationExtensions
{
    /// <summary>
    /// 判断配置节点是否存在
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <returns>是否存在</returns>
    public static bool Exists(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Exists();
    }

    /// <summary>
    /// 获取配置节点并转换成指定类型
    /// </summary>
    /// <typeparam name="T">节点类型</typeparam>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <returns>节点类型实例</returns>
    public static T Get<T>(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// 获取配置节点并转换成指定类型
    /// </summary>
    /// <typeparam name="T">节点类型</typeparam>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="configureOptions">配置值绑定到指定类型额外配置</param>
    /// <returns>节点类型实例</returns>
    public static T Get<T>(this IConfiguration configuration
        , string key
        , Action<BinderOptions> configureOptions)
    {
        return configuration.GetSection(key).Get<T>(configureOptions);
    }

    /// <summary>
    /// 获取节点配置
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="type">节点类型</param>
    /// <returns><see cref="object"/> 实例</returns>
    public static object Get(this IConfiguration configuration
        , string key
        , Type type)
    {
        return configuration.GetSection(key).Get(type);
    }

    /// <summary>
    /// 获取节点配置
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="key">节点路径</param>
    /// <param name="type">节点类型</param>
    /// <param name="configureOptions">配置值绑定到指定类型额外配置</param>
    /// <returns><see cref="object"/> 实例</returns>
    public static object Get(this IConfiguration configuration
        , string key
        , Type type
        , Action<BinderOptions> configureOptions)
    {
        return configuration.GetSection(key).Get(type, configureOptions);
    }
}