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
using Furion.JsonSerialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace System.Text.Json
{
    /// <summary>
    /// System.Text.Json 拓展
    /// </summary>
    [SuppressSniffer]
    public static class SystemTextJsonExtensions
    {
        /// <summary>
        /// 添加时间格式化
        /// </summary>
        /// <param name="converters"></param>
        /// <param name="formatString"></param>
        /// <param name="outputToLocalDateTime">自动转换 DateTimeOffset 为当地时间</param>
        public static void AddDateFormatString(this IList<JsonConverter> converters, string formatString, bool outputToLocalDateTime = false)
        {
            converters.Add(new DateTimeJsonConverter(formatString));
            converters.Add(new DateTimeOffsetJsonConverter(formatString, outputToLocalDateTime));
        }
    }
}