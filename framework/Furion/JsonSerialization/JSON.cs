// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// JSON 静态帮助类
    /// </summary>
    [SuppressSniffer]
    public static class JSON
    {
        /// <summary>
        /// 获取 JSON 序列化提供器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IJsonSerializerProvider GetJsonSerializer(IServiceProvider serviceProvider = default)
        {
            return App.GetService<IJsonSerializerProvider>(serviceProvider ?? App.RootServices);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static string Serialize(object value, object jsonSerializerOptions = default, IServiceProvider serviceProvider = default)
        {
            return GetJsonSerializer(serviceProvider).Serialize(value, jsonSerializerOptions);
        }

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json, object jsonSerializerOptions = default, IServiceProvider serviceProvider = default)
        {
            return GetJsonSerializer(serviceProvider).Deserialize<T>(json, jsonSerializerOptions);
        }

        /// <summary>
        /// 获取 JSON 配置选项
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TOptions GetSerializerOptions<TOptions>(IServiceProvider serviceProvider = default)
            where TOptions : class
        {
            return GetJsonSerializer(serviceProvider).GetSerializerOptions() as TOptions;
        }
    }
}