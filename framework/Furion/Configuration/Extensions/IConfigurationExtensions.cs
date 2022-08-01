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