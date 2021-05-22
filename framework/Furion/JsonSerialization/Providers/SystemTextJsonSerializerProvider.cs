// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.6.3
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion 
//          Github：https://github.com/monksoul/Furion 
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Furion.JsonSerialization
{
    /// <summary>
    /// System.Text.Json 序列化提供器（默认实现）
    /// </summary>
    [Injection(Order = -999)]
    public class SystemTextJsonSerializerProvider : IJsonSerializerProvider, ISingleton
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public string Serialize(object value, object jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(value, (jsonSerializerOptions ?? GetSerializerOptions()) as JsonSerializerOptions);
        }

        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json, object jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<T>(json, (jsonSerializerOptions ?? GetSerializerOptions()) as JsonSerializerOptions);
        }

        /// <summary>
        /// 返回读取全局配置的 JSON 选项
        /// </summary>
        /// <returns></returns>
        public object GetSerializerOptions()
        {
            return App.GetOptions<JsonOptions>()?.JsonSerializerOptions;
        }
    }
}